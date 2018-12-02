using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioClip ButtonPressSound;
    public AudioClip HitSound;
    public AudioClip GoldenDeathSound;
    public AudioClip LineDrawingSound;

    public List<AudioClip> DeathVoices;
    public List<AudioClip> HappyVoices;
    public List<AudioClip> ExplosionSounds;

    private List<AudioSource> Sources = new List<AudioSource>();
    private const int MaxSources = 10;
    private int m_currentSourceIndex = 0;

    // Use this for initialization
    void Start ()
    {
		for(int i = 0; i < MaxSources; i++)
        {
            Sources.Add(gameObject.AddComponent<AudioSource>());
        }
	}

    /// <summary>
    /// Play a death voice.
    /// </summary>
    public void PlayDeathVoice()
    {
        int index = Random.Range(0, DeathVoices.Count);
        PlaySound(DeathVoices[index]);
    }

    /// <summary>
    /// Play a happy voice.
    /// </summary>
    public void PlayHappyVoice()
    {
        int index = Random.Range(0, HappyVoices.Count);
        PlaySound(HappyVoices[index]);
    }

    /// <summary>
    /// Play an explosion.
    /// </summary>
    public void PlayExplosionSound()
    {
        int index = Random.Range(0, ExplosionSounds.Count);
        PlaySound(ExplosionSounds[index]);
    }

    /// <summary>
    /// Play an audio clip on the pool of sources.
    /// </summary>
    public void PlaySound(AudioClip clip)
    {
        // Get the next source.
        m_currentSourceIndex++;
        if(m_currentSourceIndex >= MaxSources)
        {
            m_currentSourceIndex = 0;
        }

        Sources[m_currentSourceIndex].Stop();
        Sources[m_currentSourceIndex].clip = clip;
        Sources[m_currentSourceIndex].Play();
    }
}
