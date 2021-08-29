using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerAudio : MonoBehaviour
{
    [SerializeField] const string AUDIO_PATH = "event:/SFX/Gameplay/ship_moves";

    [Header("Ship Moves")]
    [SerializeField] private bool isPlaying;
    private FMOD.Studio.EventInstance audioLoop;

    private Rigidbody rb;

    void Start()
    {
        audioLoop = FMODUnity.RuntimeManager.CreateInstance(AUDIO_PATH);
        rb = GetComponent<Rigidbody>();

        isPlaying = false;
    }

    private void Update()
    {
        PlayShipMoves();
    }

    void PlayShipMoves()
    {
        if (rb.velocity.magnitude > 0)
        {
            if (!isPlaying)
            {
                audioLoop.start();
                isPlaying = true;
            }
        }
        else
        {
            if (isPlaying)
            {

                audioLoop.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                isPlaying = false;
            }
        }
    }

    private void OnDestroy()
    {
        audioLoop.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
