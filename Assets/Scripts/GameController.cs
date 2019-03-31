using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public ScrollingController scrollingController;

    /// <summary>
    /// The in-game time after which restarting the game is allowed. We force a short
    /// pause between the death of the player and when the restart is allowed, so that
    /// the player does not do the restart accidentally, just by pressing an arrow key.
    /// </summary>
    public float unlockRestartTime = 0.0f;

    public float deathDelay = 5.0f;

    bool isFadingOut = false;

    public bool gameStarted = false;

    public GameObject[] generatorsToActivate = new GameObject[0];
    public float generatorActivationDelay = 3.0f;
    public GameObject[] delayedGeneratorsToActivate = new GameObject[0];
    public GameObject[] generatorsToDeactivate = new GameObject[0];

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        gameStarted = false;
        isFadingOut = false;
    }

    bool PlayersAreDead()
    {
        return gameStarted && PlayerController.NumPlayersAlive == 0;
    }

    void Update()
    {
        if (PlayersAreDead())
        {
            if (unlockRestartTime == 0.0f)
            {
                unlockRestartTime = Time.time + deathDelay;
                scrollingController.StopScroling();
                Invoke("StopGenerators", deathDelay);
            }
            if (unlockRestartTime < Time.time && Input.anyKey && !isFadingOut)
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

    public void StartGame()
    {
        if (gameStarted) return;

        gameStarted = true;
        scrollingController.StartScrolling();
        foreach (var generator in generatorsToActivate)
        {
            generator.SetActive(true);
        }
        Invoke("StartDelayedGenerators", generatorActivationDelay);
        
    }

    private void StartDelayedGenerators()
    {
        foreach (var generator in delayedGeneratorsToActivate)
        {
            generator.SetActive(true);
        }
    }

    private void StopGenerators()
    {
        foreach(var generator in generatorsToDeactivate)
        {
            generator.SetActive(false);
        }
    }
}
