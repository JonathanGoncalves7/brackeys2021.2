using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointWalker : MonoBehaviour
{
    [Header("Walking")]
    [SerializeField] private float walkCooldown = 5f;
    [SerializeField] [Range(0, 95)] private int percentageWalkCooldownMaximumChaos = 75;
    [SerializeField] private float restWalkCooldown = 0f;
    [Space]
    [Header("Speed")]
    [SerializeField] private float speed = 3.5f;
    [SerializeField] [Range(0, 75)] private int percentageSpeedMaximumChaos = 50;
    [Space]
    [Header("Waypoints")]
    [SerializeField] private Waypoints waypoints;

    private int currentWaypoint;
    private NavMeshAgent agent;
    private Rigidbody rb;
    private PlayerController playerController;
    private SpriteRenderer sprite;


    float GetWalkCooldown()
    {
        float currentCaosPoints = playerController.GetCaosPoints();
        return walkCooldown - (walkCooldown * (percentageWalkCooldownMaximumChaos * currentCaosPoints / 100));
    }

    float GetSpeed()
    {
        float currentCaosPoints = playerController.GetCaosPoints();
        return speed + (speed * (percentageSpeedMaximumChaos * currentCaosPoints / 100));
    }

    void FlipSprite(bool flip)
    {
        sprite.flipX = flip;
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        agent.autoBraking = true;
        agent.speed = speed;

        restWalkCooldown = Random.Range(0, walkCooldown);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            agent.enabled = true;
        }
    }

    private void Update()
    {
        if (!rb.useGravity)
        {
            agent.enabled = false;
        }

        if (!agent.enabled)
            return;

        if (!waypoints)
            return;

        Walking();
        UpdateSpeed();
    }

    void Walking()
    {
        if (isDestination())
        {
            restWalkCooldown -= Time.deltaTime;
        }

        if (restWalkCooldown <= 0 && playerController.GetCaosPoints() > 0)
        {
            restWalkCooldown = GetWalkCooldown();
            currentWaypoint = Random.Range(0, waypoints.waypoints.Count);

            bool posicaoDoDestinoFicaParaEsquerda = transform.position.x - waypoints.waypoints[currentWaypoint].transform.position.x < 0;

            FlipSprite(posicaoDoDestinoFicaParaEsquerda);

            agent.destination = waypoints.waypoints[currentWaypoint].transform.position;
        }
    }

    void UpdateSpeed()
    {
        agent.speed = GetSpeed();
    }

    bool isDestination()
    {
        return agent.remainingDistance < 0.5f;
    }
}
