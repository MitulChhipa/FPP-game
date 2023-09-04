using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemScript : MonoBehaviour
{
    public Items item;

    private void Start()
    {
        if(item.type == itemType.Collectible)
        {
            Destroy(gameObject,15f);
        }
    }
}
