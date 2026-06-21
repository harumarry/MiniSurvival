using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    int level = 1;
    int currentExp;
    int requiredExp = 5;

    [SerializeField] HUDUI hudUI;
    [SerializeField] LevelUpUI levelUpUI;

    private void Start()
    {
        hudUI.UpdateExp(level, currentExp, requiredExp);
    }

    public void AddExp(int amount)
    {
        currentExp += amount;

        if (currentExp >= requiredExp)
        {
            LevelUp();
            levelUpUI.Show();
        }

        hudUI.UpdateExp(level, currentExp, requiredExp);
    }

    void LevelUp()
    {
        currentExp -= requiredExp;
        level++;

        requiredExp += 5;
    }
}
