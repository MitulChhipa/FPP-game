using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{

    [SerializeField] private GameObject[] _weapon;
    [SerializeField] private Image _weaponImage;
    public bool scope;
    public bool reloading;
    private int _currentWeaponCount;


    #region Mono
    private void Start()
    {
        deactivationAllWeapons();
        ActivateCurrentWeapon(_currentWeaponCount);
    }

    private void Update()
    {
        if (scope || reloading)
        {
            return;
        }
        switch (Input.mouseScrollDelta.y)
        {
            case 1:
                ChangeWeapon(-1);
                ActivateCurrentWeapon(_currentWeaponCount);
                break;
            case -1:
                ChangeWeapon(1);
                ActivateCurrentWeapon(_currentWeaponCount);
                break;
        }
    }
    #endregion

    #region WeaponFuntions
    void ChangeWeapon(int _value)
    {
        _weapon[_currentWeaponCount].SetActive(false);
        _currentWeaponCount = _currentWeaponCount + _value;
        if (_currentWeaponCount < 0)
        {
            _currentWeaponCount += _weapon.Length;
        }
        else
        {
            _currentWeaponCount %= _weapon.Length;
        }
    }
    private void ActivateCurrentWeapon(int x)
    {
        _weapon[x].SetActive(true);
        _weaponImage.sprite = _weapon[x].GetComponent<weaponScript>().weaponScriptable.image;
        UpdateCurrentWeaponUI();
    }
    public void deactivationAllWeapons()
    {
        for (int i = 0; i < _weapon.Length; i++)
        {
            _weapon[i].SetActive(false);
        }
    }
    public void UpdateCurrentWeaponUI()
    {
        _weapon[_currentWeaponCount].GetComponent<weaponScript>().UpdateUI();
    }
    public void ActivateWeapon()
    {
        ActivateCurrentWeapon(_currentWeaponCount);
    }
    #endregion
}
