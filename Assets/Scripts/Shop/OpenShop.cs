using UnityEngine;

public class OpenShop : MonoBehaviour
{
    public void Open()
    {
        Time.timeScale = 0f;
    }

    public void Close()
    {
        Time.timeScale = 1f;
    }
}
