using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplayController : MonoBehaviour
{
    public DistanceCounter scoreCounter;

    void Update()
    {
        var text = GetComponent<Text>();
        text.text = string.Format("{0}", scoreCounter.Score);
    }
}
