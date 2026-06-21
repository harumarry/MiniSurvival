using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Mini Survival/Skill Data")]
public class SkillData : ScriptableObject
{
    public string skillName;

    [TextArea]
    public string description;

    public SkillType skillType;
    public float value;
}
