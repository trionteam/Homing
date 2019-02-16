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
        if (rb != null)
        {
            var oldPosition = rb.position;
            oldPosition.x -= scrollingController.scrollingSpeed * scrollingScaling * Time.fixedDeltaTime;
            rb.MovePosition(oldPosition);
            rb.velocity = new Vector2();
        }
        else
        {
            var oldPosition = transform.position;
            oldPosition.x -= scrollingController.scrollingSpeed * scrollingScaling * Time.fixedDeltaTime;
            transform.position = oldPosition;
        }
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
