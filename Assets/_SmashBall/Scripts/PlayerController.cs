using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody Rb;
    bool _hit;
    float _currentTime;
    bool _invincible;
    private Sound _soundManager;
    [SerializeField] AudioClip win, death, invincibleDestroy, destroy, bounce;
    private static int _currentObstacleNum;
    public int TotalObstacleNum;
    public Image InvincibleSlider;
    public GameObject InvincibleObject;
    public GameObject GameOverUI;
    public GameObject FinishUI;
    public delegate void CameraShakeEventHandler(float duration, float magnitude);
    public static event CameraShakeEventHandler CameraShakeEvent;
    public static event Action OnScore;



    public enum PlayerState{Prepare, Playing, Died, Finish}
    [HideInInspector] public PlayerState playerState = PlayerState.Prepare;

    public static int CurrentObstacleNum { get => _currentObstacleNum; set { _currentObstacleNum = value; OnScore?.Invoke(); }  } 

    void Start()
    {
        TotalObstacleNum = FindObjectsOfType<ObstacleNewController>().Length;
        _soundManager = FindObjectOfType<Sound>();
    }

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
        CurrentObstacleNum = 0;
    }

    private void Update()
    {
        if (playerState == PlayerState.Playing)
        {
            _hit = Input.GetMouseButton(0);

            if (_invincible) { _currentTime -= Time.deltaTime * 0.4f; }
            else
            {
                _currentTime += _hit ? Time.deltaTime * 0.8f : -Time.deltaTime * 0.5f;

                _currentTime = Mathf.Clamp01(_currentTime);

                if (InvincibleSlider != null)
                {
                    InvincibleSlider.fillAmount = _currentTime;
                    InvincibleObject.SetActive(_currentTime >= 0.4f || InvincibleSlider.color == Color.red);
                }

            }
            if (_currentTime >= 1)
            {
                _currentTime = 1;
                _invincible = true;
                if (InvincibleSlider != null)
                {
                    InvincibleSlider.color = Color.red;
                }

            }
            else if (_currentTime <= 0)
            {
                _currentTime = 0;
                _invincible = false;
                if (InvincibleSlider != null)
                {
                    InvincibleSlider.color = Color.white;
                }
            }

            if (InvincibleObject.activeInHierarchy)
            {
                InvincibleSlider.fillAmount = _currentTime / 1;
            }

        }

        if (playerState == PlayerState.Prepare && Input.GetMouseButton(0))
        { 
            playerState = PlayerState.Playing;
        }

        if (playerState == PlayerState.Finish && Input.GetMouseButtonDown(0))
        { 
            FindObjectOfType<LevelRotation>().NextLevel(); 
        }
    }
    public void ShatterObstacle(ObstacleNewController obstacleController)
    {
        if (obstacleController != null && !obstacleController.isBroken)
        {
           
            CameraShakeEvent?.Invoke(0.2f, 0.1f);
            _soundManager = FindObjectOfType<Sound>();
            obstacleController.ShatterAnimationAllObstacles();
            _soundManager.PlaySoundFX(_invincible ? invincibleDestroy : destroy, volume: 0.5f);
            CurrentObstacleNum++;
            OnScore.Invoke();
        }
    }

    private void FixedUpdate()
    {
        if (playerState == PlayerState.Playing && _hit)
        {
            if (Input.GetMouseButton(0)) 
            { 
                Rb.velocity = new Vector3(0, -100 * Time.fixedDeltaTime * 7, 0);
            }
           
        }
            
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (playerState == PlayerState.Playing) 
        {
            if (_hit) 
            {
                Rb.velocity = Vector3.zero; 
            }
            else 
            {
                Rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0); 
            }
            if (collision.gameObject.CompareTag("Finish"))
            {
                playerState = PlayerState.Finish;
                _soundManager.PlaySoundFX(win, volume: 0.5f);
                FinishUI.SetActive(true);
                FinishUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Level " + PlayerPrefs.GetInt("Level", 1);
                return;

            }

            ObstacleNewController obstacleNewController = collision.gameObject.GetComponentInParent<ObstacleNewController>();

            if (_hit) 
            {
                if (collision.gameObject.CompareTag("enemy"))
                {
                    obstacleNewController.ShatterAnimationAllObstacles();
                    ShatterObstacle(obstacleNewController);
                    _soundManager.PlaySoundFX(_invincible ? invincibleDestroy : destroy, volume: 0.5f);
                    CurrentObstacleNum++;
                }
                else if (collision.gameObject.CompareTag("plane") && _invincible)
                {
                    obstacleNewController.ShatterAnimationAllObstacles();
                    ShatterObstacle(obstacleNewController);
                    GameOverUI.SetActive(true);
                    playerState = PlayerState.Finish;
                    gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    _soundManager.PlaySoundFX(_invincible ? invincibleDestroy : destroy, volume: 0.5f);
                    CurrentObstacleNum++;
                }
            }

            GameUI.Instance.LevelSliderFill(CurrentObstacleNum / (float)TotalObstacleNum);
        }
    }


    private void OnCollisionStay(Collision collision)
    {
        if (!_hit || collision.gameObject.CompareTag("Finish"))
        {
            Rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
            _soundManager.PlaySoundFX(bounce, volume: 0.5f);
        }
    }
}
