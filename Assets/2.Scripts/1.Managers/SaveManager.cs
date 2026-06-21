using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    const string BEST_SURVIVAL_TIME_KEY = "BEST_SURVIVAL_TIME";

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    // 최고 기록 저장
    public void SaveBestTime(float time)
    {
        float currentBest = PlayerPrefs.GetFloat(BEST_SURVIVAL_TIME_KEY, 0f);

        if (time > currentBest)
        {
            PlayerPrefs.SetFloat(BEST_SURVIVAL_TIME_KEY, time);
            PlayerPrefs.Save();
        }
    }

    // 최고 기록 불러오기
    public float LoadBestTime()
    {
        return PlayerPrefs.GetFloat(BEST_SURVIVAL_TIME_KEY, 0f);
    }
}
