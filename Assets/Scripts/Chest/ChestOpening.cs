using UnityEngine;
using UnityEngine.UI;

public class ChestOpening : MonoBehaviour
{
    [SerializeField] private Text _playerInfoText;

    private Animator _animator;
    private bool _playerOnCollider;

    private const string HINT = "ֽאזלטעו F";

    private void Start()
    {
        _animator = GetComponent<Animator>();    
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && _playerOnCollider)
        {
            _animator.SetTrigger("IsOpening");   
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerInfoText.text = HINT;
            _playerOnCollider = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerInfoText.text = "";
            _playerOnCollider = false;
        }
    }
    
    // method for add money
    public void AddMoneyForPlayer()
    {
        Player.Instance.AddMoney(AddRandomCountMoney());
    }

    // method for random count
    private int AddRandomCountMoney()
    {
        return Random.Range(30, 60);
    }
}
