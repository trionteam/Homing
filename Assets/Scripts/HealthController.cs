using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public float health = 1.0f;

    public float score = 0.0f;

    public bool disableColliders = true;

    public bool wasDestroyed = false;

    public Sprite destroyedSprite;

    private void Start()
    {
        wasDestroyed = false;
    }

    private void Update()
    {
        if (health <= 0.0f)
        {
            if (destroyedSprite != null)
            {
                var spriteRenderer = GetComponentInChildren<SpriteRenderer>();
                spriteRenderer.sprite = destroyedSprite;
            }
            if (disableColliders)
            {
                foreach (var collider in GetComponentsInChildren<Collider2D>())
                {
                    collider.enabled = false;
                }
            }
            if (score != 0.0f && !wasDestroyed)
            {
                DistanceCounter.Instance.PostScore(score, transform);
            }
            if (!wasDestroyed)
            {
                var audioSource = GetComponentInParent<AudioSource>();
                if (audioSource != null)
                {
                    if (!audioSource.isPlaying) audioSource.Play();
                }
            }
            wasDestroyed = true;
        }
    }
}
