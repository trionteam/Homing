using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoController : MonoBehaviour
{
    public float removeAfterDistance = 2.0f;

    float scrolledDistance = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        scrolledDistance = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        var scrollingController = ScrollingController.GetInstance();
        scrolledDistance += Time.deltaTime * scrollingController.scrollingSpeed;

        var percent = 1.0f - scrolledDistance / removeAfterDistance;
        if (percent < 0.0f)
        {
            Destroy(gameObject);
        }
        else
        {
            var renderer = GetComponent<SpriteRenderer>();
            var color = renderer.color;
            color.a = percent;
            renderer.color = color;
        }
    }
}
