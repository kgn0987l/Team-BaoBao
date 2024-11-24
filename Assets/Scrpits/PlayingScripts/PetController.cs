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
    public float rotationSpeed = 5f; // 회전 속도

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

    //공의 위치까지 이동하게하는 코드
    public void moveToDestination()
    {
        if (isMove && !isBall)
        {
            Vector3 currentPosition = this.gameObject.transform.position;
            Vector3 targetPosition = Ball.transform.position;

            // 이동 목표 위치 (x, z 축만 따라감)
            Vector3 target = new Vector3(targetPosition.x, currentPosition.y, targetPosition.z);

            // 현재 위치에서 목표 위치로 이동
            this.gameObject.transform.position = Vector3.MoveTowards(currentPosition, target, moveSpeed * Time.deltaTime);

            // 이동 방향 계산
            Vector3 direction = target - currentPosition;

            // 방향 벡터가 존재하면 회전
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                this.gameObject.transform.rotation = Quaternion.Slerp(this.gameObject.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }


    //공을 목적지까지 이동하게하는 코드
    public void MoveToGoal()
    {
        if (isMove && isBall)
        {
            Vector3 currentPosition = this.gameObject.transform.position;
            Vector3 targetPosition = new Vector3(0, 0, 6);

            // 목표 위치 (x, z 축만 따라감)
            Vector3 target = new Vector3(targetPosition.x, currentPosition.y, targetPosition.z);

            // 현재 위치에서 목표 위치로 이동
            this.gameObject.transform.position = Vector3.MoveTowards(currentPosition, target, moveSpeed * Time.deltaTime);

            // 이동 방향 계산
            Vector3 direction = target - currentPosition;

            // 방향 벡터가 존재하면 회전
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
