using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(PlayerController))]
public class PlayerAudio : MonoBehaviour
{
    const string SHIP_MOVES_PATH = "event:/SFX/Gameplay/ship_moves";
    const string ABDUCTION_PATH = "event:/SFX/Gameplay/Abduction";
    const string NO_POWER_PATH = "event:/SFX/HUD/Power Bar/no_power";

    [Header("Ship Moves")]
    [SerializeField] private bool isPlayingShipMoves;
    private FMOD.Studio.EventInstance shipMoves;

    [Header("Abduction")]
    [SerializeField] private bool isPlayingAbduction;
    private FMOD.Studio.EventInstance abduction;

    [Header("Abduction")]
    [SerializeField] private bool isPlayingNoPower;
    private FMOD.Studio.EventInstance noPower;

    private Rigidbody playerRigibody;
    private PlayerController playerController;

    void Start()
    {
        shipMoves = FMODUnity.RuntimeManager.CreateInstance(SHIP_MOVES_PATH);
        abduction = FMODUnity.RuntimeManager.CreateInstance(ABDUCTION_PATH);
        noPower = FMODUnity.RuntimeManager.CreateInstance(NO_POWER_PATH);


        playerRigibody = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();

        isPlayingShipMoves = false;
        isPlayingAbduction = false;
    }

    private void Update()
    {
        PlayShipMoves();
        PlayAbduction();
        PlayNoPower();
    }

    void PlayShipMoves()
    {
        if (playerRigibody.velocity.magnitude > 0)
        {
            if (!isPlayingShipMoves)
            {
                shipMoves.start();
                isPlayingShipMoves = true;
            }
        }
        else
        {
            if (isPlayingShipMoves)
            {

                shipMoves.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                isPlayingShipMoves = false;
            }
        }
    }

    void PlayAbduction()
    {
        if (Input.GetKey(KeyCode.Space) && playerController.PermiteAbduzir())
        {
            if (!isPlayingAbduction)
            {
                abduction.start();
                isPlayingAbduction = true;
            }
        }
        else
        {
            if (isPlayingAbduction)
            {

                abduction.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                isPlayingAbduction = false;
            }
        }
    }

    void PlayNoPower()
    {
        if (Input.GetKey(KeyCode.Space) && !playerController.PermiteAbduzir() && !IsPlaying(noPower))
        {
            noPower.start();
        }
    }

    public static bool IsPlaying(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE state;
        instance.getPlaybackState(out state);
        return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }

    private void OnDestroy()
    {
        shipMoves.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        abduction.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        noPower.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
