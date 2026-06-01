using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableButtons : MonoBehaviour
{
    [SerializeField] private Button _secondLevelBtn;
    [SerializeField] private Button _thirdLevelBtn;

    private void Start()
    {
        if (PlayerPrefs.HasKey("SecondLevelInteractable") || GameModeManager.Instance.IsFirstCompleted)
            _secondLevelBtn.interactable = true;

        if (PlayerPrefs.HasKey("ThirdLevelInteractable") || GameModeManager.Instance.IsSecondLevelCompleted)
            _thirdLevelBtn.interactable = true;
    }
    private void Update()
    {
        if (GameModeManager.Instance.IsFirstCompleted)
        {
            _secondLevelBtn.interactable = true;
            PlayerPrefs.SetString("SecondLevelInteractable", "true");
        }
        if(GameModeManager.Instance.IsSecondLevelCompleted)
        {
            _thirdLevelBtn.interactable = true;
            PlayerPrefs.SetString("ThirdLevelInteractable", "true");
        }
    }
}
