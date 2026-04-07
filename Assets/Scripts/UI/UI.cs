using UnityEngine;
using UnityEngine.Windows;

public class UI : MonoBehaviour
{
    public bool alternativeInput { get; private set; }
    public UI_Inventory inventoryUI { get; private set; }
    public UI_Quest questui { get; private set; }

    private bool inventoryEnabled;
    private PlayerInputSet input;
    public UI_SkillToolTip skillToolTip;
    public UI_SkillTree skillTree;
    private bool skillTreeEnabled;
    private UI_SkillSlot[] skillSlots;


    private void Awake()
    {

        //questui = GetComponentInChildren<UI_Quest>(true);

        //inventoryEnabled = inventoryUI.gameObject.activeSelf;

        skillToolTip = GetComponentInChildren<UI_SkillToolTip>();
        skillTree = GetComponentInChildren<UI_SkillTree>(true);
    }

    public void ToggleSkillTreeUI()
    {
        skillTreeEnabled = !skillTreeEnabled;
        skillTree.gameObject.SetActive(skillTreeEnabled);
        skillToolTip.ShowToolTip(false, null);
    }

    public void ToggleDialogueUI() 
    {
        
    }
    private void StopPlayerControls(bool stopControls)
    {
        if (stopControls)
            input.Player.Disable();
        else
            input.Player.Enable();
    }

    public void OpenQuestUI(QuestDataSO[] questToShow)
    {
        StopPlayerControls(true);

        questui.gameObject.SetActive(true);
    }

    public UI_SkillSlot GetSkillSlot(SkillType skillType)
    {
        if (skillSlots == null)
            skillSlots = GetComponentsInChildren<UI_SkillSlot>(true);

        foreach (var slot in skillSlots)
        {
            if (slot.skillType == skillType)
            {
                slot.gameObject.SetActive(true);
                return slot;
            }
        }

        return null;
    }
}
