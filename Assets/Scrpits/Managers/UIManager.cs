using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public void OpenUIPanel()
    {
        EventSystem.current.enabled = false;
    }

    public void CloseUIPanel()
    {
        EventSystem.current.enabled = true;
    }
}
