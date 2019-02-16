using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSignController : MonoBehaviour
{
    bool gameStarted = false;
    bool speedup = false;

    float finalScrollingSpeed = 1.0f;

    public float missilesDelay = 3.0f;

    public ScrollingController scrollingController;

    public GameObject[] generatorsToActivate = new GameObject[0];

    public GameObject[] delayedGeneratorsToActivate = new GameObject[0];

    // Start is called before the first frame update
    void Start()
    {
        gameStarted = false;
        speedup = false;
        finalScrollingSpeed = scrollingController.scrollingSpeed;
        scrollingController.scrollingSpeed = 0.0f;
    }

    IEnumerator DelayedStartGenerators()
    {
        yield return new WaitForSeconds(missilesDelay);
        foreach (var generator in delayedGeneratorsToActivate)
        {
            generator.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        if (speedup)
        {
            scrollingController.scrollingSpeed = Mathf.Lerp(scrollingController.scrollingSpeed, finalScrollingSpeed, 0.1f);
            if (Mathf.Abs(scrollingController.scrollingSpeed - finalScrollingSpeed) < 1e-6)
            {
                gameStarted = false;
                speedup = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameStarted && collision.GetComponentInParent<PlayerController>() != null)
        {
            gameStarted = true;
            speedup = true;
            foreach (var generator in generatorsToActivate)
            {
                generator.SetActive(true);
            }
            StartCoroutine(DelayedStartGenerators());
        }
    }
}
