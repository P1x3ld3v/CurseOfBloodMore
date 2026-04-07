using TMPro;
using UnityEngine;

public class QuestLogSlot : MonoBehaviour
{
    [SerializeField] private TMP_Text questNameText;
    [SerializeField] private TMP_Text questLevelText;

    public QuestSO currentQuest;
    public QuestLogUI questLogUI;

    public void SetQuest(QuestSO questSO)
    {
        currentQuest = questSO;

        if (questSO == null)
        {
            Debug.Log("SetQuest received null, clearing slot.");
            ClearSlot();
            return;
        }

        Debug.Log("QuestLogSlot showing: " + questSO.questName);

        if (questNameText != null)
            questNameText.text = questSO.questName;
        else
            Debug.LogWarning("QuestLogSlot questNameText is NOT assigned.");

        if (questLevelText != null)
            questLevelText.text = "Lv. " + questSO.questLevel;
        else
            Debug.LogWarning("QuestLogSlot questLevelText is NOT assigned.");

        gameObject.SetActive(true);
    }

    public void ClearSlot()
    {
        currentQuest = null;
        gameObject.SetActive(false);
    }

    public void OnSlotclicked()
    {
        if (currentQuest == null || questLogUI == null) return;
        questLogUI.HandleQuestClicked(currentQuest);
    }
}