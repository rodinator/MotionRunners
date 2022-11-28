﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    Camera cam;
    public GameObject[] groundTile;
    float groundTileLength = 10;
    float farestGroundTileZ = 0;
    
    public GameObject[] obstacles;

    float farestObstacleZ = 0;
    public float obstacleMinDistance = 30;
    public float obstacleMaxDistance = 70;

    public int sightDistance = 110;
    public int distanceToDelete = 30;


    float camZ;

    void Start()
    {
        cam = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        camZ = cam.transform.position.z;
        GenerateGround();
        GenerateObstacles();
        DestroyMapBehindCam();
    }

    void GenerateGround(){

        //for(float i = farestGroundTileZ; i < sightDistance + cameraZ; i++)
        while (farestGroundTileZ < sightDistance + camZ){
        GameObject newTile = Instantiate(groundTile[Random.Range(0, groundTile.Length)], new Vector3(0, 0, farestGroundTileZ), Quaternion.identity, this.transform);
        
        float mirrorTile = -1;
        if (Random.value > .5f)
            mirrorTile = 1;
        
        Vector3 newScale = newTile.transform.localScale;
        newScale.x = newScale.x * mirrorTile;
        newTile.transform.localScale = newScale;

        farestGroundTileZ += groundTileLength;
        }
    }

    void GenerateObstacles(){
        while (farestObstacleZ < sightDistance + camZ){
            GameObject obstacleToInstantiate = obstacles[Random.Range(0, obstacles.Length)];

            Vector3 obstaclePosition = obstacleToInstantiate.transform.position;
            float obstacleZPosition = farestObstacleZ + Random.Range(obstacleMinDistance, obstacleMaxDistance);
            obstaclePosition.z = obstacleZPosition;

            GameObject newObstacle = Instantiate(obstacleToInstantiate, obstaclePosition, obstacleToInstantiate.transform.rotation, this.transform);
            farestObstacleZ = obstacleZPosition;
        }
    }

    void DestroyMapBehindCam(){
        for (int i = transform.childCount -1; i >= 0; i--){
            Transform mapTile = transform.GetChild(i);

            if (mapTile.position.z + distanceToDelete < camZ){

                Destroy(mapTile.gameObject);

            }

        }
    }
}