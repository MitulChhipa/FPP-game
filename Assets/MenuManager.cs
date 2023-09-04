using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private cameraScript _cameraScript;
    private bool _paused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_paused)
            {
                Time.timeScale = 0f;
                _cameraScript.enabled = false;
                _paused = true;
            }
            else
            {
                Time.timeScale = 1f;
                _cameraScript.enabled = true;
                _paused = false;
            }
        }
    }
}
