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

    public void Hit(float damage, PlayerController targetPlayer)
    {
        health -= damage;
        if (health <= 0.0f) OnDestroyed(targetPlayer);
    }

    private void Start()
    {
        wasDestroyed = false;
    }

    private void Update()
    {
        if (health <= 0.0f) OnDestroyed(null);
    }

    private void OnDestroyed(PlayerController targetPlayer)
    {
        if (wasDestroyed) return;

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
        if (score != 0.0f && targetPlayer != null)
        {
            var counter = targetPlayer.GetComponent<DistanceCounter>();
            counter.PostScore(score, transform);
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
