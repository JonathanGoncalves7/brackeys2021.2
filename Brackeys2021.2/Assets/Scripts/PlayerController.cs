using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    public float moveSpeed;
    public float castingRadius;
    public Transform raycastOrigin;
    public GameObject currentHitObject;

    private Vector2 moveInput;

    private Vector3 origin;
    private Vector3 direction;

    public Slider caosSlider, abducaoSlider;
    private float caosSliderFloat, abducaoSliderFloat;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        caosSliderFloat = 0f;
        abducaoSliderFloat = 1f;
    }

    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        moveInput.Normalize();

        playerRB.velocity = new Vector3
            (moveInput.x * moveSpeed, playerRB.velocity.y, moveInput.y * moveSpeed);

        AbductionRaycast();

        caosSlider.value = caosSliderFloat;
        abducaoSlider.value = abducaoSliderFloat;
        
        
    }

    void AbductionRaycast()
    {
        origin = raycastOrigin.position;
        direction = Vector3.down;
        RaycastHit raycastHit;

        Debug.DrawRay(origin, direction * moveSpeed, Color.red);

        if(Physics.SphereCast(origin, castingRadius, direction, out raycastHit))
        {
            currentHitObject = raycastHit.transform.gameObject;
            if (currentHitObject.tag == "Target" && Input.GetKey(KeyCode.Space))
            {
                
                raycastHit.rigidbody.useGravity = false;
                raycastHit.transform.position = Vector3.Lerp(raycastHit.transform.position, transform.position, 0.5f * Time.deltaTime);
                Debug.Log(raycastHit.transform.name);
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                raycastHit.rigidbody.useGravity = true;
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.gameObject.tag == "Target")
        {
            
            Destroy(collision.gameObject);
            caosSliderFloat += 0.1f;
            abducaoSliderFloat -= 0.1f;
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(origin, castingRadius);
    }
}
