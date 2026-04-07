using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class QuestRewardSlot : MonoBehaviour
{
    public Image rewardImage;
    public TMP_Text rewardQuantity;

    public void DisplayReward(Sprite sprite, int quantity)
    {
        if (rewardImage != null)
            rewardImage.sprite = sprite;

        if (rewardQuantity != null)
            rewardQuantity.text = quantity.ToString();
    }
}