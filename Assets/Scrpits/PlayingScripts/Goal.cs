using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor.TerrainTools;
using UnityEditor.Rendering;
#endif

public class Goal : MonoBehaviour
{
    public GameObject childObject;
    public GameObject petObject;
    public GameObject returnButton;
    private PetController petController;
    private Ball ball;
    private ReturnButton returnbutton;

    private void Awake()
    {
        petController = petObject.GetComponent<PetController>();
        returnbutton = returnButton.GetComponent<ReturnButton>();
        GameObject targetObject = GameObject.Find("Main Camera");

        ball = targetObject.GetComponent<Ball>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pet") && petController.isBall == true)
        {
            petController.Goal();
            childObject.transform.SetParent(null);
            ball.ReturnToOrigin();
            returnbutton.CountScore();
        }
    }
}
