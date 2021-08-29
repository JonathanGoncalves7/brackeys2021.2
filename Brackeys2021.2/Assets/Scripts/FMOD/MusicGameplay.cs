using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class MusicGameplay : MonoBehaviour
{
    const int START = 1;
    const int STOP = 0;

    const string MUSIC_PATH = "event:/MSC/music";

    const string FIDDLE_PARAM = "Fiddle";

    const string NO_CHAOS_DRUMS_PARAM = "No Chaos_Drums";
    const string NO_CHAOS_BASS_PARAM = "No Chaos_Bass";
    const string NO_CHAOS_HARMONICA_PARAM = "No_Chaos Harmonica";
    const string NO_CHAOS_CHAOS_PARAM = "No Chaos_Banjo";

    const string CHAOS_DRUMS_PARAM = "Chaos_Drums";
    const string CHAOS_BASS_PARAM = "Chaos_Bass";
    const string CHAOS_HARMONICA_PARAM = "Chaos_Harmonica";
    const string CHAOS_BANJO_PARAM = "Chaos_Banjo";
    const string CHAOS_PERC_PARAM = "Chaos_Perc";
    const string CHAOS_BAR_VERY_HIHG_PARAM = "Chaos_Bar_Very_High";


    [SerializeField] [Range(0, 1)] private float chaosDrums = 0.1f;
    [SerializeField] [Range(0, 1)] private float chaosBass = 0.2f;
    [SerializeField] [Range(0, 1)] private float chaosHarmonica = 0.3f;
    [SerializeField] [Range(0, 1)] private float chaosBanjo = 0.4f;
    [SerializeField] [Range(0, 1)] private float chaosPerc = 0.5f;
    [SerializeField] [Range(0, 1)] private float chaosBarVeryHigh = 0.9f;

    private FMOD.Studio.EventInstance music;

    void Start()
    {
        music = FMODUnity.RuntimeManager.CreateInstance(MUSIC_PATH);

        music.setParameterByName(FIDDLE_PARAM, START);

        NoChaosMusics(START);
        ChaosMusics(STOP);

        music.start();
    }

    void NoChaosMusics(int play)
    {
        float currentChaos = CaosManager.Instance.GetCaosPoints();

        music.setParameterByName(NO_CHAOS_DRUMS_PARAM, PlayingLow(play, currentChaos, chaosDrums));
        music.setParameterByName(NO_CHAOS_BASS_PARAM, PlayingLow(play, currentChaos, chaosBass));
        music.setParameterByName(NO_CHAOS_HARMONICA_PARAM, PlayingLow(play, currentChaos, chaosHarmonica));
    }

    void ChaosMusics(int play)
    {
        float currentChaos = CaosManager.Instance.GetCaosPoints();

        music.setParameterByName(CHAOS_DRUMS_PARAM, PlayingHighOrEqual(play, currentChaos, chaosDrums));
        music.setParameterByName(CHAOS_BASS_PARAM, PlayingHighOrEqual(play, currentChaos, chaosBass));
        music.setParameterByName(CHAOS_HARMONICA_PARAM, PlayingHighOrEqual(play, currentChaos, chaosHarmonica));
        music.setParameterByName(CHAOS_BANJO_PARAM, PlayingHighOrEqual(play, currentChaos, chaosBanjo));
        music.setParameterByName(CHAOS_PERC_PARAM, PlayingHighOrEqual(play, currentChaos, chaosPerc));
        music.setParameterByName(CHAOS_BAR_VERY_HIHG_PARAM, PlayingHighOrEqual(play, currentChaos, chaosBarVeryHigh));
    }

    private void Update()
    {
        if (CaosManager.Instance.GetCaosPoints() <= 0)
        {
            NoChaosMusics(START);
            ChaosMusics(STOP);
        }
        else
        {
            NoChaosMusics(STOP);
            ChaosMusics(START);
        }
    }

    private void OnDestroy()
    {
        music.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    int PlayingHighOrEqual(int play, float chaos, float paramValue)
    {
        if (play == START && chaos >= paramValue)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    int PlayingLow(int play, float chaos, float paramValue)
    {
        if (play == START && chaos < paramValue)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

}
