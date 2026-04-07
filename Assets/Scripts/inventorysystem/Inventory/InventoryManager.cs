using UnityEngine;
using TMPro;
using System.Xml.Schema;
using System;
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public InventorySlot[] itemSlots;
    public int gold;
    public TMP_Text goldText;
    public GameObject lootPreFab;

    public Transform player;
    public static event Action<int> OnExperienceGained;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        foreach (var slot in itemSlots)
        {
            slot.UpdateUI();
        }
    }
    private void OnEnable()
    {
        Loot.OnItemLooted += AddItem;
    }

    private void OnDisable()
    {
        Loot.OnItemLooted -= AddItem;
    }

    public void AddItem(ItemSO itemSO, int quantity)
    {
        if (itemSO.isGold)
        {
            gold += quantity;
            goldText.text = gold.ToString();
            return;

        }

        if (itemSO.isXP) 
        {
            OnExperienceGained?. Invoke(quantity);
            return;
        }

        foreach (var slot1 in itemSlots)
        {
            if (slot1.itemSO == itemSO && slot1.quantity < itemSO.stackSize)
            {
                int availableSpace = itemSO.stackSize - slot1.quantity;
                int amountToAdd = Mathf.Min(availableSpace, quantity);

                slot1.quantity += amountToAdd;
                quantity -= amountToAdd;

                slot1.UpdateUI();

                if (quantity <= 0)
                    return;
            }
        }

        foreach (var slot in itemSlots)
            {
                if (slot.itemSO == null)
                {
                    int amountToAdd = Mathf.Min(itemSO.stackSize, quantity);
                    slot.itemSO = itemSO;
                    slot.quantity = quantity;
                    slot.UpdateUI();
                    return;
                }
                
            }
        if (quantity > 0)
            DropLoot(itemSO, quantity);
    }

    private void DropLoot(ItemSO itemSO, int quantity)
    {
       Loot loot = Instantiate(lootPreFab, player.position, Quaternion.identity).GetComponent<Loot>();
        loot.Initialize(itemSO, quantity);
    }

    public bool HasItem(ItemSO itemSO)
    {
        foreach (var slot in itemSlots)
        {
            if(slot.itemSO == itemSO && slot.quantity > 0)
                return true;
        }
        return false;
    }

    public int GetItemQuantity(ItemSO itemSO) 
    {
        int total = 0;
        foreach (var slot in itemSlots)
        {

            if (slot.itemSO = itemSO)
            {
                total += slot.quantity;
            }
           
        }
        return total;
    }

    public void AddGold(int amount)
    {
        gold += amount;
        goldText.text = gold.ToString();
    }
}
