using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PetCareUI : MonoBehaviour
{
    public TMP_Text PetName;

    void Start()
    {
        UpdatePetName();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePetName();
    }

    private void UpdatePetName()
    {
        PetName.text = DataManager.Instance.Petname;
    }
}
