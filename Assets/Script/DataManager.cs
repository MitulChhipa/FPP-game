using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public PlayerHealth hp;
    public Transform player;
    public Inventory inventory;
    public WeaponManager weaponManager;
    public playerMovementCC playerMovementCC;
    public researchScript research;
    public WeaponScriptable pistol;
    public WeaponScriptable m4;


    public void SaveData()
    {
        DataHandle dataHandle = new DataHandle();
        dataHandle.health = hp.health;
        
        dataHandle.slots = new List<InventorySlot>();
        dataHandle.slots.AddRange(inventory.inventoryContainer);
        
        dataHandle.waterLevel = hp.water;
        dataHandle.foodLevel = hp.food;
        dataHandle.position = player.position;
        dataHandle.rotation = player.rotation;
        dataHandle.totalPistolAmmo = pistol.totalAmmo;
        dataHandle.totalM4Ammo = m4.totalAmmo;
        dataHandle.researchDataValue = research.informationValue;
        dataHandle.pistolAmmo = pistol.currentAmmo;
        dataHandle.m4Ammo = m4.currentAmmo;

        string json = JsonUtility.ToJson(dataHandle,true);
        File.WriteAllText(Application.dataPath + "/DataFile.json", json);
    }

    public void LoadData()
    {
        playerMovementCC.enabled = false;
        string json = File.ReadAllText(Application.dataPath + "/DataFile.json");
        DataHandle dataHandle = JsonUtility.FromJson<DataHandle>(json);

        hp.health = dataHandle.health;
        hp.water = dataHandle.waterLevel;
        hp.food = dataHandle.foodLevel;

        inventory.inventoryContainer.Clear();
        inventory.inventoryContainer.AddRange(dataHandle.slots);

        player.position = dataHandle.position; 
        player.rotation = dataHandle.rotation;
        m4.totalAmmo = dataHandle.totalM4Ammo;
        pistol.totalAmmo = dataHandle.totalPistolAmmo;
        pistol.currentAmmo = dataHandle.pistolAmmo;
        m4.currentAmmo = dataHandle.m4Ammo;
        research.ResetValues(dataHandle.researchDataValue);
        hp.UpdateAllHpUi();
        weaponManager.UpdateCurrentWeaponUI();
        Invoke("ResumePlayer", 0.5f);
    }
    private void ResumePlayer()
    {
        playerMovementCC.enabled = true;
    }
}
