using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreUFOPoints : MonoBehaviour
{
    [SerializeField] private float abducaoPointsRestore = 0.1f;
    [SerializeField] private float caosPointsDown = 0.1f;
    [SerializeField] private float timeToChangePoints = 2f;

    private float restTimeToChange = 0;


    private void Start()
    {
        restTimeToChange = timeToChangePoints;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        restTimeToChange -= Time.deltaTime;

        if (restTimeToChange <= 0)
        {
            PlayerController pc = other.gameObject.GetComponent<PlayerController>();
            pc.AddAbducaoPoints(abducaoPointsRestore);
            pc.PlayAbducaoAnimation();

            CaosManager.Instance.SubCaosPoints(caosPointsDown);

            restTimeToChange = timeToChangePoints;
        }
    }
}
