using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsPanelUI : MonoBehaviour
{
    [System.Serializable]
    private struct AchievementView
    {
        public AchievementId id;
        public Image overlay;
    }

    [SerializeField] private AchievementView[] _achievements;

    private void OnEnable()
    {
        Refresh();

        AchievementManager.Instance.OnAchievementsUpdated += Refresh;
    }

    private void OnDisable()
    {
        AchievementManager.Instance.OnAchievementsUpdated -= Refresh;
    }

    private void Start()
    {
        if (_achievements == null || _achievements.Length == 0)
            BuildViewsFromChildren();

        Refresh();
    }

    private void BuildViewsFromChildren()
    {
        var views = new List<AchievementView>();

        foreach (Transform child in transform)
        {
            if (!TryMapChildName(child.name, out AchievementId id))
                continue;

            Image overlay = child.GetComponent<Image>();
            if (overlay == null)
                continue;

            views.Add(new AchievementView { id = id, overlay = overlay });
        }

        _achievements = views.ToArray();
    }

    private void Refresh()
    {
        if (_achievements == null)
            return;

        foreach (AchievementView view in _achievements)
        {
            if (view.overlay == null)
                continue;

            Color color = view.overlay.color;
            color.a = AchievementManager.Instance.GetOverlayAlpha(view.id);
            view.overlay.color = color;
        }
    }

    private static bool TryMapChildName(string childName, out AchievementId id)
    {
        switch (childName)
        {
            case "FirstAchiev":
                id = AchievementId.KillSkeletons15;
                return true;
            case "SecondAchiev":
                id = AchievementId.KillSkeletons50;
                return true;
            case "Third":
                id = AchievementId.KillSkeletonBosses4;
                return true;
            case "FourAchiev":
                id = AchievementId.KillFlyingEyes15;
                return true;
            case "FiveAchiev":
                id = AchievementId.KillFlyingEyes50;
                return true;
            case "SixAchiev":
                id = AchievementId.KillFlyingEyeBosses5;
                return true;
            case "SevenAchiev":
                id = AchievementId.OpenChests5;
                return true;
            case "EightAchiev":
                id = AchievementId.CompleteWitchLevels3;
                return true;
            case "NineAchiev":
                id = AchievementId.CompleteWizardLevels3;
                return true;
            default:
                id = default;
                return false;
        }
    }
}
