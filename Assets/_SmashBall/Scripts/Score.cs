using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class Score : MonoBehaviour
{
    public static Score _instance;

    private int _score;

    [SerializeField] TextMeshProUGUI _scoreTxt;
    private void Awake()
    {
        MakeSingleton();
        _scoreTxt = GetComponent<TextMeshProUGUI>();
        PlayerController.OnScore += UpdateText;
    }

    private void UpdateText()
    {
        
        if (_score > PlayerPrefs.GetInt(" HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", _score);
        }

        _scoreTxt.text = PlayerController.CurrentObstacleNum.ToString();
        Debug.Log("Score --> " + _score);
    }

    private void MakeSingleton()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }

   
    public void RemoveScore()
    {
        _score = 0;
    }
}
