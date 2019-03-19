using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBoundsController : MonoBehaviour
{
    public static ScreenBoundsController Instance { get; private set; }

    public Camera mainCamera = null;

    public float leftBound = -8.0f;
    public float rightBound = 8.0f;
    public float topBound = 5.0f;
    public float bottomBound = -5.0f;

    public float screenWidth = 16.0f;
    public float screenHeight = 10.0f;

    public float boundPadding = 0.15f;

    private void OnEnable()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateBounds();
    }

    void Update()
    {
        UpdateBounds();
        var collider = GetComponent<BoxCollider2D>();
        collider.size = new Vector2(2.0f * (rightBound - boundPadding), 2.0f * (topBound - boundPadding));
    }

    void UpdateBounds()
    {
        topBound = mainCamera.orthographicSize;
        bottomBound = -topBound;
        
        leftBound = -mainCamera.orthographicSize * mainCamera.aspect;
        rightBound = -leftBound;

        screenHeight = 2.0f * mainCamera.orthographicSize;
        screenWidth = mainCamera.aspect * screenHeight;
    }
}
