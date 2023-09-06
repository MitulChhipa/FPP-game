using Unity.VisualScripting;
using UnityEngine;

public class inventoryController : MonoBehaviour
{
    [SerializeField] private Inventory _currentInventory;
    [SerializeField] private Inventory _baseInventory;
    [SerializeField] private Inventory _savedInventory;
    [SerializeField] private Transform _raycastTargetOrigin;
    [SerializeField] private float _collectingRange;
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private GameObject _inventoryContainer;
    [SerializeField] private GameObject _itemContainer;
    [SerializeField] private PlayerHealth _hp;

    private bool _isOpened;
    private Ray _ray;
    private RaycastHit _hit;

    public Slot[] inventory;

    private void Start()
    {
        _inventoryPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            targettingCollectibles();
        }
        if(Input.GetKeyDown(KeyCode.Q) && !_isOpened)
        {
            _inventoryPanel.SetActive(true);
            _isOpened = true;
            Cursor.lockState = CursorLockMode.Confined;
            OpenInventory();
        }
        else if(Input.GetKeyDown(KeyCode.Q) && _isOpened)
        {
            _inventoryPanel.SetActive(false);
            _isOpened = false;
            Cursor.lockState = CursorLockMode.Locked;
            CloseInventory();
        }
        UseItem();
    }

    private void targettingCollectibles()
    {
        _ray.origin = _raycastTargetOrigin.position;
        _ray.direction = _raycastTargetOrigin.forward;
        if (Physics.Raycast(_ray, out _hit,_collectingRange))
        {
            switch (_hit.collider.gameObject.tag)
            {
                case "Item":
                    var item = _hit.collider.gameObject.GetComponent<itemScript>().item;
                    _currentInventory.addItem(item, 1);
                    Destroy(_hit.collider.gameObject);
                    break;
                case "Computer":
                    researchScript _researchScript = _hit.collider.gameObject.GetComponent<researchScript>();
                    UseCollectibles(ref _researchScript);
                    break;
            }
        }
    }

    private void OpenInventory()
    {
        foreach (var itemSlot in _currentInventory.inventoryContainer)
        {
            var Ui = Instantiate(_itemContainer, _inventoryContainer.transform);
            var UiScript = Ui.GetComponent<SettingComponent>();
            UiScript._name.text = itemSlot.item.itemName +"\n"+ itemSlot.amount.ToString();
            UiScript._description.text = itemSlot.item.description;
            UiScript._icon.sprite = itemSlot.item.image;
        }
    }

    private void CloseInventory()
    {
        foreach (Transform child in _inventoryContainer.transform) 
        {
            Destroy(child.gameObject); 
        }
    }
    private void UseItem()
    {
        switch (Input.inputString)
        {
            case "1":
                ApplyItemEffect(0);
                break;
            case "2":
                ApplyItemEffect(1);
                break;
            case "3":
                ApplyItemEffect(2);
                break;
            case "4":
                ApplyItemEffect(3);
                break;
            case "5":
                ApplyItemEffect(4);
                break;
            case "6":
                ApplyItemEffect(5);
                break;
        }
    }

    void ApplyItemEffect(int range)
    {
        if (range >= _currentInventory.inventoryContainer.Count || _currentInventory.inventoryContainer[range].item.type != itemType.Consumable)
        {
            return;
        }

        float healthBoost = _currentInventory.inventoryContainer[range].item.restoreHealth;
        float staminaBoost = _currentInventory.inventoryContainer[range].item.restoreStamina;
        float foodBoost = _currentInventory.inventoryContainer[range].item.restoreFood;
        float waterBoost = _currentInventory.inventoryContainer[range].item.restoreWater;

        _hp.changeFood(foodBoost);
        _hp.changeWater(waterBoost);
        _hp.changeHealth(healthBoost);
        _hp.changeStamina(staminaBoost);
        if (_currentInventory.inventoryContainer[range].amount > 1)
        {
            _currentInventory.inventoryContainer[range].changeAmount(-1);
        }
        else
        {
            _currentInventory.inventoryContainer.RemoveRange(range, 1);
        }
        if (_isOpened)
        {
            ResetInventory();
        }
    }
    private void ResetInventory()
    {
        CloseInventory();
        OpenInventory();
    }
    
    private void UseCollectibles(ref researchScript x)
    {
        for(int i=0;i<_currentInventory.inventoryContainer.Count;i++)
        {
            if(_currentInventory.inventoryContainer[i].item.type == itemType.Collectible)
            {
                x.UpdateInformationData(_currentInventory.inventoryContainer[i].amount * _currentInventory.inventoryContainer[i].item.increaseKnowledgeValue);
                _currentInventory.inventoryContainer.RemoveRange(i, 1);
                return;
            }
        }
    }

    public void LoadNewGameInventory()
    {
        CopyInventory(ref _currentInventory, _baseInventory);
    }

    public void LoadSavedGameInventory()
    {
        CopyInventory(ref _currentInventory, _savedInventory);
    }
    public void SaveCurrentInventory()
    {
        CopyInventory(ref _savedInventory, _currentInventory);
    }

    private void CopyInventory(ref Inventory to,Inventory from)
    {
        to.inventoryContainer.Clear();
        for (int i = 0; i < from.inventoryContainer.Count; i++)
        {
            to.addItem(from.inventoryContainer[i].item, from.inventoryContainer[i].amount);
        }
    }
}
