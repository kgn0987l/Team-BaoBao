using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallArea : MonoBehaviour
{
    public string targetChildName = "TargetEmptyObject"; // 펫의 자식 오브젝트 이름
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pet"))
        {
            // 펫 오브젝트 내에서 targetChildName에 해당하는 자식 오브젝트를 찾기
            Transform targetChild = other.transform.Find(targetChildName);

            if (targetChild != null)
            {
                transform.SetParent(targetChild, true);
                transform.position = targetChild.position;

                if (rb != null)
                {
                    rb.isKinematic = true; // Rigidbody를 비활성화하여 물리 영향을 받지 않도록 설정
                }

                // 빈 오브젝트로 돌아가도록 펫에게 명령
                PetController petController = other.GetComponent<PetController>();
                if (petController != null)
                {
                    petController.ReturnToTarget(); // 펫이 빈 오브젝트로 돌아가도록 설정
                }

                Debug.Log("Pet is returning to the target.");
            }
            else
            {
                Debug.LogWarning(targetChildName + "라는 이름의 자식 오브젝트를 찾을 수 없습니다.");
            }
        }
    }
}
