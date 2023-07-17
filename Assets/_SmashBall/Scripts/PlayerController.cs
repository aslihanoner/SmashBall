using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    bool hit;

    float currentTime;

    bool invincible;

    [SerializeField]
    AudioClip win, death, invictibledestroy, destroy, bounce;

    public int currentObstacleNum;
    public int totalObstacleNum;

    public Image InvictableSlider;
    public GameObject InvictableObject;

    public GameObject GameOverUI;
    public GameObject FinishUI;

    public enum PlayerState
    {
        Prepare,
        Playing,
        Died,
        Finish
    }
    [HideInInspector]

    public PlayerState playerState = PlayerState.Prepare;


    void Start()
    {
        totalObstacleNum = FindObjectsOfType<ObstacleNewController>().Length;



    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentObstacleNum = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (playerState == PlayerState.Playing)
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
                currentTime -= Time.deltaTime * .4f;
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


            if(currentTime>=0.15f || InvictableSlider.color == Color.red)
            {
                InvictableObject.SetActive(true);
            }
            else
            {
                InvictableObject.SetActive(false);
            }


            if (currentTime >= 1)
            {
                currentTime = 1;
                invincible = true;
                Debug.Log("invincible");
                InvictableSlider.color = Color.red;


            }
            else if (currentTime <= 0)
            {
                currentTime = 0;
                invincible = false;
                Debug.Log("*******");
                InvictableSlider.color = Color.white;
            }

            if (InvictableObject.activeInHierarchy)
            {
                InvictableSlider.fillAmount = currentTime / 1;
            }
        }

        if (playerState == PlayerState.Prepare)
        {
            if (Input.GetMouseButton(0))
            {
                playerState = PlayerState.Playing;
            }
        }

        if (playerState == PlayerState.Finish)
        {
            if (Input.GetMouseButtonDown(0))
            {
                FindObjectOfType<LevelRotation>().nextLevel();
            }
        }


    }
    public void shatterObstacle()
    {
        if (invincible)
        {
            Score.instance.addScore(1);
        }
        else
        {
            Score.instance.addScore(2);
        }
        
    }

    private void FixedUpdate()
    {
        if (playerState == PlayerState.Playing)
        {
            if (hit)
            {
                rb.velocity = new Vector3(0, -100 * Time.fixedDeltaTime * 7, 0);
            }
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
                    //Destroy(collision.transform.parent.gameObject);
                    collision.transform.parent.GetComponent<ObstacleNewController>().ShatterAnimationAllObstacles();
                    shatterObstacle();
                    Sound.instance.playSoundFX(invictibledestroy, volume: 0.5f);
                    currentObstacleNum++;

                }
                
            }
            else
            {
                if (collision.gameObject.tag == "enemy")
                {
                    //Destroy(collision.transform.parent.gameObject);
                    collision.transform.parent.GetComponent<ObstacleNewController>().ShatterAnimationAllObstacles();
                    shatterObstacle();
                    Sound.instance.playSoundFX(destroy, volume: 0.5f);
                    currentObstacleNum++;

                }
                else if (collision.gameObject.tag == "plane")
                {
                    Debug.Log(message: "GameOver");
                    GameOverUI.SetActive(true);
                    playerState = PlayerState.Finish;
                    gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    Score.instance.removeScore();
                    Sound.instance.playSoundFX(death, volume: 0.5f);
                    
                }
            }

        }

        FindObjectOfType<GameUI>().levelSliderFill(currentObstacleNum / (float)totalObstacleNum);


        if(collision.gameObject.tag=="Finish" && playerState == PlayerState.Playing)
        {
            playerState = PlayerState.Finish;
            Sound.instance.playSoundFX(win, volume: 0.5f);
            FinishUI.SetActive(true);
            FinishUI.transform.GetChild(0).GetComponent<Text>().text = "Level" + PlayerPrefs.GetInt("Level");
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if (!hit || collision.gameObject.tag == "Finish")
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
            Sound.instance.playSoundFX(bounce, volume: 0.5f);
        }
    }
}
