using UnityEngine;
using UnityEngine.UI;

public class OpenFirstLevel : MonoBehaviour
{
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _dialogPanel;
    [SerializeField] private int _firstLevelEnemy;
    [SerializeField] private Text _enemyCountText;

    public static OpenFirstLevel Instance;
    private int _currentLevelEntry = 0;

    private bool _isOpen = false;
    private bool _firsLevelCompleted;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _enemyCountText.text = _currentLevelEntry.ToString() + "/" + _firstLevelEnemy.ToString();
    }

    private void Update()
    {
        OpenWinPanel();
    }

    public void OpenWinPanel()
    {
        if(_currentLevelEntry == _firstLevelEnemy && !_isOpen)
        {
            _dialogPanel.SetActive(true);
            _winPanel.SetActive(true);
            _isOpen = true;
        }
    }

    public void AddDestroyedEntry()
    {
        _currentLevelEntry++;
        _enemyCountText.text = _currentLevelEntry.ToString() + "/"+_firstLevelEnemy.ToString();
    }
}
