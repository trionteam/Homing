using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public PlayerController player = null;

    public ExplosionController explosionPrefab = null;

    public float velocity = 1.0f;

    public float maxTurnPerFrame = 2.0f;

    public float minExplodeOffset = 2.0f;
    float MinExplodeX { get { return ScreenBoundsController.Instance.leftBound + minExplodeOffset; } }

    public float zPosition = -8.0f;

    public float damage = 1.0f;

    public Transform warning = null;

    public float warningOffset = 0.1f;

    float LeftBound { get { return ScreenBoundsController.Instance.leftBound + warningOffset; } }
    float BottomBound { get { return ScreenBoundsController.Instance.bottomBound + warningOffset; } }
    float TopBound { get { return ScreenBoundsController.Instance.topBound - warningOffset; } }

    // Start is called before the first frame update
    void Start()
    {
        var rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.right;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var scrollingController = ScrollingController.GetInstance();

        var rb = GetComponent<Rigidbody2D>();
        var playerRb = player.GetComponent<Rigidbody2D>();
        var direction = playerRb.position - rb.position;
        // TODO(ondrasej): Limit the difference in angles (limit angle delta per frame?)
        var rotation = Vector2.SignedAngle(Vector2.right, direction);
        var oldRotation = rb.rotation;
        rb.velocity = direction.normalized * velocity - new Vector2(scrollingController.scrollingSpeed, 0.0f);
        rb.MoveRotation(rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var health = collision.GetComponentInParent<HealthController>();

        if (collision.gameObject.layer == LayerMask.NameToLayer("ScreenBounds"))
        {
            // The missile entered the screen. Remove the warning.
            if (warning != null) Destroy(warning.gameObject);
            warning = null;
        }

        // The collider is in a child game object of the player.
        var player = collision.GetComponentInParent<PlayerController>();
        if (player != null)
        {
            Explode(health);
            return;
        }

        //var obstacle = collision.GetComponentInParent<ObstacleController>();
        if (collision.gameObject.layer == LayerMask.NameToLayer("MissileTarget"))
        {
            if (transform.position.x >= MinExplodeX) Explode(health);
        }

    }

    private void Update()
    {
        if (warning != null)
        {
            var playerPos = player.transform.position;
            var pos = transform.position;
            var dir = (pos - playerPos);
            dir.z = 0.0f;
            dir.Normalize();
            var toScreenLeftDist = Mathf.Infinity;
            var toScreenUpDist = Mathf.Infinity;
            var toScreenDownDist = Mathf.Infinity;
            if (dir.x < 0.0f)
            {
                toScreenLeftDist = (LeftBound - playerPos.x) / dir.x;
            }
            if (dir.y > 0.0f)
            {
                toScreenUpDist = (TopBound - playerPos.y) / dir.y;
            }
            if (dir.y < 0.0f)
            {
                toScreenDownDist = (BottomBound - playerPos.y) / dir.y;
            }
            var min = Mathf.Min(toScreenLeftDist, Mathf.Min(toScreenUpDist, toScreenDownDist));
            dir = min * dir;
            warning.position = playerPos + dir;
        }
    }

    void Explode(HealthController hitObject)
    {
        var position = transform.position;
        position.z = zPosition;
        Instantiate(explosionPrefab, position, transform.rotation);
        Destroy(gameObject);

        if (hitObject)
        {
            hitObject.health -= damage;
        }
    }
}
