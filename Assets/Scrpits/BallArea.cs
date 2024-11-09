using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallArea : MonoBehaviour
{
    public string targetChildName = "TargetEmptyObject"; // ���� �ڽ� ������Ʈ �̸�
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pet"))
        {
            // �� ������Ʈ ������ targetChildName�� �ش��ϴ� �ڽ� ������Ʈ�� ã��
            Transform targetChild = other.transform.Find(targetChildName);

            if (targetChild != null)
            {
                transform.SetParent(targetChild, true);
                transform.position = targetChild.position;

                if (rb != null)
                {
                    rb.isKinematic = true; // Rigidbody�� ��Ȱ��ȭ�Ͽ� ���� ������ ���� �ʵ��� ����
                }

                // �� ������Ʈ�� ���ư����� �꿡�� ���
                PetController petController = other.GetComponent<PetController>();
                if (petController != null)
                {
                    petController.ReturnToTarget(); // ���� �� ������Ʈ�� ���ư����� ����
                }

                Debug.Log("Pet is returning to the target.");
            }
            else
            {
                Debug.LogWarning(targetChildName + "��� �̸��� �ڽ� ������Ʈ�� ã�� �� �����ϴ�.");
            }
        }
    }
}
