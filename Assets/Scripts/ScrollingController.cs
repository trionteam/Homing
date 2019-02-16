using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingController : MonoBehaviour
{
    public float scrollingSpeed = 1.0f;

    public static ScrollingController GetInstance()
    {
        return GameObject.FindGameObjectWithTag(Tags.ScrollingController).GetComponent<ScrollingController>();
    }
}
