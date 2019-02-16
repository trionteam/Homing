using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceCounter : MonoBehaviour
{
    public float distance = 0.0f;

    public int scoreMultiplier = 10;

    public static DistanceCounter Instance { get; private set; }

    public Camera screenCamera;

    public Text positiveScoreText;
    public Text negativeScoreText;

    public PlayerController player;

    public void PostScore(float score, Transform where)
    {
        // Only count score when the player is alive.
        if (!player.IsAlive) return;

        distance += score;
        var parent = gameObject.GetComponentInParent<Canvas>().gameObject;
        var scoreTextPrefab = score > 0.0f ? positiveScoreText : negativeScoreText;
        var scoreText = Instantiate(scoreTextPrefab, parent.transform).GetComponent<Text>();
        scoreText.text = string.Format("{0}", (int)score * scoreMultiplier);

        var pos = screenCamera.WorldToScreenPoint(where.position);
        scoreText.rectTransform.position = pos;
    }

    private void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        ScrollingController scrollingController = ScrollingController.GetInstance();

        distance += scrollingController.scrollingSpeed * Time.deltaTime;
        distance = Mathf.Max(0.0f, distance);

        var text = GetComponent<Text>();
        text.text = string.Format("{0}", scoreMultiplier * (int)distance);
    }
}
