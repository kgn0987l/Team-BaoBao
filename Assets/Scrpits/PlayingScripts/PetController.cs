using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PetController : MonoBehaviour
{
    public bool isMove = false;
    public bool isBall = false;
    public bool isGoal = false;

    public GameObject Ball;
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f; // ȸ�� �ӵ�

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        moveToDestination();
        MoveToGoal();
        Debug.Log(isMove);
    }

    //���� ��ġ���� �̵��ϰ��ϴ� �ڵ�
    public void moveToDestination()
    {
        if (isMove && !isBall)
        {
            Vector3 currentPosition = this.gameObject.transform.position;
            Vector3 targetPosition = Ball.transform.position;

            // �̵� ��ǥ ��ġ (x, z �ุ ����)
            Vector3 target = new Vector3(targetPosition.x, currentPosition.y, targetPosition.z);

            // ���� ��ġ���� ��ǥ ��ġ�� �̵�
            this.gameObject.transform.position = Vector3.MoveTowards(currentPosition, target, moveSpeed * Time.deltaTime);

            // �̵� ���� ���
            Vector3 direction = target - currentPosition;

            // ���� ���Ͱ� �����ϸ� ȸ��
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                this.gameObject.transform.rotation = Quaternion.Slerp(this.gameObject.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }


    //���� ���������� �̵��ϰ��ϴ� �ڵ�
    public void MoveToGoal()
    {
        if (isMove && isBall)
        {
            Vector3 currentPosition = this.gameObject.transform.position;
            Vector3 targetPosition = new Vector3(0, 0, 6);

            // ��ǥ ��ġ (x, z �ุ ����)
            Vector3 target = new Vector3(targetPosition.x, currentPosition.y, targetPosition.z);

            // ���� ��ġ���� ��ǥ ��ġ�� �̵�
            this.gameObject.transform.position = Vector3.MoveTowards(currentPosition, target, moveSpeed * Time.deltaTime);

            // �̵� ���� ���
            Vector3 direction = target - currentPosition;

            // ���� ���Ͱ� �����ϸ� ȸ��
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                this.gameObject.transform.rotation = Quaternion.Slerp(this.gameObject.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    public void StartMoving()
    {
        animator.SetBool("isWalking", true);
        isMove = true;
    }

    public void GetBall()
    {
        isBall = true;
    }
    
    public void StopMoving()
    {
        animator.SetBool("isWalking", false);
        isMove = false;
    }

    public void LoseBall()
    {
        isBall = false;
    }
}
