using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [System.Serializable]
    public struct WeightedObstacleController : IWeightedItem<ObstacleController>
    {
        public float weight;
        public float Weight { get { return weight; } }

        public ObstacleController item;
        public ObstacleController Item { get { return item;  } }
    }

    public WeightedObstacleController[] weightedObstaclePrefabs = new WeightedObstacleController[0];

    public float obstacleTime = 0.5f;
    public float nextObstacleTime = 0.0f;

    public float obstaclePositionX = 10.0f;
    public float destroyAtPositionX = -15.0f;

    public float minObstacleY = -5.0f;
    public float maxObstacleY = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        nextObstacleTime = obstacleTime;
    }

    // Update is called once per frame
    void Update()
    {
        nextObstacleTime -= Time.deltaTime;
        if (nextObstacleTime < 0.0f)
        {
            Debug.Assert(minObstacleY < maxObstacleY);
            var delta = maxObstacleY - minObstacleY;
            var obstaclePositionY = minObstacleY + Random.value * delta;
            var newObstaclePosition = new Vector3(obstaclePositionX, obstaclePositionY, transform.position.z);
            var prefab = WeightedChoice.Choice<WeightedObstacleController, ObstacleController>(weightedObstaclePrefabs);
            var obstacle = Instantiate(prefab, newObstaclePosition, Quaternion.identity);
            nextObstacleTime += obstacleTime;
        }
    }
}
