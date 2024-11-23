using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CameraManager : MonoBehaviour
{
    private RaycastHit hit; // ray의 충돌 정보를 저장하는 구조체
    private Ray ray;

    public int maxRaycastDistance = 10; // Raycast의 최대 거리
    public GameObject Pet; // 애완동물 오브젝트
    public GameObject FriendBar; // UI 패널
    public GameObject HomeUI; // UI 패널
    public Camera perspectiveCamera; // 퍼스펙티브 카메라
    public Camera orthographicCamera; // 오소그래픽 카메라

    private void Start()
    {
        perspectiveCamera.gameObject.SetActive(false);
        orthographicCamera.gameObject.SetActive(true);
    }

    void Update()
    {
        // 마우스 왼쪽 버튼 클릭 시
        if (Input.GetMouseButtonDown(0) && OnUI.Instance.canTouch == true)
        {
            ObjHit(); // Raycast 실행
        }
    }

    // 오브젝트 클릭 시 동작
    private void ObjHit()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Ray 생성

        Debug.DrawRay(ray.origin, ray.direction * maxRaycastDistance, Color.red, 1f);

        if (Physics.Raycast(ray, out hit, maxRaycastDistance))
        {
            if (hit.collider.gameObject.tag == "Pet")
            {
                FriendBar.SetActive(true);
                HomeUI.SetActive(false);
                perspectiveCamera.gameObject.SetActive(true);
                orthographicCamera.gameObject.SetActive(false);
            }
        }
    }
}
