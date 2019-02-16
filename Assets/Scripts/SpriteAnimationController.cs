using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimationController : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[0];

    public float frameTime = 0.1f;
    public float lastFrameTime = 0.0f;

    public int currentFrame = 0;

    // Start is called before the first frame update
    void Start()
    {
        lastFrameTime = Time.time;
    }

    // Update is called once per frame
    void OnEnable()
    {
        lastFrameTime = Time.time;
    }

    private void Update()
    {
        while (Time.time > frameTime + lastFrameTime)
        {
            currentFrame = (currentFrame + 1) % sprites.Length;
            lastFrameTime += frameTime;
        }
        GetComponent<SpriteRenderer>().sprite = sprites[currentFrame];
    }
}
