using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> playList = new List<AudioClip>();
    private AudioListener listener;
    private AudioSource audioSource;
    private bool isAudioOn = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (!isAudioOn)
            audioSource.enabled = false;
    }

    void Update()
    {
        if (!isAudioOn)
            audioSource.volume = 0;
        else
        {
            audioSource.volume = 0.10f;
        }

        if (!audioSource.isPlaying)
        {
            var rnd = Random.Range(0,playList.Count);
            var audio = playList[rnd];
            audioSource.clip = audio;
            audioSource.Play();
        }
    }
    public void SwitchAudio()
    {
        isAudioOn = !isAudioOn;
    }
}
