using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectItem : MonoBehaviour
{
    public GameObject SelectUI;
    public GameObject Sponge;

    public void Select1()
    {
        Sponge.SetActive(true);
        SelectUI.SetActive(false);
    }

    public void Select2()
    {
        if(DataManager.Instance.curMoney >= 10000)
        {
            DataManager.Instance.curMoney -= 10000;
            Sponge.SetActive(true);
            SelectUI.SetActive(false);
        }
    }
        
    public void Select3()
    {
        if (DataManager.Instance.curMoney >= 50000)
        {
            DataManager.Instance.curMoney -= 50000;
            Sponge.SetActive(true);
            SelectUI.SetActive(false);
        }
    }
}
