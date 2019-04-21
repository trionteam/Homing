using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurboBarController : MonoBehaviour
{
    public PlayerController player;

    private RectTransform rect;
    private Image image;

    public float maxWidth = 0.0f;

    public float canActivateTurboAlpha = 0.9f;
    public float canNotActivateTurboAlpha = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
        maxWidth = rect.sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        var targetWidth = maxWidth * player.turboCapacity / player.maxTurboCapacity;
        var size = rect.sizeDelta;
        size.x = targetWidth;
        rect.sizeDelta = size;

        // Decrease the alpha when the turbo can't be activated.
        bool canActivateTurbo = player.turboCapacity >= player.minTurboCapacity;
        var color = image.color;
        color.a = canActivateTurbo ? canActivateTurboAlpha : canNotActivateTurboAlpha;
        image.color = color;
    }
}
