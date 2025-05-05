using UnityEngine;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private Toggle toggle;

    private void Start()
    {
        SetToggleSettings();
    }

    private void OnToggleValueChanged(bool isOn)
    {
        if (isOn)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }

    private void SetToggleSettings()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
        toggle.isOn = audioSource.isPlaying;
    }
}
