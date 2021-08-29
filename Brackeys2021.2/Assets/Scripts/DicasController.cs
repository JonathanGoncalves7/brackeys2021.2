using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DicasController : MonoBehaviour
{
    [SerializeField] GameObject tipAWSD;
    [SerializeField] GameObject tipSpace;
    [SerializeField] GameObject tipAwesome;

    void Update()
    {
        if(tipAWSD.activeInHierarchy && (Mathf.Abs(Input.GetAxis("Vertical")) > 0f || Mathf.Abs(Input.GetAxis("Horizontal")) > 0))
        {
            tipAWSD.SetActive(false);
            tipSpace.SetActive(true);
            return;
        }

        if(tipSpace.activeInHierarchy && Input.GetButton("Jump"))
        {
            tipSpace.SetActive(false);
            tipAwesome.SetActive(true);
        }

    }

}
