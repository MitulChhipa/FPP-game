using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


[CreateAssetMenu(fileName ="New Inventory",menuName ="Inventory System/Inventory")]
public class Inventory : ScriptableObject
{
    public List<InventorySlot> inventoryContainer;
    public void addItem(Items _item,int _amount)
    {
        bool hasItem = false;
        for(int i=0;  i<inventoryContainer.Count; i++)
        {
            if (inventoryContainer[i].item == _item)
            {
                inventoryContainer[i].changeAmount(_amount);
                hasItem = true;
                break;
            }
        }
        if (!hasItem)
        {
            inventoryContainer.Add(new InventorySlot(_item,_amount));
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public Items item;
    public int amount;
    public InventorySlot(Items _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }
    public void changeAmount(int value)
    {
        amount = amount + value;
    }
}
