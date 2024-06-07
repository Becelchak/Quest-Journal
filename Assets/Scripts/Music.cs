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

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            var rnd = Random.Range(0,playList.Count);
            var audio = playList[rnd];
            audioSource.clip = audio;
            audioSource.Play();
        }
    }
}
