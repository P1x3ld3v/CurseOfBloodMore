using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class QuestLogUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private QuestManager questManager;
    [SerializeField] private TMP_Text questNameText;
    [SerializeField] private TMP_Text questDescriptionText;
    [SerializeField] private QuestObjectiveSlot[] objectiveSlots;
    [SerializeField] private QuestRewardSlot[] rewardSlots;
    [SerializeField] private QuestLogSlot[] questSlots;

    [Header("Quest Assets")]
    [SerializeField] private QuestSO noAvailableQuestSO;

    [Header("Canvas Groups")]
    [SerializeField] private CanvasGroup questCanvas;
    [SerializeField] private CanvasGroup acceptCanvasGroup;
    [SerializeField] private CanvasGroup declineCanvasGroup;
    [SerializeField] private CanvasGroup completeCanvasGroup;

    private QuestSO questSO;

    private void OnEnable()
    {
        QuestEvents.OnQuestOfferRequested += ShowQuestOffer;
        QuestEvents.OnQuestturnInRequested += ShowQuestTurnIn;
    }

    private void OnDisable()
    {
        QuestEvents.OnQuestOfferRequested -= ShowQuestOffer;
        QuestEvents.OnQuestturnInRequested -= ShowQuestTurnIn;
    }

    private void Start()
    {
        questSO = null;

        SetCanvasState(questCanvas, false);
        SetCanvasState(acceptCanvasGroup, false);
        SetCanvasState(declineCanvasGroup, false);
        SetCanvasState(completeCanvasGroup, false);

        ClearQuestDetails();
        RefreshQuestList();
    }

    private void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            if (DialogueManager.instance != null && DialogueManager.instance.isDialogueActive)
                return;

            bool showingOfferOrTurnIn =
                acceptCanvasGroup.alpha > 0 ||
                completeCanvasGroup.alpha > 0;

            if (showingOfferOrTurnIn)
                return;

            ToggleQuestLog();
        }
    }

    private void ToggleQuestLog()
    {
        bool isOpen = questCanvas.alpha > 0;
        SetCanvasState(questCanvas, !isOpen);

        if (!isOpen)
            RefreshQuestList();
    }

    public void ShowQuestOffer(QuestSO incomingQuestSO)
    {
        if (incomingQuestSO == null) return;

        bool alreadyAccepted = questManager.IsQuestAccepted(incomingQuestSO);
        bool alreadyCompleted = questManager.GetCompletedQuests(incomingQuestSO);

        if (alreadyAccepted || alreadyCompleted)
        {
            questSO = noAvailableQuestSO;
            SetCanvasState(acceptCanvasGroup, false);
            SetCanvasState(declineCanvasGroup, true);
            SetCanvasState(completeCanvasGroup, false);
        }
        else
        {
            questSO = incomingQuestSO;
            SetCanvasState(acceptCanvasGroup, true);
            SetCanvasState(declineCanvasGroup, true);
            SetCanvasState(completeCanvasGroup, false);
        }

        HandleQuestClicked(questSO);
        SetCanvasState(questCanvas, true);
    }

    public void ShowQuestTurnIn(QuestSO incomingQuestSO)
    {
        if (incomingQuestSO == null) return;

        questSO = incomingQuestSO;

        HandleQuestClicked(questSO);
        SetCanvasState(completeCanvasGroup, true);
        SetCanvasState(acceptCanvasGroup, false);
        SetCanvasState(declineCanvasGroup, false);
        SetCanvasState(questCanvas, true);
    }

    public void OnAcceptQuestClicked()
    {
        if (questSO == null || questSO == noAvailableQuestSO)
            return;

        questManager.AcceptQuest(questSO);
        RefreshQuestList();

        SetCanvasState(acceptCanvasGroup, false);
        SetCanvasState(declineCanvasGroup, false);
        SetCanvasState(completeCanvasGroup, false);

        HandleQuestClicked(questSO);
    }

    public void OnDeclineQuestClicked()
    {
        SetCanvasState(acceptCanvasGroup, false);
        SetCanvasState(declineCanvasGroup, false);
        SetCanvasState(completeCanvasGroup, false);
        SetCanvasState(questCanvas, false);
        ClearQuestDetails();
    }

    public void OnCompleteQuestClicked()
    {
        if (questSO == null) return;

        questManager.CompleteQuest(questSO);
        RefreshQuestList();

        SetCanvasState(completeCanvasGroup, false);
        SetCanvasState(acceptCanvasGroup, false);
        SetCanvasState(declineCanvasGroup, false);

        ClearQuestDetails();
    }

    private void SetCanvasState(CanvasGroup group, bool activate)
    {
        if (group == null) return;

        group.alpha = activate ? 1 : 0;
        group.blocksRaycasts = activate;
        group.interactable = activate;
    }

    public void RefreshQuestList()
    {
        List<QuestSO> activeQuests = questManager.GetActiveQuests();

        for (int i = 0; i < questSlots.Length; i++)
        {
            if (i < activeQuests.Count)
            {
                questSlots[i].SetQuest(activeQuests[i]);
            }
            else
            {
                questSlots[i].ClearSlot();
            }
        }
    }

    public void HandleQuestClicked(QuestSO selectedQuest)
    {
        if (selectedQuest == null)
        {
            ClearQuestDetails();
            return;
        }

        questSO = selectedQuest;
        questNameText.text = questSO.questName;
        questDescriptionText.text = questSO.questDescription;

        DisplayObjectives();
        DisplayRewards();
    }

    private void ClearQuestDetails()
    {
        questSO = null;

        if (questNameText != null)
            questNameText.text = "";

        if (questDescriptionText != null)
            questDescriptionText.text = "";

        for (int i = 0; i < objectiveSlots.Length; i++)
        {
            if (objectiveSlots[i] != null)
                objectiveSlots[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < rewardSlots.Length; i++)
        {
            if (rewardSlots[i] != null)
                rewardSlots[i].gameObject.SetActive(false);
        }
    }

    private void DisplayObjectives()
    {
        if (questSO == null)
        {
            for (int i = 0; i < objectiveSlots.Length; i++)
                objectiveSlots[i].gameObject.SetActive(false);

            return;
        }

        for (int i = 0; i < objectiveSlots.Length; i++)
        {
            if (i < questSO.objectives.Count)
            {
                var objective = questSO.objectives[i];

                int currentAmount = 0;
                string progress = "0/" + objective.requiredAmount;
                bool isComplete = false;

                if (questManager.IsQuestAccepted(questSO))
                {
                    questManager.UpdateObjectiveProgress(questSO, objective);
                    currentAmount = questManager.GetCurrentAmount(questSO, objective);
                    progress = questManager.GetProgressText(questSO, objective);
                    isComplete = currentAmount >= objective.requiredAmount;
                }

                objectiveSlots[i].gameObject.SetActive(true);
                objectiveSlots[i].RefreshObjectives(objective.description, progress, isComplete);
            }
            else
            {
                objectiveSlots[i].gameObject.SetActive(false);
            }
        }
    }

    private void DisplayRewards()
    {
        if (questSO == null)
        {
            for (int i = 0; i < rewardSlots.Length; i++)
                rewardSlots[i].gameObject.SetActive(false);

            return;
        }

        for (int i = 0; i < rewardSlots.Length; i++)
        {
            if (i < questSO.rewards.Count)
            {
                var reward = questSO.rewards[i];

                rewardSlots[i].DisplayReward(reward.itemSO.icon, reward.quantity);
                rewardSlots[i].gameObject.SetActive(true);
            }
            else
            {
                rewardSlots[i].gameObject.SetActive(false);
            }
        }
    }
}