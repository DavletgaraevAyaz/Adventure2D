using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScenes : MonoBehaviour
{
    public void ResatrtFirst()
    {
        SceneTransition.SwitchToScene("FirstLevel");
    }

    public void OpenMainScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void RestartSecondLelvel()
    {
        if (GameModeManager.Instance.IsHardMode)
        {
            SceneTransition.SwitchToScene("FirstLevel");
        }
        else
        {
            SceneTransition.SwitchToScene("SecondLevel");
        }
    }

    public void RestartThirdLelvel()
    {
        if (GameModeManager.Instance.IsHardMode)
        {
            SceneTransition.SwitchToScene("FirstLevel");
        }
        else
        {
            SceneTransition.SwitchToScene("Card");
        }
    }
}
