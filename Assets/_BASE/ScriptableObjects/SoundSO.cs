using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "SO/SoundSO", fileName = "SoundSO")]
public class SoundSO : ScriptableObject
{
    public SoundData[] soundDatas;

    public AudioClip GetSound(TypeSound type)
    {
        for (int i = 0; i < soundDatas.Length; i++)
        {
            if (soundDatas[i].type == type)
            {
                return soundDatas[i].clip;
            }
        }
        return null;
    }
}

[System.Serializable]
public class SoundData
{
    public TypeSound type;
    public AudioClip clip;
}

