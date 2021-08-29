using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbductTarget : MonoBehaviour
{
    private PlayerController playerController;

    [SerializeField] private float speed = 1f;
    [SerializeField] private float castingRadius = 2f;
    public GameObject effectAbduction;

    private Vector3 origin;
    private Vector3 direction;

    public Transform raycastOrigin;
    public GameObject currentTarget;

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
        {
            effectAbduction.SetActive(false);
            return;
        }

        effectAbduction.SetActive(Input.GetKey(KeyCode.Space));

        origin = raycastOrigin.position;
        direction = Vector3.down;
        RaycastHit[] raycastHit = Physics.SphereCastAll(origin, castingRadius, direction);

        GameObject nextTarget = null;

        for (int i = 0; i < raycastHit.Length; i++)
        {
            GameObject gameObjectHit = raycastHit[i].transform.gameObject;

            if (!gameObjectHit.CompareTag("Target"))
                continue;

            if (nextTarget == null)
                nextTarget = gameObjectHit;

            if (gameObjectHit.Equals(currentTarget))
            {
                nextTarget = currentTarget;
            }
        }

        if (currentTarget != null && !currentTarget.Equals(nextTarget))
        {
            currentTarget.GetComponent<Rigidbody>().useGravity = true;
            currentTarget.GetComponent<Rigidbody>().isKinematic = false;
        }

        currentTarget = nextTarget;

        if (currentTarget != null)
        {
            Rigidbody targetRigidbody = currentTarget.GetComponent<Rigidbody>();

            if (Input.GetKey(KeyCode.Space))
            {
                targetRigidbody.useGravity = false;
                targetRigidbody.isKinematic = true;
                targetRigidbody.transform.position = (Vector3.Lerp(currentTarget.transform.position, origin, speed * Time.fixedDeltaTime));
            }
            else
            {
                targetRigidbody.useGravity = true;
                targetRigidbody.isKinematic = false;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(origin, castingRadius);
    }
}
