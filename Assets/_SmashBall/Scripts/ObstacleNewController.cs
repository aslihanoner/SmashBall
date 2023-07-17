using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleNewController : MonoBehaviour
{
    [SerializeField]
    private ObstacleNew[] obstacles = null;
    public void ShatterAnimationAllObstacles()
    {
        if (transform.parent != null)
        {
            transform.parent = null;

        }
        
        foreach (ObstacleNew item in obstacles)
        {
            item.ShatterAnimation();
        }

        StartCoroutine(RemoveAllShatterAnimation());
    }

    IEnumerator RemoveAllShatterAnimation()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
