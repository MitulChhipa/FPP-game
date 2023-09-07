using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class InventoryJS
{
    public List<Slot> Inventory;

    public void SetValuesInventory(in Inventory inventoryScriptable)
    {
        for(int i = 0; i< inventoryScriptable.inventoryContainer.Count; i++)
        {
            Inventory[i].SetValuesSlot(inventoryScriptable.inventoryContainer[i]);   
        }
    }
}

[System.Serializable]
public class Slot
{
    public ItemJS itemJS;
    public int amount;

    public void SetValuesSlot(in InventorySlot x)
    {
        itemJS.SetValuesItem(in x.item);
        amount = x.amount;
    }
}

[System.Serializable]
public class ItemJS
{
    public itemType type;
    public Sprite image;
    public string description;
    public string itemName;

    //for consumables;
    public float restoreHealth;
    public float restoreFood;
    public float restoreStamina;
    public float restoreWater;

    //for collectibles;
    public float increaseKnowledgeValue;

    public void SetValuesItem(in Items x)
    {
        type = x.type;
        image = x.image;
        description = x.description;
        itemName = x.itemName;
        restoreHealth = x.restoreHealth;
        restoreFood = x.restoreFood;
        restoreStamina = x.restoreStamina;
        restoreWater = x.restoreWater;
        increaseKnowledgeValue = x.increaseKnowledgeValue;
    }
}
