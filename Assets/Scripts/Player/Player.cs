using System;
using System.Collections;
using UnityEngine;

[SelectionBase]
public class Player : MonoBehaviour 
{
    public static Player Instance { get; private set; }
    public event EventHandler OnPlayerDeath;
    public event Action<int, int> OnPlayerHealthChanges;

    [Header("Health Regeneration")]
    [SerializeField] private float _healthRegenInterval = 15f; 
    [SerializeField] private int _healthRegenAmount = 1;      
    private float _healthRegenTimer;

    [Header("Player settings")]
    [SerializeField] private float _movingSpeed = 10f;
    [SerializeField] private float _damageRecoveryTime = 0.5f;
    [SerializeField] private int _maxHealth = 10;

    [Header("Money")]
    [SerializeField] private int _currentMoney = 0;
    public event Action<int> OnPlayerMoneyChange;

    [Header("Death")]
    [SerializeField] private GameObject _losePanel;
    private Vector2 _inputVector;

    private Rigidbody2D _rb;
    private KnockBack _knockBack;

    private float _minMovingSpeed = 0.1f;
    private bool _isRunning = false;

    private int _currentHealth;
    private bool _canTakeDamage;
    private bool _isAlive;

    private void Awake() {
        Instance = this;
        _rb = GetComponent<Rigidbody2D>();
        _knockBack = GetComponent<KnockBack>();
    }

    private void OnEnable()
    {
        GameInput.Instance.OnPlayerAttack += GameInput_OnPlayerAttack;
    }

    private void Start()
    {
        LoadMoney();
        _currentHealth = _maxHealth;
        _canTakeDamage = true;
        _isAlive = true;
        OnPlayerHealthChanges?.Invoke(_currentHealth, _maxHealth);
        _healthRegenTimer = _healthRegenInterval;
    }

    private void OnDisable()
    {
        GameInput.Instance.OnPlayerAttack -= GameInput_OnPlayerAttack;
    }

    private void Update() 
    {
        _inputVector = GameInput.Instance.GetMovementVector();

        if (_isAlive && _currentHealth < _maxHealth)
        {
            _healthRegenTimer -= Time.deltaTime;

            if (_healthRegenTimer <= 0f)
            {
                Heal(_healthRegenAmount);
                _healthRegenTimer = _healthRegenInterval;
            }
        }
    }


    private void FixedUpdate() 
    {
        if (_knockBack.IsGettingKnockedBack)
            return;

        HandleMovement();
    }

    public void AddMoney(int amount)
    {
        _currentMoney += amount;
        SaveMoney();

        OnPlayerMoneyChange?.Invoke(_currentMoney);
    }

    public void MinusMoney(int amount)
    {
        _currentMoney -= amount;
        SaveMoney();

        OnPlayerMoneyChange?.Invoke(_currentMoney);
    }

    public void LoadMoney()
    {
        if (PlayerPrefs.HasKey("Money"))
        {
            _currentMoney = PlayerPrefs.GetInt("Money");
            OnPlayerMoneyChange?.Invoke(_currentMoney);
        }
    }

    public void Heal(int healAmount)
    {
        _currentHealth = Mathf.Min(_maxHealth, _currentHealth + healAmount);
        OnPlayerHealthChanges?.Invoke(_currentHealth, _maxHealth);
        Debug.Log($"Player healed! Current health: {_currentHealth}");
    }
    
    public bool IsAlive()
    {
        return _isAlive;
    }

    public void TakeDamage(Transform damageTransform, int damage)
    {
        if(_canTakeDamage && _isAlive)
        {
            _canTakeDamage = false;
            _currentHealth = Mathf.Max(0, _currentHealth -= damage);
            Debug.Log(_currentHealth);

            OnPlayerHealthChanges?.Invoke(_currentHealth, _maxHealth);

            _knockBack.GetKnockedBack(damageTransform);
            StartCoroutine(DamageRecoveryRoutine());
        }

        DetectDamage();
    }

    public void AddHealth()
    {
        _currentMoney = Player.Instance.GetMoney();
        if(_currentHealth < 10)
        {
            if (_currentMoney < 50)
            {
                return;
            }
            else
            {
                _currentHealth += 1;
                MinusMoney(40);
                OnPlayerHealthChanges?.Invoke( _currentHealth, _maxHealth);
            }
        }
        else
        {
            return;
        }
    }

    public int GetCurrentHealth() => _currentHealth;
    public int GetMaxHealth() => _maxHealth;
    public int GetMoney() => _currentMoney;

    public bool IsRunning() 
    {
        return _isRunning;
    }

    public Vector3 GetPlayerScreenPosition() 
    {
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        return playerScreenPosition;
    }

    private void SaveMoney()
    {
        PlayerPrefs.SetInt("Money", _currentMoney);
        PlayerPrefs.Save();
    }

    private void DetectDamage()
    {
        if (_currentHealth == 0 && _isAlive)
        {
            _isAlive = false;

            _knockBack.StopKnockBackMovement();
            GameInput.Instance.DisableMovement();
            OnPlayerDeath?.Invoke(this, EventArgs.Empty);
            _losePanel.SetActive(true);
        }
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(_damageRecoveryTime);
        _canTakeDamage = true;
    }

    private void HandleMovement()
    {
        _rb.MovePosition(_rb.position + _inputVector * (_movingSpeed * Time.fixedDeltaTime));

        if (Mathf.Abs(_inputVector.x) > _minMovingSpeed || Mathf.Abs(_inputVector.y) > _minMovingSpeed)
        {
            _isRunning = true;
        }
        else
        {
            _isRunning = false;
        }
    }

    private void GameInput_OnPlayerAttack(object sender, System.EventArgs e)
    {
        ActiveWeapon.Instance.GetActiveWeapon().Attack();
    }
}
