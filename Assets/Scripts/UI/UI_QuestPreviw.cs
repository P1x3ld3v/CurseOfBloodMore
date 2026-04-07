using TMPro;
using UnityEngine;

public class UI_QuestPreviw : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questName;
    [SerializeField] private TextMeshProUGUI questDescription;
    [SerializeField] private TextMeshProUGUI questGoal;
   // [SerializeField] private UI_QuestRewardSlot[] questReward;

    [SerializeField] private GameObject[] additionalObjects;
    private QuestDataSO previwQuest;

    public void SetupQuestPreviw(QuestDataSO questDataSO)
    {
        EnableAdditonalObjects(true);
       // EnableQuestRewardObjects(false);

        questName.text = questDataSO.questName;
        questDescription.text = questDataSO.description;
        questGoal.text = questDataSO.questGoal + " " + questDataSO.requiredAmount;
        
    }

    public void AcceptQuestBTN()
    {
        MakeQuestPreviwEmpty();
    }

    public void MakeQuestPreviwEmpty()
    {
        questName.text = "";
        questDescription.text = "";

        EnableAdditonalObjects(false);
       // EnableQuestRewardObjects(false);
    }

    private void EnableAdditonalObjects(bool enable)
    {
        foreach (var obj in additionalObjects)
            obj.SetActive(enable);
    }

    //private void EnableQuestRewardObjects(bool enable)
    //{
    //    foreach (var obj in questReward)
    //        obj.gameObject.SetActive(enable);
    //}
}
