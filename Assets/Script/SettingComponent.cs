using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class SettingComponent : MonoBehaviour
{
    public TextMeshProUGUI _name;
    public TextMeshProUGUI _description;
    public Image _icon;
    public inventoryController inventoryController;
    public int id;

    public void UseThisItem()
    {
        inventoryController.ApplyItemEffect(id);
    }
}
