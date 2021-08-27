using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSortingLayer : MonoBehaviour
{
    [SerializeField] MeshRenderer rend;
    [SerializeField] string sortingLayerName;
    [SerializeField] int orderInLayer;

    void SetSortingLayer()
    {
        if (sortingLayerName != string.Empty)
        {
            rend.sortingLayerName = sortingLayerName;
            rend.sortingOrder = orderInLayer;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        SetSortingLayer();
    }
}
