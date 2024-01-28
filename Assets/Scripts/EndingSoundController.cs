using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingSoundController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip laughClown;
    public void ClownSound()
    {
        if (audioSource.isPlaying == false)
        {
            audioSource.PlayOneShot(laughClown, 0.2f);
        }
    }
}
