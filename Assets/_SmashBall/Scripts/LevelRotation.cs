using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRotation : MonoBehaviour
{
    public GameObject[] obstacleModel;
    public GameObject[] obstaclePrefab = new GameObject[4];
    public GameObject winPrefab;

    private GameObject temp1Obstacle, temp2Obstacle;
    private int level = 1, addNumber = 7; 
    float obstacleNumber = 0;



    void Start()
    {
        randomObstaclegenerator();
        for(obstacleNumber = 0; obstacleNumber > -level -addNumber; obstacleNumber -=0.5f )
        {
            if(level <= 12)
            {
                temp1Obstacle = Instantiate(obstaclePrefab[Random.Range(0, 2)]);
            }

            if (level > 12 && level < 20)
            {
                temp1Obstacle = Instantiate(obstaclePrefab[Random.Range(1, 3)]);
            }

            if (level >= 20 && level <= 50)
            {
                temp1Obstacle = Instantiate(obstaclePrefab[Random.Range(2, 4)]);
            }

            temp1Obstacle.transform.position = new Vector3(0, obstacleNumber - 0.01f, 0);
            temp1Obstacle.transform.eulerAngles = new Vector3(0, obstacleNumber * 8, 0);

            if (obstacleNumber >= level * .3f && obstacleNumber <= level * .6f)
            {
                temp1Obstacle.transform.eulerAngles = new Vector3(0, obstacleNumber * 8, 0);
                temp1Obstacle.transform.eulerAngles += Vector3.up * 180;
            }else if(obstacleNumber > level * .8f)
            {
                temp1Obstacle.transform.eulerAngles = new Vector3(0, obstacleNumber * 8, 0);
                temp1Obstacle.transform.eulerAngles += Vector3.up * 90;
            }
            


            temp1Obstacle.transform.parent = FindObjectOfType<RotateControl>().transform;

        }
        temp2Obstacle = Instantiate(winPrefab);
        temp2Obstacle.transform.position = new Vector3(0, obstacleNumber - 0.01f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void randomObstaclegenerator()
    {
        int random = Random.Range(0, 3);

        switch (random)
        {
            case 0:
                for (int i = 0; i < 4; i++)
                {
                    obstaclePrefab[i] = obstacleModel[i];
                }
                break;


            case 1:
                for (int i = 0; i < 4; i++)
                {
                    obstaclePrefab[i] = obstacleModel[i + 4];
                }
                break;

            case 2:
                for (int i = 0; i < 4; i++)
                {
                    obstaclePrefab[i] = obstacleModel[i + 8];
                }
                break;
            default:
                break;
        }
    }


}
