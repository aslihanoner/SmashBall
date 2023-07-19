using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FollowCamera : MonoBehaviour
{
    private Transform _player, _win;
    private float _cameraOffset = 4f;
    private bool _isShaking = false;

    private void OnEnable()
    {
        PlayerController.CameraShakeEvent += HandleCameraShake;
    }

    private void OnDisable()
    {
        PlayerController.CameraShakeEvent -= HandleCameraShake;
    }

    private void HandleCameraShake(float duration, float magnitude)
    {
        if (!_isShaking)
            StartCoroutine(Shake(duration, magnitude));
    }

    private IEnumerator Shake(float duration, float magnitude)
    {
        _isShaking = true;
        Vector3 originalPosition = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = originalPosition + new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = originalPosition;
        _isShaking = false;
    }

    private void Update()
    {
        if (transform.position.y > _player.position.y && transform.position.y > _win.position.y + _cameraOffset)
        {
            Vector3 cameraPos = new Vector3(transform.position.x, _player.position.y, transform.position.z);
            transform.position = new Vector3(transform.position.x, cameraPos.y, -10);
        }
    }

    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>().transform;
        LevelRotation.OnWinInstantiated += LevelRotation_OnWinInstantiated;
    }

    private void LevelRotation_OnWinInstantiated(GameObject obj)
    {
        _win = obj.transform;
    }
}
