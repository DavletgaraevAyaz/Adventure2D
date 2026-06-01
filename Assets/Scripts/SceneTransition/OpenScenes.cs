using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenScenes : MonoBehaviour
{
    public void OpenFirst()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("FirstLevel");
    }
    
    public void OpenMainScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene");
    }

    public void OpenSecondLelvel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SecondLevel");
    }
    
    public void OpenThirdLelvel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Card");
    }

    public void OpenFirstWizard()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("FirstLevelForWizard");
    }

    public void OpenSecondWizard()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SecondLevelForWizard");
    }
    public void OpenThirdWizard()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("ThirdLevelForWizard");
    }
}
