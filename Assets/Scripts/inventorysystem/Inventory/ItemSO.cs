using UnityEngine;

[CreateAssetMenu(fileName = "New Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    [TextArea] public string itemDesc;
    public Sprite icon;

    public bool isGold;
    public bool isXP;
    public int stackSize = 3;

    [Header("Stats")]
    public int currentHealth;
    public int maxHealth;

    [Header("For Temporary Items")]
    public float duration;
}
