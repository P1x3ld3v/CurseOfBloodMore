using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    private Inventory_Base inventory;
    private UI_ItemSlot[] uiItemSlots;


    private void Awake()
    {
        uiItemSlots = GetComponentsInChildren<UI_ItemSlot>();

        inventory = FindFirstObjectByType<Inventory_Base>();
        inventory.OnInventoryChange += UpdateInventorySlots;

    }

    private void UpdateInventorySlots()
    {
        List<Inventory_Item> itemsList = inventory.itemList;

        for (int i = 0; i < uiItemSlots.Length; i++)
        {
            if (i < itemsList.Count)
            {
                uiItemSlots[i].UpdateSlot(itemsList[i]);
            }
            else
            {
                uiItemSlots[i].UpdateSlot(null);
            }

        }
    }

  
}
