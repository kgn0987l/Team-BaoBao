using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public GameObject effect1;
    public GameObject effect2;

    void Update()
    {
        if(DataManager.Instance.curMentality <= 20)
        {
            effect1.SetActive(true);
        }
        else
        {
            effect1.SetActive(false);
        }

        if(DataManager.Instance.curHygiene <= 20)
        {
            effect2.SetActive(true);
        }
        else
        {
            effect2.SetActive(false);
        }
    }
}
