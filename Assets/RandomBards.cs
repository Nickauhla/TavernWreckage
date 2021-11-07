using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class RandomBards : MonoBehaviour
{
    [SerializeField] private AudioSource m_audioSource;

    [SerializeField] private List<AudioClip> m_clips;
    // Start is called before the first frame update
    void Awake()
    {
        RandomizeMusic();
    }

    private void RandomizeMusic()
    {
        AudioClip audioClip = SelectRandomClip();
        m_audioSource.PlayOneShot(audioClip);
        Invoke(nameof(RandomizeMusic), audioClip.length+1);
    }
    
    private AudioClip SelectRandomClip()
    {
        Random rand = new Random();
        int randomInt = rand.Next(m_clips.Count);
        return m_clips[randomInt];
    }
}
