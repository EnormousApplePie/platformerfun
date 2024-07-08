using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioSource sound_object;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    //-----------------------------------------------------------------------------------------------
    /// <summary>
    /// Play a sound clip at a specific location
    /// </summary>
    public void play_sound_clip(AudioClip audio_clip, Transform spawn_transform, float volume)
    {
        AudioSource audio_source = Instantiate(sound_object, spawn_transform.position, Quaternion.identity);

        audio_source.clip = audio_clip;
        audio_source.volume = volume;

        audio_source.Play();

        float clip_length = audio_clip.length;

        Destroy(audio_source.gameObject, clip_length);
    }
    //-----------------------------------------------------------------------------------------------
    /// <summary>
    /// Play a random sound clip from a list of sound clips at a specific location
    /// </summary>
    public void play_random_sound_clip(AudioClip[] audio_clips, Transform spawn_transform, float volume)
    {
        AudioSource audio_source = Instantiate(sound_object, spawn_transform.position, Quaternion.identity);

        int random_clip = Random.Range(0, audio_clips.Length);
        audio_source.clip = audio_clips[random_clip];
        audio_source.volume = volume;

        audio_source.Play();

        float clip_length = audio_clips[random_clip].length;

        Destroy(audio_source.gameObject, clip_length);
    }
    //-----------------------------------------------------------------------------------------------
}
