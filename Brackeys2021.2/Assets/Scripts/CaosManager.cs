using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaosManager : MonoBehaviour
{
    [SerializeField] private float caosPoints = 0f;
    [SerializeField] private float caosPointsAdicionadoPorAbducao = 0.1f;
    [Space]
    [Header("Animals Walking")]
    [SerializeField] private float walkCooldown = 5f;
    [SerializeField] [Range(0, 95)] private int percentageWalkCooldownMaximumChaos = 75;
    [Space]
    [Header("Animals Speed")]
    [SerializeField] private float speed = 3.5f;
    [SerializeField] [Range(0, 75)] private int percentageSpeedMaximumChaos = 50;

    private static CaosManager _instance;

    public static CaosManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<CaosManager>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SetCaosPoints(0);
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


    public float GetCaosPoints()
    {
        return caosPoints;
    }


    public void SetCaosPoints(float value)
    {
        if (value > 1)
            value = 1;

        if (value < 0)
            value = 0;

        caosPoints = value;
    }

    public float GetCaosPointsAdicionadoPorAbducao()
    {
        return caosPointsAdicionadoPorAbducao;
    }


    public float GetWalkCooldown()
    {
        return walkCooldown;
    }


    public int GetPercentageWalkCooldownMaximumChaos()
    {
        return percentageWalkCooldownMaximumChaos;
    }

    public float Getspeed()
    {
        return speed;
    }

    public int GetPercentageSpeedMaximumChaos()
    {
        return percentageSpeedMaximumChaos;
    }
}
