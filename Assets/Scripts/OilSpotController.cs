using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilSpotController : MonoBehaviour
{
    public float slowdownFactor = 0.5f;
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponentInParent<PlayerController>();
        if (player == null) return;

        player.velocityModifier = slowdownFactor;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponentInParent<PlayerController>();
        if (player == null) return;

        player.velocityModifier = 1.0f;
    }
}
