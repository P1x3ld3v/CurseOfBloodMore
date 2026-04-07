using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [Header("UI References")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image portrait;
    [SerializeField] private TMP_Text actorNameText;
    [SerializeField] private TMP_Text dialogueText;

    [Header("Keep these animators playing while paused")]
    [SerializeField] private Animator[] animatorsToKeepPlaying;

    public bool isDialogueActive { get; private set; }

    private DialogueSO currentDialogue;
    private int dialogueIndex;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        HideUI();
        isDialogueActive = false;
        currentDialogue = null;
        dialogueIndex = 0;
    }

    public void StartDialogue(DialogueSO dialogueSO)
    {
        if (dialogueSO == null)
        {
            Debug.LogWarning("DialogueManager: StartDialogue called with null DialogueSO.");
            return;
        }

        if (dialogueSO.lines == null || dialogueSO.lines.Length == 0)
        {
            Debug.LogWarning($"DialogueManager: Dialogue '{dialogueSO.name}' has no lines.");
            return;
        }

        currentDialogue = dialogueSO;
        dialogueIndex = 0;
        isDialogueActive = true;

        PauseGameKeepIdleAnims(true);
        ShowUI();
        ShowCurrentLine();
    }

    public void AdvanceDialogue()
    {
        if (!isDialogueActive || currentDialogue == null)
            return;

        dialogueIndex++;

        if (dialogueIndex >= currentDialogue.lines.Length)
        {
            EndDialogue();
            return;
        }

        ShowCurrentLine();
    }

    private void ShowCurrentLine()
    {
        if (currentDialogue == null)
            return;

        if (dialogueIndex < 0 || dialogueIndex >= currentDialogue.lines.Length)
            return;

        DialogueLine line = currentDialogue.lines[dialogueIndex];

        if (line.speaker != null)
        {
            if (portrait != null)
                portrait.sprite = line.speaker.potrait;

            if (actorNameText != null)
                actorNameText.text = line.speaker.actorName;
        }
        else
        {
            if (portrait != null)
                portrait.sprite = null;

            if (actorNameText != null)
                actorNameText.text = "";
        }

        if (dialogueText != null)
            dialogueText.text = line.text;
    }

    private void EndDialogue()
    {
        QuestSO questToOffer = null;

        if (currentDialogue != null)
            questToOffer = currentDialogue.offerQuestOnEnd;

        currentDialogue = null;
        dialogueIndex = 0;
        isDialogueActive = false;

        HideUI();
        PauseGameKeepIdleAnims(false);

        if (questToOffer != null)
        {
            QuestEvents.OnQuestOfferRequested?.Invoke(questToOffer);
        }
    }

    private void ShowUI()
    {
        if (canvasGroup == null) return;

        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    private void HideUI()
    {
        if (canvasGroup == null) return;

        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void PauseGameKeepIdleAnims(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0f;

            if (animatorsToKeepPlaying != null)
            {
                for (int i = 0; i < animatorsToKeepPlaying.Length; i++)
                {
                    if (animatorsToKeepPlaying[i] != null)
                        animatorsToKeepPlaying[i].updateMode = AnimatorUpdateMode.UnscaledTime;
                }
            }
        }
        else
        {
            Time.timeScale = 1f;

            if (animatorsToKeepPlaying != null)
            {
                for (int i = 0; i < animatorsToKeepPlaying.Length; i++)
                {
                    if (animatorsToKeepPlaying[i] != null)
                        animatorsToKeepPlaying[i].updateMode = AnimatorUpdateMode.Normal;
                }
            }
        }
    }
}