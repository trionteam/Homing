using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public ScrollingController scrollingController;

    public float deathSlowdown = 0.01f;
    public float deathTime = 0.0f;
    public float deathDelay = 3.0f;

    bool isFadingOut = false;

    public GameObject[] generatorsToDeactivate = new GameObject[0];

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        isFadingOut = false;
    }

    bool PlayersAreDead()
    {
        var playerObjects = GameObject.FindGameObjectsWithTag(Tags.Player);
        foreach (var playerObject in playerObjects)
        {
            PlayerController player = playerObject.GetComponent<PlayerController>();
            if (player.IsAlive) return false;
        }
        return true;
    }

    void FixedUpdate()
    {
        if (PlayersAreDead())
        {
            scrollingController.scrollingSpeed = Mathf.Lerp(scrollingController.scrollingSpeed, 0.0f, deathSlowdown);
        }
    }

    void Update()
    {
        if (PlayersAreDead())
        {
            // Start countdown until any key will work.
            if (deathTime == 0.0f) deathTime = Time.time + deathDelay;
            if (deathTime < Time.time)
            {
                foreach (GameObject generator in generatorsToDeactivate) generator.SetActive(false);
            }
            if (Input.anyKey && !isFadingOut)
            {
                isFadingOut = true;
                var fader = GameObject.FindGameObjectWithTag("ScreenFade").GetComponent<ScreenFadeController>();
                fader.FadeOut();

                Invoke("RestartScene", 1.0f);
            }
        }
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(0);
    }
}
