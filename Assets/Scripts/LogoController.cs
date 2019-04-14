using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoController : MonoBehaviour
{
    public float fadeoutDuration = 1.0f;

    public float disappearAfterTime = 10.0f;

    public bool isFadingOut = false;

    float displayedTime = 0.0f;

    float startFadeOutTime = 0.0f;

    void Start()
    {
        displayedTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFadingOut)
        {
            var percent = startFadeOutTime + fadeoutDuration - Time.time;
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
        else if (displayedTime + disappearAfterTime < Time.time ||
                 Input.GetAxis("Horizontal-1") != 0.0f ||
                 Input.GetAxis("Horizontal-2") != 0.0f ||
                 Input.GetAxis("Vertical-1") != 0.0f ||
                 Input.GetAxis("Vertical-2") != 0.0f)
        {
            isFadingOut = true;
            startFadeOutTime = Time.time;
        }
    }
}
