using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingController : MonoBehaviour
{
    public enum ScrollingState
    {
        Stopped,
        Starting,
        Scrolling,
        Stopping
    }

    public static ScrollingController Instance { get; private set; }

    public float scrollingSpeed = 0.0f;

    public float targetScrollingSpeed = 3.0f;

    public float slowdownFactor = 0.01f;
    public float speedupFactor = 0.1f;

    ScrollingState state = ScrollingState.Stopped;

    public static ScrollingController GetInstance()
    {
        return GameObject.FindGameObjectWithTag(Tags.ScrollingController).GetComponent<ScrollingController>();
    }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        scrollingSpeed = 0.0f;
        state = ScrollingState.Stopped;
    }

    void FixedUpdate()
    {
        switch(state)
        {
            case ScrollingState.Starting:
                scrollingSpeed = Mathf.Lerp(scrollingSpeed, targetScrollingSpeed, speedupFactor);
                if (Mathf.Abs(scrollingSpeed - targetScrollingSpeed) < 1e-6)
                {
                    state = ScrollingState.Scrolling;
                }
                break;
            case ScrollingState.Stopping:
                scrollingSpeed = Mathf.Lerp(scrollingSpeed, 0.0f, slowdownFactor);
                if (scrollingSpeed < 1e-6)
                {
                    state = ScrollingState.Stopped;
                }
                break;
        }
    }

    public void StartScrolling()
    {
        state = ScrollingState.Starting;
    }

    public void StopScroling()
    {
        state = ScrollingState.Stopping;
    }
}
