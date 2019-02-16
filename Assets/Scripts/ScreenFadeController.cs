using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFadeController : MonoBehaviour
{
    public float fadeDuration = 1.0f;

    public bool fadeIn = true;

    public bool isFading = false;

    void Start()
    {
        isFading = true;
        fadeIn = true;

        GetComponent<MeshRenderer>().enabled = true;
    }

    public void FadeIn()
    {
        isFading = true;
        fadeIn = true;
        var renderer = GetComponent<MeshRenderer>();
        renderer.enabled = true;
        renderer.material.color = Color.black;
    }

    public void FadeOut()
    {
        isFading = true;
        fadeIn = false;
        var renderer = GetComponent<MeshRenderer>();
        renderer.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFading) return;

        var fadeSpeed = 1.0f / fadeDuration;
        var renderer = GetComponent<MeshRenderer>();
        var material = renderer.material;

        var color = material.color;
        var delta = Time.deltaTime * fadeSpeed;
        if (fadeIn) color.a -= delta;
        else color.a += delta;

        if (fadeIn && color.a <= 0.0f)
        {
            color.a = 0.0f;
            renderer.enabled = false;
            isFading = false;
        }
        if (!fadeIn && color.a >= 1.0f)
        {
            color.a = 1.0f;
            isFading = false;
        }
        material.color = color;
    }
}
