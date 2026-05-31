using UnityEngine;

public class WizardAttack : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] private MagicProjectile projectilePrefab;
    [SerializeField] private Transform firePoint;

    [Header("Stats")]
    [SerializeField] private int damage = 2;
    [SerializeField] private float cooldown = 1.5f;

    private Animator animator;

    private float nextAttackTime;

    private Vector2 savedDirection;

    private const string ATTACK = "Attack";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameInput.Instance.OnPlayerAttack += GameInput_OnPlayerAttack;
    }


    private void OnDisable()
    {
        if (GameInput.Instance != null)
            GameInput.Instance.OnPlayerAttack -= GameInput_OnPlayerAttack;
    }

    private void GameInput_OnPlayerAttack(object sender, System.EventArgs e)
    {
        if (!Player.Instance.IsAlive())
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

    // Animation Event
    public void ShootProjectile()
    {
        MagicProjectile projectile =
            Instantiate(projectilePrefab,
                firePoint.position,
                Quaternion.identity);

        projectile.Initialize(savedDirection, damage);
    }
}