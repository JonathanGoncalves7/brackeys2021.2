using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] Transform playerVisual;
    [SerializeField] float rotateAmount;
    [SerializeField] float rotateSpeed;

    Rigidbody rb;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 newRotation = new Vector3(0, 0, 1) * -rb.velocity.normalized.x * rotateAmount;
        newRotation.x = 30f;
        playerVisual.localRotation = Quaternion.Lerp(playerVisual.localRotation, Quaternion.Euler(newRotation), rotateAmount * rotateSpeed * Time.deltaTime);
    }

}
