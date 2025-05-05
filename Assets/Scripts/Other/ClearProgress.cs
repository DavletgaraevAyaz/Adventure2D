using UnityEngine;

public class ClearProgress : MonoBehaviour
{
    // method for clear player prefs
    public void Clear()
    {
        PlayerPrefs.DeleteAll();
    }
}
