using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyboardController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            // When at the main screen (the logo is still visible), kill the app. Otherwise, go to main menu.
            if (GameObject.FindGameObjectWithTag(Tags.Logo))
            {
#if !UNITY_WEBGL
                // In WebGL mode, this seems to hang the application.
                Application.Quit();
#endif
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
