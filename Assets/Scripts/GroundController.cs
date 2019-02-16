using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    public int numXTiles = 1;
    public int numYTiles = 1;

    public GroundScrolling tilePrefab;
    public GroundScrolling topTilePrefab;

    public float tileSize = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        float x_offset = -numXTiles * tileSize / 2.0f;
        float y_offset = -numYTiles * tileSize / 2.0f;
        for (int x = 0; x < numXTiles; ++x)
        {
            for (int y = 0; y < numYTiles; ++y)
            {
                var tilePosition = new Vector3(x_offset + x * tileSize, y_offset + y * tileSize, transform.position.z);
                var prefab = y == numYTiles - 1 ? topTilePrefab : tilePrefab;
                var tile = Instantiate(prefab, tilePosition, Quaternion.identity, transform).GetComponent<GroundScrolling>();
                tile.moveRight = tileSize * numXTiles;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
