using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGamerOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private float timeToGameOver = 10f;
    [SerializeField] private float restTimeToGameOver = 0;


    // Start is called before the first frame update
    void Start()
    {
        restTimeToGameOver = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (CaosManager.Instance.GetCaosPoints() == 1)
        {
            restTimeToGameOver += Time.deltaTime;

            if (restTimeToGameOver >= timeToGameOver)
            {
                gameOverPanel.SetActive(true);
            }
        }
        else
        {
            restTimeToGameOver = 0;
        }
    }
}
