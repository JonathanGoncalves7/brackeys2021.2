using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VcaController : MonoBehaviour
{
    private FMOD.Studio.VCA _vcaController;    
    private Slider _slider;
    
    public string vcaName;

    void Start()
    {
        _vcaController = FMODUnity.RuntimeManager.GetVCA("vca:/" + vcaName);
        _slider = GetComponent<Slider>();
    }

    public void SetVolume(float volume){
        _vcaController.setVolume(volume); 
    }
}
