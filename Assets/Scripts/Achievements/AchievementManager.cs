using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AchievementManager : MonoBehaviour
{
    private static AchievementManager _instance;

    public static AchievementManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<AchievementManager>();

            if (_instance == null)
            {
                var managerObject = new GameObject(nameof(AchievementManager));
                _instance = managerObject.AddComponent<AchievementManager>();
            }

            return _instance;
        }
    }

    private const float LockedOverlayAlpha = 0.6745098f;
    private const float CompletedOverlayAlpha = 1f;

    private static readonly Dictionary<AchievementId, int> Thresholds = new Dictionary<AchievementId, int>
    {
        { AchievementId.KillSkeletons15, 15 },
        { AchievementId.KillSkeletons50, 50 },
        { AchievementId.KillSkeletonBosses4, 4 },
        { AchievementId.KillFlyingEyes15, 15 },
        { AchievementId.KillFlyingEyes50, 50 },
        { AchievementId.KillFlyingEyeBosses5, 5 },
        { AchievementId.OpenChests5, 5 },
        { AchievementId.CompleteWitchLevels3, 3 },
        { AchievementId.CompleteWizardLevels3, 3 },
    };

    private static readonly HashSet<string> WitchLevels = new HashSet<string>
    {
        "FirstLevel",
        "SecondLevel",
        "Card",
    };

    private static readonly HashSet<string> WizardLevels = new HashSet<string>
    {
        "FirstLevelForWizard",
        "SecondLevelForWizard",
        "ThirdLevelForWizard",
    };

    public event System.Action OnAchievementsUpdated;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void RegisterEnemyKill(EnemyEntity enemy)
    {
        if (enemy == null)
            return;

        if (enemy.IsFlyingEye())
        {
            IncrementCounter(AchievementId.KillFlyingEyes15);
            IncrementCounter(AchievementId.KillFlyingEyes50);

            if (enemy.IsFlyingEyeBoss())
                IncrementCounter(AchievementId.KillFlyingEyeBosses5);

            return;
        }

        if (enemy.IsBoss())
        {
            IncrementCounter(AchievementId.KillSkeletonBosses4);
            return;
        }

        IncrementCounter(AchievementId.KillSkeletons15);
        IncrementCounter(AchievementId.KillSkeletons50);
    }

    public void RegisterChestOpened()
    {
        IncrementCounter(AchievementId.OpenChests5);
    }

    public void RegisterLevelCompleted()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (WitchLevels.Contains(sceneName))
            RegisterCompletedLevel(AchievementId.CompleteWitchLevels3, sceneName);

        if (WizardLevels.Contains(sceneName))
            RegisterCompletedLevel(AchievementId.CompleteWizardLevels3, sceneName);
    }

    public bool IsUnlocked(AchievementId id)
    {
        return PlayerPrefs.GetInt(GetUnlockedKey(id), 0) == 1;
    }

    public float GetOverlayAlpha(AchievementId id)
    {
        return IsUnlocked(id) ? CompletedOverlayAlpha : LockedOverlayAlpha;
    }

    private void RegisterCompletedLevel(AchievementId id, string sceneName)
    {
        string completedKey = GetCompletedLevelsKey(id);

        if (PlayerPrefs.GetInt(completedKey + sceneName, 0) == 1)
            return;

        PlayerPrefs.SetInt(completedKey + sceneName, 1);

        int completedCount = PlayerPrefs.GetInt(GetCounterKey(id), 0) + 1;
        PlayerPrefs.SetInt(GetCounterKey(id), completedCount);
        PlayerPrefs.Save();

        CheckUnlock(id, completedCount);
    }

    private void IncrementCounter(AchievementId id)
    {
        if (IsUnlocked(id))
            return;

        string counterKey = GetCounterKey(id);
        int count = PlayerPrefs.GetInt(counterKey, 0) + 1;
        PlayerPrefs.SetInt(counterKey, count);
        PlayerPrefs.Save();

        CheckUnlock(id, count);
    }

    private void CheckUnlock(AchievementId id, int count)
    {
        if (!Thresholds.TryGetValue(id, out int threshold))
            return;

        if (count < threshold || IsUnlocked(id))
            return;

        PlayerPrefs.SetInt(GetUnlockedKey(id), 1);
        PlayerPrefs.Save();
        OnAchievementsUpdated?.Invoke();
    }

    private static string GetCounterKey(AchievementId id) => "AchievementCounter_" + id;

    private static string GetUnlockedKey(AchievementId id) => "AchievementUnlocked_" + id;

    private static string GetCompletedLevelsKey(AchievementId id) => "AchievementLevel_" + id + "_";
}

public enum AchievementId
{
    KillSkeletons15,
    KillSkeletons50,
    KillSkeletonBosses4,
    KillFlyingEyes15,
    KillFlyingEyes50,
    KillFlyingEyeBosses5,
    OpenChests5,
    CompleteWitchLevels3,
    CompleteWizardLevels3,
}
