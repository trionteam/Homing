using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayTheSound();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayTheSound();
    }


    private void PlayTheSound()
    { 
        var audioSource = GetComponentInParent<AudioSource>();
        if (audioSource != null)
        {
            if (!audioSource.isPlaying) audioSource.Play();
        }
    }
}
