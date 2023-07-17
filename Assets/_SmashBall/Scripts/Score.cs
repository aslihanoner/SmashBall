using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Score instance;

    public int score;
    public Text scoreTxt;
    private void Awake()
    {
        makeSingleton();
        scoreTxt = GameObject.Find("scoreTxt").GetComponent<Text>();

    }

    private void makeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    void Start()
    {
        addScore(0);
    }

    
    void Update()
    {
        if (scoreTxt == null)
        {
            scoreTxt = GameObject.Find("scoreTxt").GetComponent<Text>();
        }
    }

    public void addScore(int value)
    {
        score += value;
        if(score > PlayerPrefs.GetInt(" HighScore",0))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }

        scoreTxt.text = score.ToString();
        Debug.Log("Score --> " + score);
    }

    public void removeScore()
    {
        score = 0;
    }
}
