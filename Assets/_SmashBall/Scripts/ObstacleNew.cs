using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleNew : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;
    private Collider _collider;
    private ObstacleNewController _ObstacleNewController;
    public static bool isBroken = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
        _ObstacleNewController = transform.parent.GetComponent<ObstacleNewController>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShatterAnimation()
    {
        _rigidbody.isKinematic = false;
        _collider.enabled = false;


        Vector3 forcePoint = transform.parent.position;
        float parentX = transform.parent.position.x;
        float xPosition = _meshRenderer.bounds.center.x;

        Vector3 subDirection = ( parentX - xPosition < 0) ? Vector3.right : Vector3.left;

        Vector3 direction = (Vector3.up * 1.8f + subDirection).normalized;

        float force = Random.Range(10, 25);
        float torque = Random.Range(100, 200);

        _rigidbody.AddForceAtPosition(direction * force, forcePoint, ForceMode.Impulse);
        _rigidbody.AddTorque(Vector3.left * torque);
        _rigidbody.velocity = Vector3.down;

        isBroken = true;

    }
}
