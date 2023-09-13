using TMPro;
using UnityEngine;

public class weaponScript : MonoBehaviour
{

    [SerializeField] private Transform _raycastTargetOrigin;
    [SerializeField] private ParticleSystem _muzzleFlash;
    [SerializeField] private ParticleSystem _fireImpact;
    [SerializeField] private ParticleSystem _fleshImpact;
    [SerializeField] private AudioSource _fireSound;
    [SerializeField] private TextMeshProUGUI _ammoCount;
    [SerializeField] private TextMeshProUGUI _totalAmmoCount;
    [SerializeField] private Animator _animator;
    [SerializeField] private Animator _fireArmAnimator;
    [SerializeField] private cameraScript _cameraScript;
    [SerializeField] private GameObject _crossHair;
    [SerializeField] private WeaponManager _weaponManager;
    [SerializeField] private AudioSource click;
    
    public WeaponScriptable weaponScriptable;

    private Ray _ray;
    private RaycastHit _hit;
    private bool _isFiring;
    private float _firingTime = 0f;


    #region Mono
    private void Start()
    {
        UpdateAmmo();
        _totalAmmoCount.text = weaponScriptable.totalAmmo.ToString();
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0) && !_weaponManager.reloading)
        {
            startFire();
        }
        _isFiring = Input.GetMouseButton(0);
        if (_isFiring && !_weaponManager.reloading)
        {
            _firingTime = _firingTime + Time.deltaTime;
            if(_firingTime > weaponScriptable.fireTime)
            {
                startFire();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.R) && !_weaponManager.reloading && (weaponScriptable.currentAmmo <weaponScriptable.totalAmmo) && (weaponScriptable.totalAmmo != 0))
        {
            reload();
        }
        cameraScope();
    }
    #endregion

    #region Fire
    private void startFire()
    {
        _firingTime = 0;
        switch (weaponScriptable.type)
        {
            case itemType.MeleeWeapon:
                MeleeWeaponFire();
                break;
            case itemType.ShootingWeapon:
                ShootingWeaponFire();
                break;
        }
    }
    private void MeleeWeaponFire()
    {
        _fireArmAnimator.SetTrigger("Shoot");

        _ray.origin = _raycastTargetOrigin.position;
        _ray.direction = _raycastTargetOrigin.forward;
        _fireSound.Play();
        if (Physics.Raycast(_ray, out _hit, weaponScriptable.fireRange))
        {
            switch (_hit.collider.gameObject.tag)
            {
                case "Enemy":
                    _hit.collider.gameObject.GetComponent<EnemyScript>().bulletHit(weaponScriptable.damage);
                    ParticleEffectEmission(_fleshImpact, _hit);
                    break;
                case "Player":
                    break;
            }
        }
    }
    private void ShootingWeaponFire()
    {
        if(weaponScriptable.totalAmmo == 0 && weaponScriptable.currentAmmo == 0)
        {
            click.Play();
            return;
        }
        if (weaponScriptable.currentAmmo == 0 && !_weaponManager.reloading)
        {
            click.Play();
            reload();
            return;
        }

        _animator.SetTrigger("Shoot 0");
        _fireArmAnimator.SetTrigger("Shoot");
        weaponScriptable.currentAmmo = weaponScriptable.currentAmmo - 1;
        UpdateAmmo();
        _muzzleFlash.transform.Rotate(0f, 0f, (Random.Range(0f, 180f)));
        _muzzleFlash.Play();
        _fireSound.Play();
        _ray.origin = _raycastTargetOrigin.position;
        _ray.direction = _raycastTargetOrigin.forward;
        if (Physics.Raycast(_ray, out _hit, weaponScriptable.fireRange))
        {
            switch (_hit.collider.gameObject.tag)
            {
                case "Enemy":
                    _hit.collider.gameObject.GetComponent<EnemyScript>().bulletHit(weaponScriptable.damage);
                    ParticleEffectEmission(_fleshImpact, _hit);
                    break;
                case "EnemyBody":
                    _hit.collider.gameObject.GetComponent<Rigidbody>().AddForce(_hit.normal * -500f);
                    ParticleEffectEmission(_fleshImpact, _hit);

                    break;
                case "Player":
                    break;
                case "Computer":
                    break;
                default:
                    ParticleEffectEmission(_fireImpact, _hit);
                    break;
            }
        }
    }
    void ParticleEffectEmission(ParticleSystem x , RaycastHit y)
    {
        x.transform.position = y.point;
        x.transform.forward = y.normal;
        x.Emit(1);
    }
    #endregion

    #region Reload
    private void reload()
    {
        _weaponManager.reloading = true;
        _weaponManager.scope = false;

        _crossHair.SetActive(true);
        _fireArmAnimator.SetBool("Scope", false);
        _animator.SetBool("Scope", false);
        _animator.ResetTrigger("Shoot 0");
        _animator.ResetTrigger("Shoot 1");
        _fireArmAnimator.SetTrigger("Reload");
        _fireArmAnimator.ResetTrigger("Shoot");

        Invoke("stopReload", weaponScriptable.reloadTime);
    }

    private void stopReload()
    {
        if(weaponScriptable.totalAmmo > 0)
        {
            int temp;
            temp = weaponScriptable.maxAmmo - weaponScriptable.currentAmmo;
            if (temp <= weaponScriptable.totalAmmo)
            {
                weaponScriptable.currentAmmo = weaponScriptable.currentAmmo + temp;
                weaponScriptable.totalAmmo -= temp;
            }
            else
            {
                weaponScriptable.currentAmmo = weaponScriptable.currentAmmo + weaponScriptable.totalAmmo;
                weaponScriptable.totalAmmo -= weaponScriptable.totalAmmo;
            }
        }

        _weaponManager.reloading = false;
        _totalAmmoCount.text = weaponScriptable.totalAmmo.ToString();
        UpdateAmmo();
    }
    #endregion

    #region Scope
    private void cameraScope()
    {
        if (Input.GetMouseButtonDown(1) && !_weaponManager.reloading /*&& !_weaponManager.scope*/ && weaponScriptable.type == itemType.ShootingWeapon)
        {
            _fireArmAnimator.SetBool("Scope", true);
            _animator.SetBool("Scope", true);
            _weaponManager.scope = true;
            _crossHair.SetActive(false);
        }
        else if (Input.GetMouseButtonUp(1) && _weaponManager.scope)
        {
            _fireArmAnimator.SetBool("Scope", false);
            _animator.SetBool("Scope", false);
            _weaponManager.scope = false;
            _crossHair.SetActive(true);
        }
    }
    #endregion

    #region UIUpdates
    public void UpdateAmmo()
    {
        _ammoCount.text = weaponScriptable.currentAmmo.ToString();
    }
    public void UpdateUI()
    {
        switch (weaponScriptable.type)
        {
            case itemType.MeleeWeapon:
                _totalAmmoCount.text = "∞";
                _ammoCount.text = "∞";
                break;
            case itemType.ShootingWeapon: 
                _totalAmmoCount.text = weaponScriptable.totalAmmo.ToString();
                _ammoCount.text = weaponScriptable.currentAmmo.ToString();
                break;
        }
    }
    #endregion
}
