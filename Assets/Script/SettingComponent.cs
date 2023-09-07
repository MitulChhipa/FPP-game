using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class SettingComponent : MonoBehaviour
{
    public TextMeshProUGUI _name;
    public TextMeshProUGUI _description;
    public Image _icon;
    public int id;
    public inventoryController inventoryController;
    public Items item;

    public void UseThisItem()
    {
        inventoryController.ApplyItemEffect(id);
    }
}
