using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
    [SerializeField] private GameObject Clear;

    public float pickUpRange = 5f; // 오브젝트를 집을 수 있는 최대 거리
    public float throwForce = 1f; // 던질 때의 기본 힘
    public Transform holdPoint; // 오브젝트를 들고 있을 위치
    public float dragSensitivity = 0.01f; // 드래그 민감도
    public Camera mainCamera; // 메인 카메라
    public GameObject objectToPick; // 집을 오브젝트

    private int totalreturn = 0;

    private GameObject pickedObject = null;
    private Vector3 dragStartPos;
    private Vector3 dragEndPos;
    private bool isDragging = false;
    private bool isHolding = false;
    private bool isReturning = false; // 공이 원래 위치로 돌아가는 중인지 여부
    private Rigidbody objectRigidbody; // 공의 Rigidbody 컴포넌트

    private Ray ray;

    // 공이 원래 위치로 돌아갈 때 호출할 이벤트
    public UnityEvent OnReturnToOrigin;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // 오브젝트를 들고 있을 위치 설정 (카메라 앞)
        if (holdPoint == null)
        {
            holdPoint = new GameObject("HoldPoint").transform;
            holdPoint.SetParent(mainCamera.transform);
            holdPoint.localPosition = new Vector3(0, 0, 2);
        }

        // 오브젝트를 카메라의 중앙에 위치
        if (objectToPick != null)
        {
            Rigidbody rb = objectToPick.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false; // 중력 영향 제거
                rb.isKinematic = true; // 초기에는 움직이지 않도록 설정
                objectRigidbody = rb; // Rigidbody 컴포넌트 저장
            }
            objectToPick.transform.position = holdPoint.position; // 카메라 앞에 위치시킴
            pickedObject = objectToPick; // 오브젝트를 픽업 상태로 설정
        }
    }


    void Update()
    {
        if (isReturning)
        {
            return; // 공이 원래 위치로 돌아가는 중이면 더 이상의 동작을 수행하지 않습니다.
        }


        if (Input.GetMouseButtonDown(0)) // 마우스 버튼 누름
        {
            if (pickedObject == null)
            {
                PickUpObject();
            }
            else
            {
                // 드래그 시작 위치 저장
                dragStartPos = Input.mousePosition;
                isDragging = true;
                isHolding = true;
            }
        }

        if (Input.GetMouseButtonUp(0) && isDragging) // 마우스 버튼을 떼면
        {
            // 드래그 끝 위치 저장
            dragEndPos = Input.mousePosition;
            ThrowObject();
            isDragging = false;
            isHolding = false;
        }

        if (pickedObject != null && isHolding)
        {
            // 오브젝트를 마우스 위치로 이동
            MoveObjectWithMouse();
        }

        // 매 프레임마다 레이캐스트를 시각적으로 표시
        Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.forward * pickUpRange, Color.red);
    }

    void PickUpObject()
    {
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Ray 생성

        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            Debug.Log("Raycast hit: " + hit.collider.name);
            if (hit.collider != null && hit.collider.GetComponent<Rigidbody>() != null)
            {
                pickedObject = hit.collider.gameObject;
                Rigidbody rb = pickedObject.GetComponent<Rigidbody>();
                rb.isKinematic = true; // 물리 효과 잠금
                pickedObject.transform.position = holdPoint.position; // 오브젝트를 HoldPoint로 이동
                pickedObject.transform.SetParent(holdPoint); // 오브젝트를 HoldPoint에 부모로 설정
            }
        }
        else
        {
            Debug.Log("없음");
        }
    }

    void MoveObjectWithMouse()
    {
        // 마우스 위치를 월드 좌표로 변환
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = holdPoint.localPosition.z;
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);

        // 오브젝트를 마우스 위치로 이동
        pickedObject.transform.position = worldPos;
    }

    void ThrowObject()
    {
        Rigidbody rb = pickedObject.GetComponent<Rigidbody>();
        pickedObject.transform.SetParent(null); // 부모 관계 해제
        rb.isKinematic = false;
        rb.useGravity = true; // 던질 때 중력 적용

        // 드래그 벡터 계산
        Vector3 dragVector = dragEndPos - dragStartPos;
        Vector3 throwDirection = new Vector3(dragVector.x, dragVector.y, dragVector.y) * dragSensitivity;

        // 힘을 가하여 던지기
        rb.AddForce(throwDirection * throwForce, ForceMode.VelocityChange);

        pickedObject = null;
    }

    // 버튼이 클릭되었을 때 호출될 메서드
    public void ReturnToOrigin()
    {
        if (objectToPick != null)
        {
            // 공이 원래 위치로 돌아가는 중임을 표시
            isReturning = true;

            // 공의 속도를 0으로 설정하여 멈춤
            Rigidbody rb = objectToPick.GetComponent<Rigidbody>();
            if (objectRigidbody != null)
            {
                rb.isKinematic = true; // 움직이지 않도록 설정
            }

            // 오브젝트를 원래 위치로 이동
            objectToPick.transform.position = holdPoint.position;
            objectToPick.transform.rotation = holdPoint.rotation;

            totalreturn++;

            if (totalreturn >= 5)
            {
                Clear.SetActive(true);
                DataManager.Instance.curMentality += 30;
                Invoke("HideClear", 1f);
                Invoke("SceneChangeMethod", 1.5f);
                return;
            }

            // 중력 제거
            if (objectRigidbody != null)
            {
                objectRigidbody.useGravity = false;
            }

            // 이벤트 호출
            OnReturnToOrigin.Invoke();

            // 공이 원래 위치로 돌아왔으므로 상태 플래그 해제
            isReturning = false;
        }
    }

    private void HideClear()
    {
        Clear.SetActive(false);
    }

    public void SceneChangeMethod()
    {
        SceneChangeManager.Instance.ChangeScene(3);
    }
}
