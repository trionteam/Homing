using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BackgroundScrollerController : MonoBehaviour
{
    [System.Serializable]
    public class ScoredTexture : IWeightedItem<Texture2D>
    {
        public float weight;
        public float Weight { get { return weight; } }

        public Texture2D texture;
        public Texture2D Item { get { return texture; } }
    }

    public ScoredTexture[] sprites = new ScoredTexture[0];
    Material[] materials = null;

    public Transform tilePrefab;
    public Material materialPrefab;

    public float tileWidth = 1.0f;
    public float tileHeight = 1.0f;

    public float leftOffset = 0.0f;
    public float rightOffset = 0.0f;

    public int numActiveTiles = 10;

    public float scrollSpeedFactor = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        leftOffset = -tileWidth * (numActiveTiles - 1) / 2.0f;
        rightOffset = tileWidth * (numActiveTiles - 1) / 2.0f;

        materials = new Material[sprites.Length];
        for (int i = 0; i < sprites.Length; ++i)
        {
            materials[i] = Instantiate(materialPrefab);
            materials[i].mainTexture = sprites[i].Item;
        }

        for (int i = 0; i < numActiveTiles; ++i)
        {
            GenerateNewTile(-(i + 1) * tileWidth);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var scrollingController = ScrollingController.GetInstance();
        var delta = scrollSpeedFactor * scrollingController.scrollingSpeed * Time.fixedDeltaTime;
        foreach (var child in GetComponentsInChildren<Transform>())
        {
            if (child == transform) continue;
            var position = child.position;
            position.x -= delta;
            child.position = position;

            if (position.x < leftOffset)
            {
                var overlap = position.x - leftOffset;
                Destroy(child.gameObject);
                GenerateNewTile(overlap);
            }
        }
    }

    GameObject GenerateNewTile(float delta)
    {
        var selected = Random.Range(0, sprites.Length);
        var sprite = sprites[selected];

        var position = transform.position;
        position.x = rightOffset + delta + tileWidth;

        var tile = Instantiate(tilePrefab, position, Quaternion.identity, transform);

        var renderer = tile.GetComponent<MeshRenderer>();
        renderer.material = materials[selected];

        return tile.gameObject;
    }
}
