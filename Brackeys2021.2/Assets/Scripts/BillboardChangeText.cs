using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardChangeText : MonoBehaviour
{
    [SerializeField] private bool changeText = true;
    [SerializeField] private GameObject hide;
    [SerializeField] private GameObject zoom;

    private void FixedUpdate()
    {
        bool chaosPointMax = CaosManager.Instance.GetCaosPoints() == 1;

        hide.SetActive(chaosPointMax);
        zoom.SetActive(!chaosPointMax);
    }
}
