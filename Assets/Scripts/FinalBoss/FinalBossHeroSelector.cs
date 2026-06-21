using UnityEngine;

public class FinalBossHeroSelector : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject heroSelectionPanel;

    [Header("Heroes")]
    [SerializeField] private GameObject witch;
    [SerializeField] private GameObject wizard;
    [SerializeField] private GameObject archer;

    [Header("Settings")]
    [SerializeField] private bool pauseGameWhileSelecting = true;

    private void Start()
    {
        if (pauseGameWhileSelecting && heroSelectionPanel != null && heroSelectionPanel.activeSelf)
            Time.timeScale = 0f;
    }

    public void SelectWitch()
    {
        SelectHero(witch);
    }

    public void SelectWizard()
    {
        SelectHero(wizard);
    }

    public void SelectArcher()
    {
        SelectHero(archer);
    }

    private void SelectHero(GameObject selectedHero)
    {
        if (witch != null)
            witch.SetActive(witch == selectedHero);

        if (wizard != null)
            wizard.SetActive(wizard == selectedHero);

        if (archer != null)
            archer.SetActive(archer == selectedHero);

        if (heroSelectionPanel != null)
            heroSelectionPanel.SetActive(false);

        Time.timeScale = 1f;
    }
}
