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
    private TMP_Text hintPanelText;

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

        if(mission == 1)
        {
            hintPanel.SetActive(true);
            hintPanelText.text = Dicas_M1[hintNumber];

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

        yield return new WaitForSeconds(4f);
        hintPanel.SetActive(false);

    }

    public void CallShowHint()
    {
        StartCoroutine(ShowHint(Statics.mission, Statics.hintNumber));
    }

}