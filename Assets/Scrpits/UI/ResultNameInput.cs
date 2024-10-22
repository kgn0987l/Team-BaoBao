using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultNameInput : MonoBehaviour
{
    public TMP_InputField petNameInput;
    private string petName = null;

    private void Awake()
    {
        petName = petNameInput.GetComponent<TMP_InputField>().text;
    }

    void Update()
    {

    }

    public void InputName()
    {
        DataManager.Instance.Petname = petNameInput.text;
    }
}
