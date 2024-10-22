using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnackController : MonoBehaviour
{
    private Rigidbody rigid;
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private Vector3 throwPower;

    private bool canThrow = false;
    private bool thrown = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.useGravity = false;
    }

    private void Update()
    {
        if (!thrown && Input.GetMouseButton(0))
        {
            float inputX = Input.GetAxis("Mouse X");
            float inputY = Input.GetAxis("Mouse Y");

            Vector3 moveVector = new Vector2(inputX, inputY);
            moveVector.Normalize();

            rigid.MovePosition(rigid.position + moveVector * moveSpeed);
            rigid.velocity = Vector3.zero;
        }
        else if (canThrow && Input.GetMouseButtonUp(0))
        {
            rigid.constraints -= RigidbodyConstraints.FreezePositionZ;
            thrown = true;
            rigid.useGravity = true;
            rigid.AddForce(throwPower);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "ThrowRange")
        {
            canThrow = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "ThrowRange")
        {
            canThrow = false;
        }

    }

}
