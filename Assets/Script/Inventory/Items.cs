using UnityEngine;
using UnityEngine.UI;

public enum itemType
{
    MeleeWeapon,
    ShootingWeapon,
    Consumable,
    Collectible
}

public abstract class Items : ScriptableObject
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
}
