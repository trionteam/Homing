using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
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
    void Update()
    {
        while (Time.time > lastFrameTime + frameTime)
        {
            currentFrame++;
            lastFrameTime += frameTime;
        }
        if (currentFrame < sprites.Length)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[currentFrame];
        }
        else
        {
            Destroy(gameObject);
        }
        var scrollingController = ScrollingController.GetInstance();
        var position = transform.position;
        position.x -= scrollingController.scrollingSpeed * Time.deltaTime;
        transform.position = position;
    }
}
