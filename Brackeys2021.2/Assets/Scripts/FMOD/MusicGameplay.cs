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
        music.setParameterByName(NO_CHAOS_DRUMS_PARAM, play);
        music.setParameterByName(NO_CHAOS_BASS_PARAM, play);
        music.setParameterByName(NO_CHAOS_HARMONICA_PARAM, play);
    }

    void ChaosMusics(int play)
    {
        music.setParameterByName(CHAOS_DRUMS_PARAM, play);
        music.setParameterByName(CHAOS_BASS_PARAM, play);
        music.setParameterByName(CHAOS_HARMONICA_PARAM, play);
        music.setParameterByName(CHAOS_BANJO_PARAM, play);
        music.setParameterByName(CHAOS_PERC_PARAM, play);

        if (play == START && (CaosManager.Instance.GetCaosPoints() >= 0.9f))
        {
            music.setParameterByName(CHAOS_BAR_VERY_HIHG_PARAM, START);
        }
        else
        {
            music.setParameterByName(CHAOS_BAR_VERY_HIHG_PARAM, STOP);
        }
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

}
