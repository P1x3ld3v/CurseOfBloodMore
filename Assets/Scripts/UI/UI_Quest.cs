using UnityEngine;

public class UI_Quest : MonoBehaviour
{

    [SerializeField] private UI_QuestPreviw questPreview;
    private UI_QuestSlot[] questSlots;

    private void Awake()
    {
        questSlots = GetComponentsInChildren<UI_QuestSlot>(true);
    }

    public void SetupQuestUI(QuestDataSO[] questsToSetup)
    {
        foreach(var slot in questSlots)
            slot.gameObject.SetActive(false);

        for (int i = 0; i < questsToSetup.Length; i++)
        {
            questSlots[i].gameObject.SetActive(true);
            questSlots[i].SetupQuestSlot(questsToSetup[i]);
        }

        questPreview.MakeQuestPreviwEmpty();
    }

    public UI_QuestPreviw GetQuestPreview() => questPreview;
}
