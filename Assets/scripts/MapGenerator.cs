using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject groundTile;
    float groundTileLength = 10;
    
    public GameObject[] obstacles;

    float groundTileNextZ = 0;
    float obstacleLastZ = 0;
    public float obstacleMinDistance = 30;
    public float obstacleMaxDistance = 70;


    public int levelSizeInGroundTiles = 500;

    float LevelSize(){return levelSizeInGroundTiles * groundTileLength;}


    void Start()
    {
        GenerateGround();
        GenerateObstacles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateGround(){
        float nextGroundTileZ = 0;

        for(int i = 0; i < levelSizeInGroundTiles; i++){
        Instantiate(groundTile, new Vector3(0, 0, nextGroundTileZ), Quaternion.identity);
        nextGroundTileZ += groundTileLength;
        }
    }

    void GenerateObstacles(){
        while (obstacleLastZ < LevelSize()){
            GameObject obstacleToInstantiate = obstacles[Random.Range(0, obstacles.Length)];

            Vector3 obstaclePosition = obstacleToInstantiate.transform.position;
            float obstacleZPosition = obstacleLastZ + Random.Range(obstacleMinDistance, obstacleMaxDistance);
            obstaclePosition.z = obstacleZPosition;

            Instantiate(obstacleToInstantiate, obstaclePosition, Quaternion.identity);

            obstacleLastZ = obstacleZPosition;
        }
    }
}
