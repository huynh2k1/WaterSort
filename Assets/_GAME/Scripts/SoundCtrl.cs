using System.Collections.Generic;
using UnityEngine;

public class SoundCtrl : MonoBehaviour
{
    public static SoundCtrl I;
    public SoundSO dataSound;

    [Header("Music")]
    public AudioSource music_source;

    [Header("Sound")]
    public AudioSource[] sound_sources;
    private Queue<AudioSource> queue_sources;

    private void Awake()
    {
        I = this;
    }

    private void Start()
    {
        queue_sources = new Queue<AudioSource>(sound_sources);
        Init();
    }

    public void Init()
    {
        UpdateMute();
        PlayMusic();
    }

    public void UpdateMute()
    {
        CheckMusic();
    }

    public void CheckMusic()
    {
        music_source.mute = !PrefData.Music;
        if(PrefData.Music == false)
        {
            music_source.Stop();
        }
        else
        {
            music_source.Play();
        }
    }
 

    public void Vibrate()
    {
        if (PrefData.Vibrate)
        {
            Handheld.Vibrate();
        }
    }

    public void PlaySound(TypeSound type, float volume = 0.7f)
    {
        if (PrefData.Sound == false)
            return;
        var source = queue_sources.Dequeue();
        if (source == null)
            return;
        AudioClip clip = dataSound.GetSound(type);
        source.volume = volume;
        source.PlayOneShot(clip);
        queue_sources.Enqueue(source);
    }

    public void PlayMusic(float volume = 0.7f)
    {
        music_source.clip = dataSound.GetSound(TypeSound.MUSIC);
        music_source.volume = volume;
        music_source.Play();
    }

    public void StopMusic()
    {
        music_source.Stop();
    }
}
public enum TypeSound
{
    MUSIC,
    CLICK,
    WIN,
    LOSE,
    BOTTLECOMPLETE
}
