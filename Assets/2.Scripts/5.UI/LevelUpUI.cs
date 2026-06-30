using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpUI : MonoBehaviour
{
    [SerializeField] GameObject skillPanel;

    [SerializeField] TMP_Text[] skillNameTexts;
    [SerializeField] TMP_Text[] skillDescriptionTexts;
    [SerializeField] Button[] skillButtons;

    SkillData[] currentSkills;

    public void Show()
    {
        skillPanel.SetActive(true);
        Time.timeScale = 0f;

        currentSkills = SkillManager.instance.GetSelectedSkills();

        for (int i = 0; i < skillButtons.Length; i++)
        {
            int index = i;

            skillNameTexts[i].text = currentSkills[i].skillName;
            skillDescriptionTexts[i].text = currentSkills[i].description;

            skillButtons[i].onClick.RemoveAllListeners();
            skillButtons[i].onClick.AddListener(() => SelectSkill(index));
        }
    }

    void SelectSkill(int index)
    {
        SkillManager.instance.ApplySkill(currentSkills[index]);
        Close();
    }

    void Close()
    {
        skillPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
