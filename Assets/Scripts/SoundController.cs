using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip gameOverSound;
    public AudioClip damageSound;

    static public SoundController soundController;

    void Awake()
    {
        soundController = this;
    }
    void Start()
    {
        audioSource.Play();
    }

    public void GameOverSound()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(gameOverSound, 0.2f);
    }

    public void DamageSound()
    {
        audioSource.PlayOneShot(damageSound, 0.2f);
    }
}
