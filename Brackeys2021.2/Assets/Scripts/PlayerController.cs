using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Movimentacao")]
    [SerializeField] private float moveSpeed = 50;
    private Vector2 moveInput;

    [Header("Caos")]
    [SerializeField] private float caosPoints = 0;
    [SerializeField] private float caosPointsAdicionadoPorAbducao = 0.1f;
    [SerializeField] private Slider caosSlider;

    [Header("Abducao")]
    [SerializeField] private float abducaoPoints = 1;
    [SerializeField] private float abducaoPointsPerdidoPorAbducao = 0.1f;
    [SerializeField] private Slider abducaoSlider;

    private Rigidbody playerRB;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        SetCaosPoints(0);
        SetAbducaoPoints(1);
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
        caosSlider.value = caosPoints;
        abducaoSlider.value = abducaoPoints;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.CompareTag("Target"))
        {
            Destroy(collision.gameObject);

            AddCaosPoints(caosPointsAdicionadoPorAbducao);
            SubAbducaoPoints(abducaoPointsPerdidoPorAbducao);
        }

    }

    public void AddCaosPoints(float value)
    {
        caosPoints += value;

        if (caosPoints > 1)
            caosPoints = 1;
    }

    public void SubCaosPoints(float value)
    {
        caosPoints -= value;

        if (caosPoints <= 0)
            caosPoints = 0;
    }

    public void AddAbducaoPoints(float value)
    {
        abducaoPoints += value;

        if (abducaoPoints > 1)
            abducaoPoints = 1;
    }

    public void SubAbducaoPoints(float value)
    {
        abducaoPoints -= value;

        if (abducaoPoints <= 0)
            abducaoPoints = 0;
    }

    public float GetCaosPoints()
    {
        return caosPoints;
    }

    public float GetAbducaoPoints()
    {
        return abducaoPoints;
    }

    public void SetCaosPoints(float value)
    {
        if (value > 1)
            value = 1;

        if (value < 0)
            value = 0;

        caosPoints = value;
    }

    public void SetAbducaoPoints(float value)
    {
        if (value > 1)
            value = 1;

        if (value < 0)
            value = 0;

        abducaoPoints = value;
    }
}
