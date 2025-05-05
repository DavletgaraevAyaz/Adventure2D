using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Image healthBar;
    [SerializeField] private Text moneyText;
    [SerializeField] private Text healthText;

    private void Start()
    {
        player.OnPlayerHealthChanges += UpdateHealthUI;
        player.OnPlayerMoneyChange += UpdateMoneyUI;

        UpdateHealthUI(player.GetCurrentHealth(), player.GetMaxHealth());
        UpdateMoneyUI(player.GetMoney());
    }

    private void OnDestroy()
    {
        player.OnPlayerHealthChanges -= UpdateHealthUI;
        player.OnPlayerMoneyChange -= UpdateMoneyUI;
    }

    private void UpdateHealthUI(int currentHealth, int maxHealth)
    {
        float healthPercentage = (float)currentHealth / maxHealth;
        healthBar.fillAmount = healthPercentage;
        healthText.text = "Health: " + currentHealth;
    }
    
    private void UpdateMoneyUI(int currentMoney)
    {
        moneyText.text = currentMoney.ToString();
    }
}
