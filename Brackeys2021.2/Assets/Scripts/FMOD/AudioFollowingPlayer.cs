using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFollowingPlayer : MonoBehaviour
{
    [SerializeField] private Vector3 area;
    [SerializeField] private string pathAudio;
    private Transform Player;
    private float xPosMax;
    private float xPosMin;
    private float yPosMax;
    private float yPosMin;
    private float zPosMax;
    private float zPosMin;
    private Vector3 audioPoint;

    private float timeLeftToStart = 0;

    private FMOD.Studio.EventInstance audioLoop;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        audioLoop = FMODUnity.RuntimeManager.CreateInstance("event:/" + pathAudio);

        timeLeftToStart = Random.Range(
            CaosManager.Instance.GetMinTimeToStartSounds(),
            CaosManager.Instance.GetMaxTimeToStartSounds());
    }


    void UpdatePosMax()
    {
        xPosMax = transform.position.x + (area.x * transform.lossyScale.x / 2f);
        yPosMax = transform.position.y + (area.y * transform.lossyScale.y / 2f);
        zPosMax = transform.position.z + (area.z * transform.lossyScale.z / 2f);
    }

    void UpdatePosMin()
    {
        xPosMin = transform.position.x - (area.x * transform.lossyScale.x / 2f);
        yPosMin = transform.position.y - (area.y * transform.lossyScale.y / 2f);
        zPosMin = transform.position.z - (area.z * transform.lossyScale.z / 2f);
    }

    Vector3 GetAudioPoint()
    {
        float xPos = Mathf.Clamp(Player.position.x, xPosMin, xPosMax);
        float yPos = Mathf.Clamp(Player.position.y, yPosMin, yPosMax);
        float zPos = Mathf.Clamp(Player.position.z, zPosMin, zPosMax);
        return new Vector3(xPos, yPos, zPos);
    }

    void StartAudio()
    {
        timeLeftToStart -= Time.deltaTime;
        if (timeLeftToStart <= 0)
        {
            timeLeftToStart = GetTime();

            audioLoop.start();
        }
    }

    void Update()
    {
        UpdatePosMax();
        UpdatePosMin();

        audioPoint = GetAudioPoint();

        audioLoop.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(audioPoint));

        StartAudio();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(audioPoint, 0.25f);
    }

    private void OnDestroy()
    {
        audioLoop.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    float GetTime()
    {
        float time = Random.Range(CaosManager.Instance.GetMinTimeToStartSounds(), CaosManager.Instance.GetMaxTimeToStartSounds());
        float currentCaosPoints = CaosManager.Instance.GetCaosPoints();
        float porcentage = CaosManager.Instance.GetPercentageReduceTimeSoundsMaximumChaos();

        return time - (time * porcentage * currentCaosPoints / 100);
    }
}
