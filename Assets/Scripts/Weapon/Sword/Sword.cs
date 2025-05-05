using System;
using UnityEngine;
using UnityEngine.UI;

public class Sword : MonoBehaviour 
{

    [SerializeField] private Text _damageText;
    [SerializeField] private int _damageAmount = 2;

    public event EventHandler OnSwordSwing;

    private PolygonCollider2D _polygonCollider2D;

    private int _money;

    private void Awake() 
    {
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    private void Start() 
    {
        AttackColliderTurnOff();
        LoadDamage();
    }


    public void Attack() 
    {
        AttackColliderTurnOffOn();

        OnSwordSwing?.Invoke(this, EventArgs.Empty);
    }
    
    public void AddDamage()
    {
        _money = Player.Instance.GetMoney();
        if(_money < 60)
        {
            return;
        }
        else
        {
            _damageAmount += 1;
            _damageText.text = "Damage: " + _damageAmount;
            Player.Instance.MinusMoney(60);
            PlayerPrefs.SetInt("Damage", _damageAmount);
            PlayerPrefs.Save();
        }
    }

    private void LoadDamage()
    {
        if (PlayerPrefs.HasKey("Damage"))
        {
            _damageAmount = PlayerPrefs.GetInt("Damage");
            _damageText.text = "Damage: " + _damageAmount;
        }
        else
        {
            _damageText.text = "Damage: " + _damageAmount;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.transform.TryGetComponent(out EnemyEntity enemyEntity)) 
        {
            enemyEntity.TakeDamage(_damageAmount);
        }
    }

    public void AttackColliderTurnOff() 
    {
        _polygonCollider2D.enabled = false;
    }

    private void AttackColliderTurnOn() 
    {
        _polygonCollider2D.enabled = true;
    }

    private void AttackColliderTurnOffOn() {
        AttackColliderTurnOff();
        AttackColliderTurnOn();
    }

}
