using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIopen : MonoBehaviour
{

    public void OpenUI()
    {
        OnUI.Instance.canTouch = false;
    }

    public void CloseUI()
    {
        OnUI.Instance.canTouch = true;
    }
}
