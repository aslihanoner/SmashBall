using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateControl : MonoBehaviour
{
    public float Speed= 100f;
    void Start()
    {
        
    }
    void Update()
    {
        transform.Rotate(new Vector3(0, Speed * Time.deltaTime));
    }
}
