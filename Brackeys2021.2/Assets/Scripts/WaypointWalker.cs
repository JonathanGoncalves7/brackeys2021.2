using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointWalker : MonoBehaviour
{
    [Header("Waypoints")]
    [SerializeField] private Waypoints waypoints;

    private int currentWaypoint;
    private NavMeshAgent agent;
    private Rigidbody rb;
    private SpriteRenderer sprite;
    [SerializeField] private float restWalkCooldown = 0f;

    float GetWalkCooldown()
    {
        float walkCooldown = CaosManager.Instance.GetWalkCooldown();
        float currentCaosPoints = CaosManager.Instance.GetCaosPoints();
        float porcentage = CaosManager.Instance.GetPercentageWalkCooldownMaximumChaos();

        return walkCooldown - (walkCooldown * (porcentage * currentCaosPoints / 100));
    }

    float GetSpeed()
    {
        float speed = CaosManager.Instance.Getspeed();
        float currentCaosPoints = CaosManager.Instance.GetCaosPoints();
        float porcentage = CaosManager.Instance.GetPercentageSpeedMaximumChaos();

        return speed + (speed * (porcentage * currentCaosPoints / 100));
    }

    void FlipSprite(bool flip)
    {
        sprite.flipX = flip;
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        agent.autoBraking = true;
        agent.speed = GetSpeed();

        restWalkCooldown = Random.Range(0, CaosManager.Instance.GetWalkCooldown());
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

        if (restWalkCooldown <= 0 && CaosManager.Instance.GetCaosPoints() > 0)
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
