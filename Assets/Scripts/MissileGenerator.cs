using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileGenerator : MonoBehaviour
{
    public MissileController missilePrefab;

    public float backStartX = -10.0f;
    public float backMinY = -5.0f;
    public float backMaxY = 5.0f;

    public float sideStartY = 6.0f;
    public float sideMinX = -8.0f;
    public float sideMaxX = 0.0f;

    public float lastMissile = 0.0f;
    public float missileTime = 0.5f;

    public float startTime = 0.0f;
    public float onlyOneMissileTime = 20.0f;
    public float onlyLeftMissileTime = 40.0f;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        lastMissile = startTime + onlyOneMissileTime;
    }

    // Update is called once per frame
    void Update()
    {
        var missiles = GameObject.FindGameObjectsWithTag(Tags.Missile);
        if (missiles.Length == 0 || lastMissile + missileTime < Time.time)
        {
            // Add new missile.
            var y = Random.Range(backMinY, backMaxY);
            var newMissile = Instantiate(missilePrefab, NewMissilePosition(), Quaternion.identity).GetComponent<MissileController>();
            newMissile.player = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<PlayerController>();
            lastMissile = Mathf.Max(Time.time, lastMissile);
        }
    }

    Vector2 NewMissilePosition()
    {
        int type = 0;
        if (Time.time > startTime +  onlyLeftMissileTime) type = Random.Range(0, 5);
        switch (type)
        {
            case 0:
            case 1:
            case 2:
                return new Vector2(backStartX, Random.Range(backMinY, backMaxY));
            case 3:
                return new Vector2(Random.Range(sideMinX, sideMaxX), -sideStartY);
            case 4:
                return new Vector2(Random.Range(sideMinX, sideMaxX), sideStartY);
        }
        return new Vector2();
    }
}
