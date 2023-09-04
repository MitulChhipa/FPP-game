using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataHandle
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
    public List<InventorySlot> slots;
}
