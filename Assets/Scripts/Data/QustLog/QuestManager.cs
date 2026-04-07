using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<QuestSO, Dictionary<QuestObjective, int>> questProgress = new();
    private List<QuestSO> completedQuests = new();


    public void OnEnable()
    {
        QuestEvents.IsQuestComplete += IsQuestComplete;
    }

    public void OnDisable()
    {
        QuestEvents.IsQuestComplete -= IsQuestComplete;
    }


    public List<QuestSO> GetActiveQuests()
    {
        return new List<QuestSO>(questProgress.Keys);
    }
    public void AcceptQuest(QuestSO questSO)
    {
        if (questSO == null || questProgress.ContainsKey(questSO))
            return;

        questProgress[questSO] = new Dictionary<QuestObjective, int>();

        foreach (var objective in questSO.objectives)
        {
            questProgress[questSO][objective] = 0;
            UpdateObjectiveProgress(questSO, objective);
        }
    }

    public bool IsQuestAccepted(QuestSO questSO)
    { 
           return questProgress.ContainsKey(questSO); 
    }
    public bool IsQuestComplete(QuestSO questSO)
    {
        if (!questProgress.TryGetValue(questSO, out var progressDict))
        {
            return false;
        }

        foreach (var objective in questSO.objectives)
        {
            UpdateObjectiveProgress(questSO, objective);
        }

        foreach (var objective in questSO.objectives)
        {
            if (progressDict[objective] < objective.requiredAmount)
            {
                return false;
            }
        }
        return true;
    }

    public void CompleteQuest(QuestSO questSO)
    {
        questProgress.Remove(questSO);
        completedQuests.Add(questSO);
        foreach (var REWARD in questSO.rewards)
        {
            InventoryManager.Instance.AddItem(REWARD.itemSO, REWARD.quantity);
        }
    }

    public bool GetCompletedQuests(QuestSO questSO)
    {
        return completedQuests.Contains(questSO);
    }
    public void UpdateObjectiveProgress(QuestSO questSO, QuestObjective objective) 
    {
        if (!questProgress.ContainsKey(questSO))
            return;

        var progressDictionary = questProgress[questSO];

        int newAmount = 0;

        //if (objective.targetNPC != null && GameManager.Instance.DialogueHistoryTracker.HasSpokenWith(objective.targetNPC))
        //    newAmount = -objective.requiredAmount;
        if (objective.targetItem != null)
            newAmount = InventoryManager.Instance.GetItemQuantity(objective.targetItem);

        progressDictionary[objective] = newAmount;
    }   
   

    public string GetProgressText(QuestSO questSO, QuestObjective objective)
    {
        int currentAmount = GetCurrentAmount(questSO, objective);

        if (currentAmount >= objective.requiredAmount)
            return "Complete";


        else if (objective.targetItem != null)
            return $"{currentAmount}/{objective.requiredAmount}"; 

        else if (objective.targetNPC != null)
            return $"{currentAmount}/{objective.requiredAmount}";

        else return "In Progress";
    }

    public int GetCurrentAmount(QuestSO questSO, QuestObjective objective)
    {
        if (questProgress.TryGetValue(questSO, out var objectiveDictionary))
            if(objectiveDictionary.TryGetValue(objective, out int amount))
            return amount;
        return 0;
    }
}
