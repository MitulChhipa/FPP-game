using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Collectible", menuName = "Inventory System/Items/Collectible")]
public class Collectibles : Items
{
    private void Awake()
    {
        type = itemType.Collectible;
    }
}
