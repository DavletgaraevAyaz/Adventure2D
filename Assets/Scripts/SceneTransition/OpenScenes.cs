using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenScenes : MonoBehaviour
{
    public void OpenFirst()
    {
        SceneManager.LoadScene("FirstLevel");
    }
    
    public void OpenMainScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void OpenSecondLelvel()
    {
        SceneManager.LoadScene("SecondLevel");
    }
    
    public void OpenThirdLelvel()
    {
        SceneManager.LoadScene("Card");
    }
}
