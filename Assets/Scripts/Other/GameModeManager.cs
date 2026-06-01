using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    public static GameModeManager Instance { get; private set; }

    private const string HardModeKey = "HardMode";
    private const string FirstCompletedKey = "FirstLevelCompleted";
    private const string SecondCompletedKey = "SecondLevelCompleted";

    public bool IsHardMode { get; private set; }
    public bool IsFirstCompleted { get; set; }
    public bool IsSecondLevelCompleted { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadProgress();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetHardMode(bool isHard)
    {
        IsHardMode = isHard;
        PlayerPrefs.SetInt(HardModeKey, isHard ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SaveLevelProgress()
    {
        if (IsFirstCompleted)
            PlayerPrefs.SetInt(FirstCompletedKey, 1);

        if (IsSecondLevelCompleted)
            PlayerPrefs.SetInt(SecondCompletedKey, 1);

        PlayerPrefs.Save();
    }

    private void LoadProgress()
    {
        IsHardMode = PlayerPrefs.GetInt(HardModeKey, 0) == 1;
        IsFirstCompleted = PlayerPrefs.GetInt(FirstCompletedKey, 0) == 1;
        IsSecondLevelCompleted = PlayerPrefs.GetInt(SecondCompletedKey, 0) == 1;
    }
}