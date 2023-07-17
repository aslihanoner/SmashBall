using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleNew : MonoBehaviour
{
    private Rigidbody rigidbody;
    private MeshRenderer meshRenderer;
    private Collider collider;
    private ObstacleNewController ObstacleNewController;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
        ObstacleNewController = transform.parent.GetComponent<ObstacleNewController>();
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
        rigidbody.isKinematic = false;
        collider.enabled = false;

        Vector3 forcePoint = transform.parent.position;
        float parentX = transform.parent.position.x;
        float xPosition = meshRenderer.bounds.center.x;

        Vector3 subDirection = ( parentX - xPosition < 0) ? Vector3.right : Vector3.left;

        Vector3 direction = (Vector3.up * 1.8f + subDirection).normalized;

        float force = Random.Range(10, 25);
        float torque = Random.Range(100, 200);

        rigidbody.AddForceAtPosition(direction * force, forcePoint, ForceMode.Impulse);
        rigidbody.AddTorque(Vector3.left * torque);
        rigidbody.velocity = Vector3.down;

    }
}
