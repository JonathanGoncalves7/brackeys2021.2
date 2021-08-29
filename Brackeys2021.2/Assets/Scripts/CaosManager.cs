using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaosManager : MonoBehaviour
{
    [SerializeField] [Range(0, 1)] private float caosPoints = 0f;
    [SerializeField] private float caosPointsAdicionadoPorAbducao = 0.1f;
    [Space]
    [Header("Animals Walking")]
    [SerializeField] private float walkCooldown = 5f;
    [SerializeField] [Range(0, 95)] private int percentageWalkCooldownMaximumChaos = 75;
    [Space]
    [Header("Animals Speed")]
    [SerializeField] private float speed = 3.5f;
    [SerializeField] [Range(0, 75)] private int percentageSpeedMaximumChaos = 50;
    [Space]
    [Header("Animals Sounds")]
    [SerializeField] private float minTimeToStartSounds = 4f;
    [SerializeField] private float maxTimeToStartSounds = 8f;
    [SerializeField] [Range(0, 75)] private float percentageReduceTimeMaximumChaos = 50f;

    public static CaosManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

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

    public float GetSpeed()
    {
        return speed;
    }

    public int GetPercentageSpeedMaximumChaos()
    {
        return percentageSpeedMaximumChaos;
    }

    public float GetMinTimeToStartSounds()
    {
        return minTimeToStartSounds;
    }

    public float GetMaxTimeToStartSounds()
    {
        return maxTimeToStartSounds;
    }
    public float GetPercentageReduceTimeSoundsMaximumChaos()
    {
        return percentageReduceTimeMaximumChaos;
    }
}
