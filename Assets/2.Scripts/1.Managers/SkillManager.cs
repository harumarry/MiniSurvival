using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    [SerializeField] SkillData[] allSkills;

    [SerializeField] PlayerAttackController PlayerAttackController;
    [SerializeField] PlayerController PlayerController;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public SkillData[] GetSelectedSkills()
    {
        SkillData[] selectedSkills = new SkillData[3];
        List<SkillData> skillPool = new List<SkillData>(allSkills);

        for (int i = 0; i < selectedSkills.Length; i++)
        {
            int randomIndex = Random.Range(0, skillPool.Count);

            selectedSkills[i] = skillPool[randomIndex];
            skillPool.RemoveAt(randomIndex);
        }

        return selectedSkills;
    }

    public void ApplySkill(SkillData skillData)
    {
        switch (skillData.skillType)
        {
            case SkillType.DamageUp:
                PlayerAttackController.IncreaseDamage(skillData.value);
                break;

            case SkillType.AttackSpeedUp:
                PlayerAttackController.IncreaseAttackSpeed(skillData.value);
                break;

            case SkillType.MoveSpeedUp:
                PlayerController.IncreaseMoveSpeed(skillData.value);
                break;

            case SkillType.ProjectileCountUp:
                PlayerAttackController.IncreaseProjectileCount((int)skillData.value);
                break;
        }
    }
}
