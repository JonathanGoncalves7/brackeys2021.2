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
    [SerializeField] private Slider caosSlider;

    [Header("Abducao")]
    [SerializeField] private float abducaoPoints = 1;
    [SerializeField] private float abducaoPointsPerdidoPorAbducao = 0.1f;
    [SerializeField] private Slider abducaoSlider;
    [SerializeField] private Animation abducaoAnim;
    [SerializeField] private Animation caosAnim;

    //private readonly string anim_abducao_idle = "anim_Abduction_Idle";
    private readonly string anim_abducao_dropping = "anim_Abduction_Using";

    private readonly string anim_caos_idle = "anim_Chaos_Idle";
    private readonly string anim_caos_total = "anim_Chaos_Total";

    private Rigidbody playerRB;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();

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
        caosSlider.value = CaosManager.Instance.GetCaosPoints();
        abducaoSlider.value = abducaoPoints;

        if(caosSlider.value >= 1)
        {
            caosAnim.Play(anim_caos_total);
        }
        else
        {
            if(caosAnim.isPlaying)
            {
                caosAnim.Stop();
                caosAnim.Play(anim_caos_idle);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.CompareTag("Target"))
        {
            Destroy(collision.gameObject);

            CaosManager.Instance.AddCaosPoints(CaosManager.Instance.GetCaosPointsAdicionadoPorAbducao());
            SubAbducaoPoints(abducaoPointsPerdidoPorAbducao);
            PlayAbducaoAnimation();
        }

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

    public float GetAbducaoPoints()
    {
        return abducaoPoints;
    }

    public void SetAbducaoPoints(float value)
    {
        if (value > 1)
            value = 1;

        if (value < 0)
            value = 0;

        abducaoPoints = value;
    }

    public void PlayAbducaoAnimation()
    {
        abducaoAnim.Play(anim_abducao_dropping);
    }

    public bool PermiteAbduzir()
    {
        return (abducaoPoints - abducaoPointsPerdidoPorAbducao) >= 0;
    }
}
