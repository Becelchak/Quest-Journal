using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> pagesSound = new List<AudioClip>();
    [SerializeField]
    private List<AudioClip> pineSound = new List<AudioClip>();
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    public void SwapPage()
    {
        var rnd = Random.Range(0, pagesSound.Count);
        audioSource.Stop();
        audioSource.clip = pagesSound[rnd];
        audioSource.Play();
    }

    public void PineSound()
    {
        var rnd = Random.Range(0, pineSound.Count);
        audioSource.Stop();
        audioSource.clip = pineSound[rnd];
        audioSource.Play();
    }
}
