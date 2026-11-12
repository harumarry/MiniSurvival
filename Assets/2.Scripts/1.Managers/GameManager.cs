using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public HUDUI hudUI;

    public GameObject victoryPanel;
    public GameObject readyPanel;
    public GameObject pausedPanel;
    public GameObject skillPanel;
    public GameObject gameOverPanel;

    [HideInInspector] public bool isGameOver = false;

    public float survivalTime;
    int killCount;
    int victoryTime = 60 * 4;

    private void Awake()
    {
        Time.timeScale = 0;
        readyPanel.SetActive(true);

        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        hudUI.UpdateKillCount(killCount);
    }

    private void Update()
    {
        if (isGameOver) return;

        survivalTime += Time.deltaTime;

        hudUI.UpdateSurvivalTime(survivalTime);

        if (survivalTime >= victoryTime)
        {
            Victory();
            return;
        }

        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) &&
            !readyPanel.activeSelf && !skillPanel.activeSelf && !victoryPanel.activeSelf && !gameOverPanel.activeSelf)
        {
            if (pausedPanel.activeSelf)
            {
                resumeGame();
                return;
            }

            Time.timeScale = 0;
            pausedPanel.SetActive(true);
        }
    }

    public void AddKillCount()
    {
        if (isGameOver) return;

        killCount++;
        hudUI.UpdateKillCount(killCount);
    }

    public void GameOver()
    {
        if (isGameOver) return;

        AudioManager.instance.PlayGameOver();

        isGameOver = true;
        Time.timeScale = 0f;

        // 최고 기록 저장
        SaveManager.instance.SaveBestTime(survivalTime);

        // 결과 UI 업데이트
        hudUI.ShowGameOverUI(survivalTime, SaveManager.instance.LoadBestTime());
    }

    public void Victory()
    {
        isGameOver = true;
        Time.timeScale = 0f;
        victoryPanel.SetActive(true);

        AudioManager.instance.PlayVictory();

        // 최고 기록 저장
        SaveManager.instance.SaveBestTime(survivalTime);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }

    public void StartGame()
    {
        readyPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void resumeGame()
    {
        pausedPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
