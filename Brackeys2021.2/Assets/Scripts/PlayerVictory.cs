using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVictory : MonoBehaviour
{
    [SerializeField] private GameObject victoryPanel;

    public void VictoryGame()
    {
        victoryPanel.SetActive(true);
    }
}
