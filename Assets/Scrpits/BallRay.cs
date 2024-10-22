using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRay : MonoBehaviour
{
    public float rayLength; // 레이캐스트의 길이
    public float isKinematicDelay = 2f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        // 공의 현재 위치
        Vector3 ballPosition = transform.position;

        // 공 아래쪽 방향으로 레이캐스트를 쏩니다.
        RaycastHit hit;
        if (Physics.Raycast(ballPosition, Vector3.down, out hit, rayLength))
        {
            // 레이캐스트가 태그가 "Ground"인 객체에 닿았을 때의 처리를 여기에 추가합니다.
            if (hit.collider.CompareTag("Ground"))
            {
                Debug.Log("Hit ground! Hit point: " + hit.point);
                // 태그가 "Ground"인 객체에 대한 추가적인 처리를 수행합니다.

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
            rb.isKinematic = true; // isKinematic을 켭니다.
        }
    }
}
