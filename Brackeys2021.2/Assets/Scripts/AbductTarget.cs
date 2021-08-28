using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbductTarget : MonoBehaviour
{
    private PlayerController playerController;

    [SerializeField] private float speed = 1f;
    [SerializeField] private float castingRadius = 2f;

    private Vector3 origin;
    private Vector3 direction;

    public Transform raycastOrigin;
    public GameObject currentHitObject;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    void FixedUpdate()
    {
        Abduction();
    }

    void Abduction()
    {
        if (playerController.GetAbducaoPoints() <= 0)
            return;

        origin = raycastOrigin.position;
        direction = Vector3.down;
        RaycastHit raycastHit;

        if (Physics.SphereCast(origin, castingRadius, direction, out raycastHit))
        {
            currentHitObject = raycastHit.transform.gameObject;
            if (currentHitObject.CompareTag("Target") && Input.GetKey(KeyCode.Space))
            {
                raycastHit.rigidbody.useGravity = false;
                raycastHit.rigidbody.MovePosition(Vector3.Lerp(raycastHit.transform.position, origin, speed * Time.fixedDeltaTime));
            }
            else if (Input.GetKeyUp(KeyCode.Space) && raycastHit.rigidbody != null)
            {
                raycastHit.rigidbody.useGravity = true;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(origin, castingRadius);
    }
}
