using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HintPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject hintPanel;

    [SerializeField]
    private GameObject checklistPanel;

    [SerializeField]
    private TMP_Text[] DicasChecklist;

    [SerializeField]
    private TMP_Text hintPanelText;

    [SerializeField]
    private float timeMessage = 8f;

    [SerializeField] private GameObject prop_final;
    [SerializeField] private GameObject wayPoints;

    [SerializeField]
    private string[] Dicas_M1;
    [SerializeField]
    private string[] Dicas_M2;
    [SerializeField]
    private string[] Dicas_M3;
    [SerializeField]
    private string[] Dicas_M4;





    private void Start()
    {
        StartCoroutine(ShowHint(Statics.mission, Statics.hintNumber));
    }

    public IEnumerator ShowHint(int mission, int hintNumber)
    {

        if (mission == 1)
        {
            hintPanel.SetActive(true);
            hintPanelText.text = Dicas_M1[hintNumber];
            DicasChecklist[hintNumber].text = Dicas_M1[hintNumber];

        }
        if (mission == 2)
        {
            hintPanel.SetActive(true);
            hintPanelText.text = Dicas_M2[hintNumber];

        }
        if (mission == 3)
        {
            hintPanel.SetActive(true);
            hintPanelText.text = Dicas_M3[hintNumber];

        }
        if (mission == 4)
        {
            hintPanel.SetActive(true);
            hintPanelText.text = Dicas_M4[hintNumber];

        }

        yield return new WaitForSeconds(timeMessage);
        hintPanel.SetActive(false);

    }

    public void CallShowHint()
    {
        if (Statics.hintNumber == (Dicas_M1.Length - 1))
        {
            GameObject final_waypoints = wayPoints.GetComponent<Waypoints>().GetRandom();
            GameObject newAnimal = Instantiate(prop_final, final_waypoints.transform.position, Quaternion.identity);
            newAnimal.GetComponent<WaypointWalker>().waypoints = wayPoints.GetComponent<Waypoints>();
        }

        StartCoroutine(ShowHint(Statics.mission, Statics.hintNumber));
    }

}
