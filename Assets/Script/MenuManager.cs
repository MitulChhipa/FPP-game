using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private cameraScript _cameraScript;
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private AudioSource[] _audioSources;
    [SerializeField] private weaponScript[] _weaponScripts;
    [SerializeField] private WeaponManager _weaponManager;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject[] _other;
    [SerializeField] private playerMovementCC _playerMovementCC;
    [SerializeField] private EnemyManager enemyManager;

    public bool canActiveMenu;
    
    private bool _paused = false;

    private void Start()
    {
        _menuPanel.SetActive(false);
        _winPanel.SetActive(false);
        ActiveMainMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_weaponManager.scope && canActiveMenu)
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
        canActiveMenu = true;
        _cameraScript.enabled = true;
        _paused = false;
        _gameOverPanel.SetActive(false);    
        _menuPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        _weaponManager.enabled = true;
        _weaponManager.ActivateWeapon();
        for (int i = 0; i < _weaponScripts.Length; i++)
        {
            _weaponScripts[i].enabled = true;
        }
    }

    public void ActiveMainMenu()
    {
        enemyManager.ResetAllEnemies();
        _weaponManager.enabled = true;
        Time.timeScale = 1f;
        _mainMenu.SetActive(true);
        _playerMovementCC.enabled = false;
        for (int i = 0; i < _other.Length; i++)
        {
            _other[i].SetActive(false);
        }
    }
    public void DeactivateMainMenu()
    {
        _mainMenu.SetActive(false );
        _playerMovementCC.enabled = false;
        for (int i = 0; i < _other.Length; i++)
        {
            _other[i].SetActive(true);
        }
        Resume();
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        canActiveMenu = false;
        _playerMovementCC.enabled = false;
        _gameOverPanel.SetActive(true);
        _weaponManager.deactivationAllWeapons();
        _weaponManager.enabled = false;
        Cursor.lockState = CursorLockMode.Confined;
        _cameraScript.enabled = false;
    }
    public void Win()
    {
        canActiveMenu = false;
        _playerMovementCC.enabled = false;
        _winPanel.SetActive(true);
        _weaponManager.deactivationAllWeapons();
        _weaponManager.enabled = false;
        Cursor.lockState = CursorLockMode.Confined;
        _cameraScript.enabled=false;
    }
}
