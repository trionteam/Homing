using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScrolling : MonoBehaviour
{
    public float minX = -20.0f;
    public float moveRight = 30.0f;

    // Update is called once per frame
    void Update()
    {
        var scrollingController = ScrollingController.GetInstance();

        var position = transform.position;
        position.x -= scrollingController.scrollingSpeed * Time.deltaTime;
        if (position.x < minX) position.x += moveRight;
        transform.position = position;
    }
}
