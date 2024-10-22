using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text MoneyTxt;

    void Start()
    {
        
    }

    void Update()
    {
        if (MoneyTxt != null)
        {
            int curMoney = DataManager.Instance.curMoney;
            MoneyTxt.text = curMoney.ToString("#,##0");
        }
    }
}
