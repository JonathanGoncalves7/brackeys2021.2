using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointWalker : MonoBehaviour
{
    [SerializeField] private Waypoints waypoints;
    [SerializeField] private float restWalkCooldown = 0f;

    private int currentWaypoint;
    private NavMeshAgent agent;
    private Rigidbody rb;
    private SpriteRenderer sprite;

    float GetWalkCooldown()
    {
        float walkCooldown = CaosManager.Instance.GetWalkCooldown();
        float currentCaosPoints = CaosManager.Instance.GetCaosPoints();
        float porcentage = CaosManager.Instance.GetPercentageWalkCooldownMaximumChaos();

        return walkCooldown - (walkCooldown * (porcentage * currentCaosPoints / 100));
    }

    float GetSpeedWalk()
    {
        float speed = CaosManager.Instance.GetSpeed();
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

        UpdateSpeedAndAcceleration();

        restWalkCooldown = 0;
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

        UpdateSpeedAndAcceleration();
        Walking();
    }

    void Walking()
    {
        if (isDestination())
        {
            restWalkCooldown -= Time.deltaTime;
        }

        if (CaosManager.Instance.GetCaosPoints() <= 0)
        {
            agent.isStopped = true;
            agent.destination = transform.position;
            restWalkCooldown = 0;
            return;
        }

        if (restWalkCooldown <= 0)
        {
            agent.isStopped = false;
            restWalkCooldown = GetWalkCooldown();
            currentWaypoint = Random.Range(0, waypoints.waypoints.Count);

            bool positionDestinationToLeft = transform.position.x - waypoints.waypoints[currentWaypoint].transform.position.x < 0;

            FlipSprite(positionDestinationToLeft);

            agent.destination = waypoints.waypoints[currentWaypoint].transform.position;
        }
    }

    void UpdateSpeedAndAcceleration()
    {
        agent.speed = GetSpeedWalk();
        agent.acceleration = GetSpeedWalk();
    }

    bool isDestination()
    {
        return agent.remainingDistance < 0.5f;
    }
}
