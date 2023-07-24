using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleNewController : MonoBehaviour
{
    [SerializeField]
    private ObstacleNew[] _obstacles = null;
    public bool isBroken = false;
    public void ShatterAnimationAllObstacles()
    {
        if (transform.parent != null)
        {
            transform.parent = null;

        }
        
        foreach (ObstacleNew item in _obstacles)
        {
            if (!ObstacleNew.isBroken)
            { 
                item.ShatterAnimation(); 
            } 
        }

        StartCoroutine(RemoveAllShatterAnimation());
    }

    IEnumerator RemoveAllShatterAnimation()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
