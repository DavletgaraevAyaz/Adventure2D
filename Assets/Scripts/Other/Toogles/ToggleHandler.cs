using UnityEngine;
using UnityEngine.UI;

public class ToggleHandler : MonoBehaviour
{
    [SerializeField] private Toggle hardModeToggle;

    private void Start()
    {
        SetToggleSettings();
    }

    private void OnToggleValueChanged(bool isOn)
    {
        GameModeManager.Instance.SetHardMode(isOn);
    }

    private void SetToggleSettings()
    {
        hardModeToggle.isOn = GameModeManager.Instance.IsHardMode;
        hardModeToggle.onValueChanged.AddListener(OnToggleValueChanged);
    }
}