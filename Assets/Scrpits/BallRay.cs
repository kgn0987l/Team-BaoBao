using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRay : MonoBehaviour
{
    public float rayLength; // ����ĳ��Ʈ�� ����
    public float isKinematicDelay = 2f;

    private Rigidbody rb;

    // ���� ������ �� �ִ� ������ �߰��մϴ�.
    public PetController petController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // ���� ���� ��ġ
        Vector3 ballPosition = transform.position;

        // �� �Ʒ��� �������� ����ĳ��Ʈ�� ���ϴ�.
        RaycastHit hit;
        if (Physics.Raycast(ballPosition, Vector3.down, out hit, rayLength))
        {
            // ����ĳ��Ʈ�� �±װ� "Ground"�� ��ü�� ����� ���� ó���� ���⿡ �߰��մϴ�.
            if (hit.collider.CompareTag("Ground"))
            {
                Debug.Log("Hit ground! Hit point: " + hit.point);
                // �±װ� "Ground"�� ��ü�� ���� �߰����� ó���� �����մϴ�.

                StartCoroutine(TurnOnIsKinematicAfterDelay());
            }
        }

        Debug.DrawLine(transform.position, transform.position + Vector3.down * rayLength, Color.blue);
    }

    IEnumerator TurnOnIsKinematicAfterDelay()
    {
        yield return new WaitForSeconds(isKinematicDelay);
        if (rb != null)
        {
            rb.isKinematic = true; // isKinematic�� �մϴ�.
        }

        // ���� ��ġ�� �꿡�� �����Ͽ� ���� ���� ���� �̵��ϵ��� �����մϴ�.
        if (petController != null)
        {
            petController.SetTargetPosition(transform.position); // ���� ���� ��ġ�� �̵��ϵ��� ����
            petController.StartMoving(); // ���� �̵��� ����
        }
    }
}
