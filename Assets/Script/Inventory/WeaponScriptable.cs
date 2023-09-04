using UnityEngine;

[CreateAssetMenu(fileName ="New Weapon",menuName ="Inventory System/Items/Weapon")]
public class WeaponScriptable : Items
{
    public int currentAmmo;
    public int maxAmmo;
    public int totalAmmo;
    public float fireTime;
    public float fireRange;
    public float damage;
    public float reloadTime;
}
