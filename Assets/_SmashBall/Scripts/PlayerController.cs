using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    bool hit;

    float currentTime;

    bool invincible;
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


        if (invincible)
        {
            currentTime -= Time.deltaTime * .35f;
        }
        else
        {
            if (hit)
            {
                currentTime += Time.deltaTime * 0.8f;
            }
            else
            {
                currentTime -= Time.deltaTime * 0.5f;
            }
        }

        if(currentTime >= 1)
        {
            currentTime = 1;
            invincible = true;
            Debug.Log("invincible");
        }else if(currentTime <= 0)
        {
            currentTime = 0;
            invincible = false;
            Debug.Log("*******");
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
        else
        {

            if (invincible)
            {
                if (collision.gameObject.tag == "enemy" && collision.gameObject.tag == "plane")
                {
                    Destroy(collision.transform.parent.gameObject);

                }
                
            }
            else
            {
                if (collision.gameObject.tag == "enemy")
                {
                    Destroy(collision.transform.parent.gameObject);

                }
                else if (collision.gameObject.tag == "plane")
                {
                    Debug.Log("GameOver");
                }
            }



      
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!hit)
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
        }
    }
}
