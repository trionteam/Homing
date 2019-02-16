using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public float scrollingScaling = 1.0f;

    private void FixedUpdate()
    {
        var scrollingController = ScrollingController.GetInstance();
        var rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-scrollingController.scrollingSpeed * scrollingScaling, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -15.0f)
        {
            Destroy(gameObject);
        }
    }
}
