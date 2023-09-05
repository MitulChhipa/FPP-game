using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private cameraScript _cameraScript;
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private AudioSource[] _audioSources;
    private bool _paused = false;
    [SerializeField] private weaponScript[] _weaponScripts;

    private void Start()
    {
        _menuPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_paused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        _cameraScript.enabled = false;
        _paused = true;
        _menuPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        for (int i = 0; i < _weaponScripts.Length; i++)
        {
            _weaponScripts[i].enabled = false;
        }
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        _cameraScript.enabled = true;
        _paused = false;
        _menuPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        for (int i = 0; i < _weaponScripts.Length; i++)
        {
            _weaponScripts[i].enabled = true;
        }
    }
}
