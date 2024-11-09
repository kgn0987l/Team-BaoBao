using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{
    public float moveSpeed = 3f; // 펫의 이동 속도
    public Transform returnTarget; // 펫이 돌아갈 목표 위치(빈 오브젝트)
    private Vector3 targetPosition; // 펫이 이동할 목표 위치
    private bool isMoving = false;
    private float stoppingDistance = 0.1f; // 목표 지점에 도달한 간격 (목표와의 허용 오차)

    void Update()
    {
        // 펫이 목표 위치로 이동해야 하는 경우
        if (isMoving)
        {
            MoveTowardsTarget();
        }
    }

    // 목표 위치로 펫을 이동시킵니다.
    private void MoveTowardsTarget()
    {
        if (targetPosition != null)
        {
            // 목표 위치로 부드럽게 이동합니다.
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // 목표 지점에 충분히 가까워졌다면 멈추도록 설정합니다.
            if (Vector3.Distance(transform.position, targetPosition) <= stoppingDistance)
            {
                isMoving = false; // 이동 멈춤
                transform.position = targetPosition; // 정확히 목표 지점에 도달하도록 위치 설정
                Debug.Log("Pet reached the target position!");
            }
        }
    }


    // 목표 위치를 설정하는 메서드
    public void SetTargetPosition(Vector3 newTargetPosition)
    {
        targetPosition = newTargetPosition;
        isMoving = true; // 목표 위치로 이동을 시작
    }

    // 이동을 시작하는 메서드
    public void StartMoving()
    {
        isMoving = true;
    }

    // 펫이 공을 물고 돌아갈 때 호출되는 메서드
    public void ReturnToTarget()
    {
        if (returnTarget != null)
        {
            targetPosition = returnTarget.position; // 빈 오브젝트의 위치를 목표로 설정
            Debug.Log("Pet target position: " + targetPosition); // 위치 확인
            isMoving = true; // 이동 시작
            Debug.Log("Pet is returning to target position: " + returnTarget.position);
        }
    }

}
