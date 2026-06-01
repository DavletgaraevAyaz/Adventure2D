using UnityEngine;

public class ClearProgress : MonoBehaviour
{
    public void Clear()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
