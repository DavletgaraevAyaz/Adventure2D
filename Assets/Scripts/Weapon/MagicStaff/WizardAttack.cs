using UnityEngine;
using UnityEngine.UI;

public class WizardAttack : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] private MagicProjectile projectilePrefab;
    [SerializeField] private Arrow arrowPrefab;
    [SerializeField] private Transform firePoint;

    [Header("Stats")]
    [SerializeField] private Text _damageText;
    [SerializeField] private int damage = 2;
    [SerializeField] private float cooldown = 1.5f;

    private Animator animator;
    private GameInput _gameInput;

    private float nextAttackTime;

    private Vector2 savedDirection;

    private const string ATTACK = "Attack";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        LoadDamage();
        _gameInput = GameInput.Instance;
        if (_gameInput != null)
            _gameInput.OnPlayerAttack += GameInput_OnPlayerAttack;
    }

    private void OnDisable()
    {
        UnsubscribeFromInput();
    }

    private void OnDestroy()
    {
        UnsubscribeFromInput();
    }

    private void UnsubscribeFromInput()
    {
        if (_gameInput != null)
        {
            _gameInput.OnPlayerAttack -= GameInput_OnPlayerAttack;
            _gameInput = null;
        }
    }

    private void GameInput_OnPlayerAttack(object sender, System.EventArgs e)
    {
        if (!isActiveAndEnabled || firePoint == null || animator == null)
            return;

        if (Player.Instance == null || !Player.Instance.IsAlive())
            return;

        if (Time.time < nextAttackTime)
            return;

        nextAttackTime = Time.time + cooldown;

        Vector3 mouseWorld =
            Camera.main.ScreenToWorldPoint(GameInput.Instance.GetMousePosition());

        mouseWorld.z = 0;

        savedDirection =
            (mouseWorld - firePoint.position).normalized;

        animator.SetTrigger(ATTACK);
    }

    public void AddDamage()
    {
        if (Player.Instance.GetMoney() < 60)
            return;

        damage += 1;
        UpdateDamageText();
        Player.Instance.MinusMoney(60);
        PlayerPrefs.SetInt("Damage", damage);
        PlayerPrefs.Save();
    }

    // Animation Event
    public void ShootProjectile()
    {
        if (projectilePrefab == null || firePoint == null)
            return;

        MagicProjectile projectile =
            Instantiate(projectilePrefab,
                firePoint.position,
                Quaternion.identity);

        projectile.Initialize(savedDirection, damage);
    }
    
    public void ShootArrow()
    {
        if (arrowPrefab == null || firePoint == null)
            return;

        Arrow arrowPrefab1 =
            Instantiate(arrowPrefab,
                firePoint.position,
                Quaternion.identity);

        arrowPrefab1.Initialize(savedDirection, damage);
    }

    private void LoadDamage()
    {
        if (PlayerPrefs.HasKey("Damage"))
            damage = PlayerPrefs.GetInt("Damage");

        UpdateDamageText();
    }

    private void UpdateDamageText()
    {
        if (_damageText != null)
            _damageText.text = "Damage: " + damage;
    }
}