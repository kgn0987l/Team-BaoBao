using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallArea : MonoBehaviour
{
    public GameObject parentObject;
    public GameObject petObject;
    private PetController petController;

    private void Awake()
    {
        petController = petObject.GetComponent<PetController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pet"))
        {
            transform.SetParent(parentObject.transform);
            transform.localPosition = Vector3.zero;
            petController.GetBall();
        }
    }
}
