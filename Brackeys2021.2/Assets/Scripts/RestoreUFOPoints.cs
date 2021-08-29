using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreUFOPoints : MonoBehaviour
{
    const string RECOVERING_POWER_PATH = "event:/SFX/HUD/Power Bar/recovering_power";
    const string RECOVERING_POWER_PARAM = "Recovering Power";

    [SerializeField] private float abducaoPointsRestore = 0.1f;
    [SerializeField] private float caosPointsDown = 0.1f;
    [SerializeField] private float timeToChangePoints = 2f;
    [SerializeField] private int timeRecovering = 0;

    private FMOD.Studio.EventInstance recoveringPower;


    private float restTimeToChange = 0;


    private void Start()
    {
        recoveringPower = FMODUnity.RuntimeManager.CreateInstance(RECOVERING_POWER_PATH);

        restTimeToChange = timeToChangePoints;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timeRecovering = 0;
            recoveringPower.start();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            recoveringPower.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        timeRecovering++;
        restTimeToChange -= Time.deltaTime;

        if (restTimeToChange <= 0)
        {
            PlayerController pc = other.gameObject.GetComponent<PlayerController>();
            pc.AddAbducaoPoints(abducaoPointsRestore);
            pc.PlayAbducaoAnimation();

            CaosManager.Instance.SubCaosPoints(caosPointsDown);

            recoveringPower.setParameterByName(RECOVERING_POWER_PARAM, timeRecovering);

            restTimeToChange = timeToChangePoints;
        }
    }


}
