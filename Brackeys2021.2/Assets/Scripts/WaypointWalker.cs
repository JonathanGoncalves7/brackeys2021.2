using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointWalker : MonoBehaviour
{
    private int currentWaypoint;
    [SerializeField] private float speed = 0.5f;
    private float waypointRadius;
    private float walkCooldown = 2;
    private Waypoints waypoints;
    private bool isWalking;

    void Start()
    {
        waypoints = FindObjectOfType<Waypoints>().GetComponent<Waypoints>();
        StartCoroutine("Walking");
        isWalking = false;

    }



    public IEnumerator Walking()
    {
        yield return new WaitForSeconds(walkCooldown);


        int maxRandomNumber = waypoints.waypointsCercadinho1.Length;
        int randomWaypoint = Random.Range(0, maxRandomNumber);


        
        if (randomWaypoint <= waypoints.waypointsCercadinho1.Length && isWalking == false)
        {
            currentWaypoint = randomWaypoint;
            while (Vector3.Distance(transform.position, waypoints.waypointsCercadinho1[currentWaypoint].transform.position) > 0.5f)
            {
                isWalking = true;
                transform.position = Vector3.Lerp(transform.position, waypoints.waypointsCercadinho1[currentWaypoint].transform.position,speed * Time.deltaTime);

            }
            isWalking = false;
            StartCoroutine("Walking");  
        }


    }
}
