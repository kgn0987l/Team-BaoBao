using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChoiceAction : MonoBehaviour
{
    private Animator anim;
    [SerializeField]
    private GameObject descriptPanel;
    [SerializeField]
    private GameObject SelectImage;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void ChangeSelectied(bool isActive)
    {
        anim.SetBool("Selected", isActive);
        descriptPanel.SetActive(isActive);
        SelectImage.SetActive(isActive);
    }

}
