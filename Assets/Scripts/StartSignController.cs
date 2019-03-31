using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSignController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<PlayerController>() != null)
        {
            GameController.Instance.StartGame();
        }
    }
}
