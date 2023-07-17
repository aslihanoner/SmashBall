using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FollowCamera : MonoBehaviour
{
    private Vector3 cameraPos;
    private Transform player, win;

    private float cameraOffset = 4f;

    

    public delegate void CameraShakeEventHandler(float duration, float magnitude);
    public static event CameraShakeEventHandler CameraShakeEvent;

    private void ShakeCamera(float duration, float magnitude)
    {
        
        CameraShakeEvent?.Invoke(duration, magnitude);
    }

    private void HandleCameraShake(float duration, float magnitude)
    {
        StartCoroutine(Shake(duration, magnitude));
    }

    private IEnumerator Shake(float duration, float magnitude)
    {
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
    }

    private void OnEnable()
    {
        CameraShakeEvent += HandleCameraShake;
    }

    private void OnDisable()
    {
        CameraShakeEvent -= HandleCameraShake;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShakeCamera(0.5f, 0.2f);
        }

        if (transform.position.y > player.position.y && transform.position.y > win.position.y + cameraOffset)
        {
            cameraPos = new Vector3(transform.position.x, player.position.y, transform.position.z);
            transform.position = new Vector3(transform.position.x, cameraPos.y, -10);
        }
    }




    private void Awake()
    {
        player = FindObjectOfType<PlayerController>().transform;
        LevelRotation.OnWinInstantiated += LevelRotation_OnWinInstantiated;
    }

    private void LevelRotation_OnWinInstantiated(GameObject obj)
    {
        win = obj.transform;
    }

    
}
