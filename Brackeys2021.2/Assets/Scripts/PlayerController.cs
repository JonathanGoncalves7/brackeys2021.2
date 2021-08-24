using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    public float moveSpeed;

    private Vector2 moveInput;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        moveInput.Normalize();

        playerRB.velocity = new Vector3
            (moveInput.x * moveSpeed, playerRB.velocity.y, moveInput.y * moveSpeed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AbductionRaycast();
        }
        
    }

    void AbductionRaycast()
    {
        Vector3 origin = transform.position;
        Vector3 direction = Vector3.down;

        Debug.DrawRay(origin, direction * moveSpeed, Color.red);
        Ray ray = new Ray(origin, direction);

        if(Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            if(raycastHit.transform.tag == "Target")
            {
                Vector3 targetPos = raycastHit.transform.position;
                raycastHit.transform.position = Vector3.Lerp(targetPos, origin, 5f * Time.deltaTime);
            }
        }
    }

}
