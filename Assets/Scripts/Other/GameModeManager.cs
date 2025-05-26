using UnityEngine;
using UnityEngine.UI;

public class GameModeManager : MonoBehaviour
{
    public static GameModeManager Instance { get; private set; }

    public bool IsHardMode { get; private set; }
    public bool IsFirstCompleted { get; set; }
    public bool IsSecondLevelCompleted { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetHardMode(bool isHard)
    {
        IsHardMode = isHard;
    }
}