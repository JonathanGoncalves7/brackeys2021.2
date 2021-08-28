using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    public float moveSpeed;

    private Vector2 moveInput;

    public Slider caosSlider, abducaoSlider;
    [SerializeField] private float caosSliderFloat, abducaoSliderFloat;

    public void AddCaosPoints(float value)
    {
        caosSliderFloat += value;

        if (caosSliderFloat > 1)
            caosSliderFloat = 1;
    }

    public void SubCaosPoints(float value)
    {
        caosSliderFloat -= value;

        if (caosSliderFloat <= 0)
            caosSliderFloat = 0;
    }

    public void AddAbducaoPoints(float value)
    {
        abducaoSliderFloat += value;

        if (abducaoSliderFloat > 1)
            abducaoSliderFloat = 1;
    }

    public void SubAbducaoPoints(float value)
    {
        abducaoSliderFloat -= value;

        if (abducaoSliderFloat <= 0)
            abducaoSliderFloat = 0;
    }

    public float GetCaosPoints()
    {
        return caosSliderFloat;
    }

    public float GetAbducaoPoints()
    {
        return abducaoSliderFloat;
    }

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        caosSliderFloat = 0f;
        abducaoSliderFloat = 1f;
    }

    void FixedUpdate()
    {
        MovePlayer();
        UpdateSliders();
    }

    private void MovePlayer()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        moveInput.Normalize();

        playerRB.velocity = new Vector3(moveInput.x * moveSpeed, playerRB.velocity.y, moveInput.y * moveSpeed);
    }

    private void UpdateSliders()
    {
        caosSlider.value = caosSliderFloat;
        abducaoSlider.value = abducaoSliderFloat;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.tag == "Target")
        {

            Destroy(collision.gameObject);
            caosSliderFloat += 0.1f;
            abducaoSliderFloat -= 0.1f;
        }

    }
}
