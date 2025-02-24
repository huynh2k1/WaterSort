using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefData
{
    public static int CurLevel
    {
        get => PlayerPrefs.GetInt("Cur_Level", 0);
        set => PlayerPrefs.SetInt("Cur_Level", value);
    }
    #region SOUND, MUSIC AND VIBRATION
    public static bool Sound
    {
        get
        {
            return PlayerPrefs.GetInt("Sound", 0) == 0 ? true : false;
        }
        set
        {
            PlayerPrefs.SetInt("Sound", value ? 0 : 1);
        }
    }

    public static bool Music
    {
        get
        {
            return PlayerPrefs.GetInt("Music", 0) == 0 ? true : false;
        }
        set
        {
            PlayerPrefs.SetInt("Music", value ? 0 : 1);
        }
    }

    public static bool Vibrate
    {
        get
        {
            return PlayerPrefs.GetInt("Vibrate", 0) == 0 ? true : false;
        }
        set
        {
            PlayerPrefs.SetInt("Vibrate", value ? 0 : 1);
        }
    }

    #endregion
}
