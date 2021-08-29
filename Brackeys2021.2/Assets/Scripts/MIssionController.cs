using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class MIssionController : MonoBehaviour
{
    [SerializeField]
    private bool isRightTarget;

    private HintPanel hintPanel;

    private void Start()
    {
        hintPanel = FindObjectOfType<HintPanel>().GetComponent<HintPanel>();
    }

    void hintChoose()
    {
       if(Regex.IsMatch(transform.name, "Cow"))
       {
            Statics.hintNumber = 1;
       }

        if (Regex.IsMatch(transform.name, "Horse"))
        {
            Statics.hintNumber = 2;
        }

        if (Regex.IsMatch(transform.name, "Duck"))
        {
            Statics.hintNumber = 3;
        }

        if (Regex.IsMatch(transform.name, "Pig"))
        {
            Statics.hintNumber = 4;
        }

        if (Regex.IsMatch(transform.name, "Chicken"))
        {
            Statics.hintNumber = 5;
        }

        if (Regex.IsMatch(transform.name, "Crow"))
        {
            Statics.hintNumber = 6;
        }

        if (Regex.IsMatch(transform.name, "Farmer"))
        {
            Statics.hintNumber = 7;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (isRightTarget)
            { 
                Statics.mission++;

            }
            else if (!isRightTarget)
            {
                hintChoose();
                Debug.Log(Statics.hintNumber + "  " + Statics.nextHintNumber);
                if(Statics.hintNumber == Statics.nextHintNumber)
                {
                    hintPanel.CallShowHint();
                    Statics.nextHintNumber++;
                }
                
            }
        }
    }
}
