using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{
    public float moveSpeed = 3f; // ���� �̵� �ӵ�
    public Transform returnTarget; // ���� ���ư� ��ǥ ��ġ(�� ������Ʈ)
    private Vector3 targetPosition; // ���� �̵��� ��ǥ ��ġ
    private bool isMoving = false;
    private float stoppingDistance = 0.1f; // ��ǥ ������ ������ ���� (��ǥ���� ��� ����)

    void Update()
    {
        // ���� ��ǥ ��ġ�� �̵��ؾ� �ϴ� ���
        if (isMoving)
        {
            MoveTowardsTarget();
        }
    }

    // ��ǥ ��ġ�� ���� �̵���ŵ�ϴ�.
    private void MoveTowardsTarget()
    {
        if (targetPosition != null)
        {
            // ��ǥ ��ġ�� �ε巴�� �̵��մϴ�.
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // ��ǥ ������ ����� ��������ٸ� ���ߵ��� �����մϴ�.
            if (Vector3.Distance(transform.position, targetPosition) <= stoppingDistance)
            {
                isMoving = false; // �̵� ����
                transform.position = targetPosition; // ��Ȯ�� ��ǥ ������ �����ϵ��� ��ġ ����
                Debug.Log("Pet reached the target position!");
            }
        }
    }


    // ��ǥ ��ġ�� �����ϴ� �޼���
    public void SetTargetPosition(Vector3 newTargetPosition)
    {
        targetPosition = newTargetPosition;
        isMoving = true; // ��ǥ ��ġ�� �̵��� ����
    }

    // �̵��� �����ϴ� �޼���
    public void StartMoving()
    {
        isMoving = true;
    }

    // ���� ���� ���� ���ư� �� ȣ��Ǵ� �޼���
    public void ReturnToTarget()
    {
        if (returnTarget != null)
        {
            targetPosition = returnTarget.position; // �� ������Ʈ�� ��ġ�� ��ǥ�� ����
            Debug.Log("Pet target position: " + targetPosition); // ��ġ Ȯ��
            isMoving = true; // �̵� ����
            Debug.Log("Pet is returning to target position: " + returnTarget.position);
        }
    }

}
