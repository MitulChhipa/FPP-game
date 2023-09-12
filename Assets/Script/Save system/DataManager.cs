using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        File.WriteAllText(Application.persistentDataPath + "/DataFile.json", json);

        inventoryController.SaveCurrentInventory();

        string invenotoryAsJson = JsonConvert.SerializeObject(Inventory, Formatting.Indented, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });
        File.WriteAllText(Application.persistentDataPath + "/Inventory.json", invenotoryAsJson);
    }

    public void LoadData()
    {
        playerMovementCC.enabled = false;
        string json = File.ReadAllText(Application.persistentDataPath + "/DataFile.json");
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


        //string invenotoryAsJson = File.ReadAllText(Application.persistentDataPath + "/Inventory.json");
        //new1 = JsonConvert.DeserializeObject<Inventory>(invenotoryAsJson, new JsonSerializerSettings
        //{
        //    NullValueHandling = NullValueHandling.Ignore
        //});

    }
    
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

    private void ResumePlayer()
    {
        playerMovementCC.enabled = true;
    }
}
