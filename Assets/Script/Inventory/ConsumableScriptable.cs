using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory System/Items/Consumable")]
public class ConsumableScriptable : Items
{
    private void Awake()
    {
        type = itemType.Consumable;
    }
}
