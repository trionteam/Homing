using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreFloatController : MonoBehaviour
{
    public float velocity = 0.1f;

    public float lifespan = 0.5f;

    public float deadline = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        deadline = Time.time + lifespan;
    }

    // Update is called once per frame
    void Update()
    {
        var text = GetComponent<Text>();

        var pos = text.rectTransform.position;
        pos.y += velocity * Time.deltaTime;
        text.rectTransform.position = pos;

        var timeLeft = deadline - Time.time;

        var color = text.color;

        color.a = timeLeft / lifespan;
        text.color = color;

        if (timeLeft <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
