using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LevelRotation : MonoBehaviour
{
    [SerializeField] Transform _cylinderPivot;

    [SerializeField] GameObject[] _obstacleModel;
    [SerializeField] GameObject[] _obstaclePrefab = new GameObject[4];
    [SerializeField] GameObject _winPrefab;


    private int _level = 1, _addNumber = 7; 
    float _obstacleNumber = 0;

    [SerializeField] Material _plateMaterial, _baseMaterial;
    [SerializeField] MeshRenderer _playerMeshRenderer;

    public static event Action<GameObject> OnWinInstantiated;

    void Awake()
    {
        _level = PlayerPrefs.GetInt("level", 1);

        ObstacleGenerator();
        SetFinishLinePosition();
    }

    private void SetFinishLinePosition()
    {
        GameObject _finishLine = Instantiate(_winPrefab);
        OnWinInstantiated?.Invoke(_finishLine);
        _finishLine.transform.position = new Vector3(0, _obstacleNumber - 0.01f, 0);

        _cylinderPivot.position = _finishLine.transform.position;
        _cylinderPivot.localScale = new Vector3(1,1000f,1);
    }

    private void ObstacleGenerator()
    {
        GameObject _obstacle = default;

        RandomObstacleGenerator();
        float randomNumber = Random.value;
        for (_obstacleNumber = 0; _obstacleNumber > -_level - _addNumber; _obstacleNumber -= 0.5f)
        {
            if (_level <= 10)
            {
                _obstacle = Instantiate(_obstaclePrefab[Random.Range(0, 2)]);
            }

            if (_level > 10 && _level < 20)
            {
                _obstacle = Instantiate(_obstaclePrefab[Random.Range(1, 3)]);
            }

            if (_level >= 20 && _level <= 50)
            {
                _obstacle = Instantiate(_obstaclePrefab[Random.Range(2, 4)]);
            }

            _obstacle.transform.position = new Vector3(0, _obstacleNumber - 0.01f, 0);
            _obstacle.transform.eulerAngles = new Vector3(0, _obstacleNumber * 8, 0);

            if (Mathf.Abs(_obstacleNumber) >= _level * .3f && Mathf.Abs(_obstacleNumber) <= _level * .6f)
            {
                _obstacle.transform.eulerAngles = new Vector3(0, _obstacleNumber * 8, 0);
                _obstacle.transform.eulerAngles += Vector3.up * 180;
            }
            else if (Mathf.Abs(_obstacleNumber) > _level * .8f)
            {
                _obstacle.transform.eulerAngles = new Vector3(0, _obstacleNumber * 8, 0);
                if (randomNumber > .82f)
                {
                    _obstacle.transform.eulerAngles += Vector3.up * 180;
                }

            }



            _obstacle.transform.parent = FindObjectOfType<RotateControl>().transform;

        }

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _plateMaterial.color = Random.ColorHSV(0, 1, 0.5f, 1, 1, 1);
            _baseMaterial.color = _plateMaterial.color + Color.gray;
            _playerMeshRenderer.material.color = _baseMaterial.color;
        }
    }
    public void RandomObstacleGenerator()
    {
        int random = Random.Range(0, 2);

        switch (random)
        {
            case 0:
                for (int i = 0; i < 4; i++)
                {
                    _obstaclePrefab[i] = _obstacleModel[i];
                }
                break;


            case 1:
                for (int i = 0; i < 4; i++)
                {
                    _obstaclePrefab[i] = _obstacleModel[i + 4];
                }
                break;
            default:
                break;
        }
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
        SceneManager.LoadScene(0);
    }





}
