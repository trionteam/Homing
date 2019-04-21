using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : MonoBehaviour
{
    public float explosionZPosition = -8.0f;

    public float damage = 1.0f;

    public GameObject explosionPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();
        if (player == null || !player.IsAlive || !player.isActive) return;

        // The player drove on the mine -> explode.
        var position = transform.position;
        position.z = explosionZPosition;
        Instantiate(explosionPrefab, position, Quaternion.identity);
        Destroy(gameObject);

        player.GetComponent<HealthController>().Hit(damage, null);
    }
}
