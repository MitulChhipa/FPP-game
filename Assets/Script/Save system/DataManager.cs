using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public PlayerHealth hp;
    public Transform player;
    public PlayerInitialInfo initialInfo;
    public WeaponManager weaponManager;
    public playerMovementCC playerMovementCC;
    public researchScript research;
    public WeaponScriptable pistol;
    public WeaponScriptable m4;
    public inventoryController inventoryController;
    public Inventory Inventory;
    [SerializeField] private CollectiblesScript _collectibles;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.N))
        {
            NewGameData();
        }
    }

    public void SaveData()
    {
        DataHandle dataHandle = new DataHandle();
        dataHandle.health = hp.health;

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

        //string jsonInventory = JsonUtility.ToJson(inventoryController._inventoryJS,true);
        //File.WriteAllText(Application.dataPath + "/Inventory.json", jsonInventory);

        inventoryController.SaveCurrentInventory();
    }

    public void LoadData()
    {
        playerMovementCC.enabled = false;
        string json = File.ReadAllText(Application.dataPath + "/DataFile.json");
        DataHandle dataHandle = JsonUtility.FromJson<DataHandle>(json);

        hp.health = dataHandle.health;
        hp.water = dataHandle.waterLevel;
        hp.food = dataHandle.foodLevel;
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
        inventoryController.LoadSavedGameInventory();
        _collectibles.ResetCollectibles();
    }
    //public void NewGameLoad()
    //{
    //    playerMovementCC.enabled = false;
    //    string json = File.ReadAllText(Application.dataPath + "/NewGameDataFile.json");
    //    DataHandle dataHandle = JsonUtility.FromJson<DataHandle>(json);

    //    hp.health = dataHandle.health;
    //    hp.water = dataHandle.waterLevel;
    //    hp.food = dataHandle.foodLevel;
    //    hp.stamina = 100f;

    //    player.position = dataHandle.position;
    //    player.rotation = dataHandle.rotation;
    //    m4.totalAmmo = dataHandle.totalM4Ammo;
    //    pistol.totalAmmo = dataHandle.totalPistolAmmo;
    //    pistol.currentAmmo = dataHandle.pistolAmmo;
    //    m4.currentAmmo = dataHandle.m4Ammo;
    //    research.ResetValues(dataHandle.researchDataValue);
    //    hp.UpdateAllHpUi();
    //    weaponManager.UpdateCurrentWeaponUI();
    //    Invoke("ResumePlayer", 0.5f);
    //}
    public void StartNewGame()
    {
        playerMovementCC.enabled = false;
        hp.health = initialInfo.health;
        hp.water = initialInfo.waterLevel;
        hp.food = initialInfo.foodLevel;

        player.position = initialInfo.position;
        player.rotation = initialInfo.rotation;
        m4.totalAmmo = initialInfo.totalM4Ammo;
        pistol.totalAmmo = initialInfo.totalPistolAmmo;
        pistol.currentAmmo = initialInfo.pistolAmmo;
        m4.currentAmmo = initialInfo.m4Ammo;
        research.ResetValues(initialInfo.researchDataValue);
        hp.UpdateAllHpUi();
        weaponManager.UpdateCurrentWeaponUI();
        Invoke("ResumePlayer", 0.5f);
        inventoryController.LoadNewGameInventory();
        _collectibles.ResetCollectibles();
    }

    void NewGameData()
    {
        DataHandle dataHandle = new DataHandle();
        dataHandle.health = 100f;
        dataHandle.waterLevel = 100f;
        dataHandle.foodLevel = 100f;
        dataHandle.position = player.position;
        dataHandle.rotation = player.rotation;

        dataHandle.totalPistolAmmo = pistol.totalAmmo;
        dataHandle.totalM4Ammo = m4.totalAmmo;
        dataHandle.researchDataValue = research.informationValue;
        dataHandle.pistolAmmo = pistol.currentAmmo;
        dataHandle.m4Ammo = m4.currentAmmo;

        string json = JsonUtility.ToJson(dataHandle, true);
        File.WriteAllText(Application.dataPath + "/NewGameDataFile.json", json);
    }

    private void ResumePlayer()
    {
        playerMovementCC.enabled = true;
    }
}
