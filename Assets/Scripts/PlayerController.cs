using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocityScaling = 1.0f;

    public float verticalMargin = 1.0f;

    float MinX { get { return ScreenBoundsController.Instance.leftBound + verticalMargin; } }
    float MaxX { get { return ScreenBoundsController.Instance.rightBound - verticalMargin; } }

    public float minY = -4.5f;
    public float maxY = 4.5f;

    public float baseTilt = -5.0f;

    public float maxTilt = 45.0f;
    public float minTilt = -45.0f;

    public float tiltScaling = 15.0f;

    public float velocityModifier = 1.0f;

    public float basePitch = 0.4f;
    public float pitchRatio = 0.2f;

    public float wheelRotationScaling = 10.0f;

    public float deathTime = 0.0f;
    public float deathDelay = 5.0f;

    public Sprite[] states = new Sprite[0];
    public SpriteRenderer sprite = null;

    public string horizontalAxisName = null;
    public string verticalAxisName = null;
    public string controlSuffix = "";

    public Transform wheel;

    public bool isLastPlayer = false;

    public bool isActive = false;

    void Start()
    {
        if (string.IsNullOrEmpty(horizontalAxisName)) horizontalAxisName = "Horizontal" + controlSuffix;
        if (string.IsNullOrEmpty(verticalAxisName)) verticalAxisName = "Vertical" + controlSuffix;
    }

    void FixedUpdate()
    {
        Rigidbody2D rb = GetComponentInChildren<Rigidbody2D>();
        var scrollingController = ScrollingController.GetInstance();

        var scrollVelocity = new Vector2(-scrollingController.scrollingSpeed, 0.0f);

        bool alive = IsAlive;
        float horizontalAxis = alive ? Input.GetAxis(horizontalAxisName) : 0.0f;
        float verticalAxis = alive ? Input.GetAxis(verticalAxisName) : 0.0f;
        var accelerationMagnitude = Mathf.Sqrt(horizontalAxis * horizontalAxis + verticalAxis * verticalAxis);

        // Tilt the player according to the velocity in the previous frame.
        var angle = Mathf.Max(minTilt, Mathf.Min(maxTilt, baseTilt - tiltScaling * horizontalAxis));
        rb.rotation = Mathf.LerpAngle(rb.rotation, angle, 0.1f);

        var playerVelocity = Vector2.zero;
        if (alive)
        {
            var velocityX = velocityScaling * horizontalAxis;
            if (isActive) velocityX += scrollingController.scrollingSpeed;
            var velocityY = velocityScaling * verticalAxis;
            playerVelocity = velocityModifier * new Vector2(velocityX, velocityY);
        }
        var targetVelocity = playerVelocity + scrollVelocity;
        rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, 0.3f);

        var position = rb.position;
        if (position.y < minY) position.y = minY;
        if (position.y > maxY) position.y = maxY;

        if (position.x > MaxX) position.x = MaxX;
        if (isActive && (alive || isLastPlayer) && position.x < MinX) position.x = MinX;
        rb.position = position;

        transform.position = new Vector3(position.x, position.y, transform.position.z);

        // At the beginning of the game, allow activating the second player before they
        // get out of the screen.
        if (position.x >= MinX)
        {
            if (accelerationMagnitude > 0.0f) isActive = true;
        }

        // Update the pitch.
        var audio = GetComponent<AudioSource>();
        var targetPitch = basePitch + accelerationMagnitude * pitchRatio;
        audio.pitch = Mathf.Lerp(audio.pitch, targetPitch, 0.1f);
    }

    private Vector3 oldPosition;

    private void Update()
    {
        var scrollingController = ScrollingController.GetInstance();
        var delta = transform.position.x - oldPosition.x + scrollingController.scrollingSpeed * Time.deltaTime;
        var wheelRotation = Quaternion.Euler(0.0f, 0.0f, -delta * wheelRotationScaling);
        wheel.rotation = (wheel.rotation * wheelRotation).normalized;

        var audio = GetComponent<AudioSource>();
        audio.panStereo = transform.position.x / 10.0f;
        oldPosition = transform.position;

        var alive = IsAlive;
        if (!alive)
        {
            // Enable the animator. This also switches the player to the wrecked state.
            GetComponentInChildren<SpriteAnimationController>().enabled = true;
        }
        else
        {
            var health = GetComponent<HealthController>();
            var state = Mathf.Max(0, Mathf.Min((int)health.health - 1, states.Length - 1));
            sprite.sprite = states[state];
        }

        if (alive && isActive)
        {
            int alivePlayers = 0;
            foreach (var playerObject in GameObject.FindGameObjectsWithTag(Tags.Player))
            {
                var player = playerObject.GetComponent<PlayerController>();
                if (player.IsAlive && player.isActive) alivePlayers++;
            }
            isLastPlayer = alivePlayers == 1;
        }
    }

    public bool IsTarget
    {
        get
        {
            return isActive && IsAlive;
        }
    }

    public bool IsAlive
    {
        get
        {
            var health = GetComponent<HealthController>();
            return health.health > 0.0f;
        }
    }

    public static int NumPlayersAlive
    {
        get
        {
            int numPlayersAlive = 0;
            foreach (var playerObject in GameObject.FindGameObjectsWithTag(Tags.Player))
            {
                var player = playerObject.GetComponent<PlayerController>();
                if (player.IsAlive && player.isActive) numPlayersAlive++;
            }
            return numPlayersAlive;
        }
    }
}
