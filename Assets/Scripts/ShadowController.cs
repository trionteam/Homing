using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowController : MonoBehaviour
{
    public float shadowShift = 0.0f;

    public float yShift = 0.0f;
    public float xShift = 0.0f;

    public float deadScale = 1.0f;
    public float deadAlpha = 0.2f;
    public bool wasScaled = false;

    // Start is called before the first frame update
    void Start()
    {
        yShift = transform.position.y - transform.parent.position.y;
        xShift = transform.position.x - transform.parent.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.identity;
        var pos = transform.position;

        var parentPosition = transform.parent.position;
        var parentRotation = transform.parent.rotation.z;
        pos.x = parentPosition.x + xShift - Mathf.Sin(parentRotation) * shadowShift;
        pos.y = parentPosition.y + yShift;
        transform.position = pos;

        var health = GetComponentInParent<HealthController>();
        if (health != null && !wasScaled && health.health <= 0.0f)
        {
            wasScaled = true;
            var scale = transform.localScale;
            scale.Scale(new Vector3(deadScale, deadScale, 1.0f));
            transform.localScale = scale;

            var sprite = GetComponent<SpriteRenderer>();
            var color = sprite.color;
            color.a = deadAlpha;
            sprite.color = color;
        }
    }
}
