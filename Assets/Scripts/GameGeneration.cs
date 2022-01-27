using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameGeneration : MonoBehaviour
{
    [SerializeField] private int size = 15;
    
    [SerializeField] private int numberOfObstacles = 3;

    [SerializeField] private int minZones = 2;

    [SerializeField] private int maxZones = 5;

    [SerializeField] private GameObject groundGameObject;

    private List<GameObject> groundGameObjects = new List<GameObject>();
    
    [SerializeField] private GameObject wallGameObject;
    
    private List<Vector3> obstaclesPositions = new List<Vector3>();

    [SerializeField] private GameObject boxGameObject;
    
    private List<Vector3> boxesPositions = new List<Vector3>();

    [SerializeField] private GameObject zoneGameObject;
    
    private List<Vector3> zonesPositions = new List<Vector3>();

    private int numberZones;
    
    private GameManager gameManager;
    
    void Start()
    {
        InitiateObstaclesPositions();
        InitiateZonesPositions();
        InitiateBoxesPositions();
        
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.OnGameStarted += InitGame;
        
        gameManager.StartGame();
    }
    
    int InitGame()
    {
        InitiateObstaclesPositions();
        InitiateZonesPositions();
        InitiateBoxesPositions();
        for (int r = 0; r < size; r++)
        {
            for (int c = 0; c < size; c++)
            {
                bool bIsBorder = c == 0 || c == size - 1 || r == 0 || r == size - 1;
                if (bIsBorder)
                {
                    Instantiate(wallGameObject, new Vector3(c, 1, r), transform.rotation);
                }
                groundGameObjects.Add(Instantiate(groundGameObject, new Vector3(c,0,r), transform.rotation));
            }
        }
        foreach (var obstaclePosition in obstaclesPositions)
        {
            Instantiate(wallGameObject, obstaclePosition, transform.rotation);
        }
        
        foreach (var zonePosition in zonesPositions)
        {
            Instantiate(zoneGameObject, zonePosition, transform.rotation);
        }
        
        foreach (var boxPosition in boxesPositions)
        {
            Instantiate(boxGameObject, boxPosition, transform.rotation);
        }

        return numberZones;
    }

    private void InitiateObstaclesPositions()
    {

        for (int i = 0; i < numberOfObstacles; i++)
        {
            Vector3 obstaclePosition = new Vector3();

            do
            {
                obstaclePosition.Set(
                    Random.Range(1, size - 1),
                    0.5f,
                    Random.Range(1, size - 1)
                );            

            } while (!bIsSpaceFree(obstaclePosition));
            obstaclesPositions.Add(obstaclePosition);
        }
    }
    
    private void InitiateZonesPositions()
    {
        numberZones = Random.Range(minZones, maxZones + 1);

        for (int i = 0; i < numberZones; i++)
        {
            Vector3 zonePosition = new Vector3();

            do
            {
                zonePosition.Set(
                    Random.Range(1, size - 1),
                    0,
                    Random.Range(1,  size - 1)
                );
            } while (!bIsSpaceFree(zonePosition));

            zonesPositions.Add(zonePosition);
        }
    }
    
    private void InitiateBoxesPositions()
    {
        for (int i = 0; i < numberZones; i++)
        {
            Vector3 boxPosition = new Vector3();

            do
            {
                boxPosition.Set(
                    Random.Range(1, size - 2),
                    1,
                    Random.Range(1, size - 2)
                );
            } while (!bIsSpaceFree(boxPosition));

            boxesPositions.Add(boxPosition);
        }
    }

    private bool bIsSpaceFree(Vector3 position)
    {
        return obstaclesPositions.All(obstaclePosition => obstaclePosition.x != position.x && obstaclePosition.z != position.z)
               && zonesPositions.All(zonePosition => zonePosition.x != position.x && zonePosition.z != position.z) 
               && boxesPositions.All(boxPosition => boxPosition.x != position.x && boxPosition.z != position.z);
    }
    

    
}
