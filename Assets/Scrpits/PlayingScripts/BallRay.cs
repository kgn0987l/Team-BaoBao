using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BallRay : MonoBehaviour
{
    public float rayLength;
    private Rigidbody rb;
    public GameObject petObject;
    private PetController petController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        petController = petObject.GetComponent<PetController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            Invoke("StopBall", 2f);
        }
    }

    public void StopBall()
    {
        rb.isKinematic = true;
        petController.StartMoving();
    }
}
