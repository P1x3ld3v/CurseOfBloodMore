using UnityEngine;
using UnityEngine.InputSystem;

public class Object_NPC : MonoBehaviour
{
    protected Transform player;
    protected UI ui;

    [SerializeField] private Transform npc;
    [SerializeField] private GameObject interactToolTip;
    [SerializeField] private Animator animator;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private string paramName = "PlayerNear";
    private bool playerInRange;

    [Header("Floaty Tooltip")]
    [SerializeField] private float floatSpeed = 8f;
    [SerializeField] private float floatRange = .1f;
    private Vector3 startPosition;

    [Header("Dialogue")]
    [SerializeField] private DialogueSO defaultDialogue;
    [SerializeField] private DialogueSO inProgressDialogue;
    [SerializeField] private DialogueSO completedDialogue;

    [Header("Quest")]
    [SerializeField] private QuestSO npcQuest;
    [SerializeField] private QuestManager questManager;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip playerNearSound;
    [SerializeField] private AudioClip interactSound;
    [SerializeField] private AudioClip advanceDialogueSound;

    protected virtual void Awake()
    {
        ui = FindFirstObjectByType<UI>();
        startPosition = interactToolTip.transform.position;
        interactToolTip.SetActive(false);

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        HandleToolTipFloat();

        if (!playerInRange) return;
        if (Keyboard.current == null) return;

        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            if (DialogueManager.instance.isDialogueActive)
            {
                PlaySound(advanceDialogueSound);
                DialogueManager.instance.AdvanceDialogue();
            }
            else
            {
                PlaySound(interactSound);
                Interact();
            }
        }
    }

    private void HandleToolTipFloat()
    {
        if (interactToolTip.activeSelf)
        {
            float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatRange;
            interactToolTip.transform.position = startPosition + new Vector3(0, yOffset, 0);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(playerTag)) return;

        player = collision.transform;
        playerInRange = true;
        interactToolTip.SetActive(true);

        PlaySound(playerNearSound);

        if (animator != null)
            animator.SetBool(paramName, true);
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag(playerTag)) return;

        playerInRange = false;
        interactToolTip.SetActive(false);

        if (animator != null)
            animator.SetBool(paramName, false);
    }

    private void Interact()
    {
        if (npcQuest == null || questManager == null)
        {
            DialogueManager.instance.StartDialogue(defaultDialogue);
            return;
        }

        // Quest already completed
        if (questManager.GetCompletedQuests(npcQuest))
        {
            if (completedDialogue != null)
                DialogueManager.instance.StartDialogue(completedDialogue);
            else
                DialogueManager.instance.StartDialogue(defaultDialogue);

            return;
        }

        // Quest accepted and ready to turn in
        if (questManager.IsQuestAccepted(npcQuest) && questManager.IsQuestComplete(npcQuest))
        {
            QuestEvents.OnQuestturnInRequested?.Invoke(npcQuest);
            return;
        }

        // Quest accepted but not complete
        if (questManager.IsQuestAccepted(npcQuest))
        {
            if (inProgressDialogue != null)
                DialogueManager.instance.StartDialogue(inProgressDialogue);
            else
                DialogueManager.instance.StartDialogue(defaultDialogue);

            return;
        }

        // Quest not accepted yet
        DialogueManager.instance.StartDialogue(defaultDialogue);
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource == null || clip == null) return;
        audioSource.PlayOneShot(clip);
    }

    // Use this from Animation Events
    public void PlayAnimationSound(AudioClip clip)
    {
        PlaySound(clip);
    }

    // Example named methods for animation events
    public void PlayWaveSound()
    {
        PlaySound(interactSound);
    }

    public void PlayGreetingSound()
    {
        PlaySound(playerNearSound);
    }
}