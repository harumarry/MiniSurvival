using UnityEngine;
using TMPro;

public class HUDUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI survivalTimeText;
    [SerializeField] TextMeshProUGUI finalSurvivalTimeText; // 최종 생존 시간
    [SerializeField] TextMeshProUGUI bestSurvivalTimeText; // 최고 기록

    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI killCountText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI expText;

    [SerializeField] GameObject gameOverPanel;

    public void UpdateSurvivalTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        survivalTimeText.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }

    public void UpdateHp(float currentHp, float maxHp)
    {
        hpText.text = "HP  "+ currentHp + "/" + maxHp;
    }

    public void UpdateKillCount(int killCount)
    {
        killCountText.text =  "Kill  " + killCount;
    }

    public void UpdateExp(int level, int currentExp, int requiredExp)
    {
        levelText.text = "Lv. " + level;
        expText.text = "EXP  " + currentExp + " / " + requiredExp;
    }

    public void ShowGameOverUI(float finalTime, float bestTime)
    {
        gameOverPanel.SetActive(true);

        int fMin = Mathf.FloorToInt(finalTime / 60);
        int fSec = Mathf.FloorToInt(finalTime % 60);
        int bMin = Mathf.FloorToInt(bestTime / 60);
        int bSec = Mathf.FloorToInt(bestTime % 60);

        finalSurvivalTimeText.text = string.Format("{0:D2}:{1:D2}", fMin, fSec);
        bestSurvivalTimeText.text = string.Format("{0:D2}:{1:D2}", bMin, bSec);
    }
}
