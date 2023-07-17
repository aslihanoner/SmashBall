using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Score _instance;

    private int _score;

    [SerializeField] TextMeshProUGUI _scoreTxt;
    private void Awake()
    {
        MakeSingleton();

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

    void Start()
    {
        AddScore(0);
    }

    
    

    public void AddScore(int value)
    {
        _score += value;
        if(_score > PlayerPrefs.GetInt(" HighScore",0))
        {
            PlayerPrefs.SetInt("HighScore", _score);
        }

        _scoreTxt.text = _score.ToString();
        Debug.Log("Score --> " + _score);
    }

    public void RemoveScore()
    {
        _score = 0;
    }
}
