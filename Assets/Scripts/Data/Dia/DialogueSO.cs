using UnityEngine;

[CreateAssetMenu(fileName ="DialogueSO", menuName ="Dialogue/DialogueNode")]
public class DialogueSO : ScriptableObject
{
    public DialogueLine[] lines;
    public DialogueOption[] options;

    [Header("Quest Offer(optional)")]
    public QuestSO offerQuestOnEnd;
}

[System.Serializable]
public class DialogueLine
{
    public ActorSO speaker;
    [TextArea(3,5)] public string text;
}

[System.Serializable]
public class DialogueOption
{
    public string stringoptionText;
    public DialogueSO nextDialogue;
    public QuestSO offerQuest;
}
