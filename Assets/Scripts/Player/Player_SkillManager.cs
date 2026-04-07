using UnityEngine;

public class Player_SkillManager : MonoBehaviour
{
    public Skill_dash dash { get; private set; }
    public Skill_Shard shard { get; private set; }

    public Skill_Ultimate ultimate { get; private set; }
    public Skill_Base[] allSkills { get; private set; }

    private void Awake()
    {
        dash = GetComponentInChildren<Skill_dash>();
        shard = GetComponentInChildren<Skill_Shard>();
        ultimate = GetComponentInChildren<Skill_Ultimate>();
        allSkills = GetComponentsInChildren<Skill_Base>();
    }

    public Skill_Base GetSkillByType(SkillType type)
    {
        switch (type)
        {
            case SkillType.Dash: return dash;
            case SkillType.TimeShard: return shard;
            default:
                Debug.Log($"Skill type {type} is not implemented");
                return null;
        }
    }
}
