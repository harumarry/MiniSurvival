using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public HUDUI hudUI;

    [HideInInspector] public bool isGameOver = false;

    float survivalTime;
    int killCount;

    private void Start()
    {
        hudUI.UpdateKillCount(killCount);
    }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (isGameOver) return;

        survivalTime += Time.deltaTime;

        hudUI.UpdateSurvivalTime(survivalTime);
    }

    public void AddKiillCount()
    {
        if (isGameOver) return;    

        killCount++;
        hudUI.UpdateKillCount(killCount);
    }

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        Time.timeScale = 0f;

        // 최고 기록 저장
        SaveManager.instance.SaveBestTime(survivalTime);

        // 결과 UI 업데이트
        float finalTime = survivalTime;
        float bestTime = SaveManager.instance.LoadBestTime();
        hudUI.ShowGameOverUI(finalTime, bestTime);

    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }
}
