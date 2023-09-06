using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New info", menuName = "Player/Info")]
public class PlayerInitialInfo : ScriptableObject
{
    public int pistolAmmo;
    public int m4Ammo;

    public int totalPistolAmmo;
    public int totalM4Ammo;

    public float health;
    public float foodLevel;
    public float waterLevel;

    public float researchDataValue;

    public Vector3 position;
    public Quaternion rotation;
}
