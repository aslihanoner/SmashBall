using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChecks : MonoBehaviour
{
    public Rigidbody rb;

    bool hit;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hit = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            hit = false;
        }

    }

    private void FixedUpdate()
    {
        if (hit)
        {
            rb.velocity = new Vector3(0, -100 * Time.fixedDeltaTime * 7, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hit)
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
        }
    }
}
