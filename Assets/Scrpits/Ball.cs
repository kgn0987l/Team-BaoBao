using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
    [SerializeField] private GameObject Clear;

    public float pickUpRange = 5f; // ������Ʈ�� ���� �� �ִ� �ִ� �Ÿ�
    public float throwForce = 1f; // ���� ���� �⺻ ��
    public Transform holdPoint; // ������Ʈ�� ��� ���� ��ġ
    public float dragSensitivity = 0.01f; // �巡�� �ΰ���
    public Camera mainCamera; // ���� ī�޶�
    public GameObject objectToPick; // ���� ������Ʈ

    private int totalreturn = 0;

    private GameObject pickedObject = null;
    private Vector3 dragStartPos;
    private Vector3 dragEndPos;
    private bool isDragging = false;
    private bool isHolding = false;
    private bool isReturning = false; // ���� ���� ��ġ�� ���ư��� ������ ����
    private Rigidbody objectRigidbody; // ���� Rigidbody ������Ʈ

    private Ray ray;

    // ���� ���� ��ġ�� ���ư� �� ȣ���� �̺�Ʈ
    public UnityEvent OnReturnToOrigin;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // ������Ʈ�� ��� ���� ��ġ ���� (ī�޶� ��)
        if (holdPoint == null)
        {
            holdPoint = new GameObject("HoldPoint").transform;
            holdPoint.SetParent(mainCamera.transform);
            holdPoint.localPosition = new Vector3(0, 0, 2);
        }

        // ������Ʈ�� ī�޶��� �߾ӿ� ��ġ
        if (objectToPick != null)
        {
            Rigidbody rb = objectToPick.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false; // �߷� ���� ����
                rb.isKinematic = true; // �ʱ⿡�� �������� �ʵ��� ����
                objectRigidbody = rb; // Rigidbody ������Ʈ ����
            }
            objectToPick.transform.position = holdPoint.position; // ī�޶� �տ� ��ġ��Ŵ
            pickedObject = objectToPick; // ������Ʈ�� �Ⱦ� ���·� ����
        }
    }


    void Update()
    {
        if (isReturning)
        {
            return; // ���� ���� ��ġ�� ���ư��� ���̸� �� �̻��� ������ �������� �ʽ��ϴ�.
        }


        if (Input.GetMouseButtonDown(0)) // ���콺 ��ư ����
        {
            if (pickedObject == null)
            {
                PickUpObject();
            }
            else
            {
                // �巡�� ���� ��ġ ����
                dragStartPos = Input.mousePosition;
                isDragging = true;
                isHolding = true;
            }
        }

        if (Input.GetMouseButtonUp(0) && isDragging) // ���콺 ��ư�� ����
        {
            // �巡�� �� ��ġ ����
            dragEndPos = Input.mousePosition;
            ThrowObject();
            isDragging = false;
            isHolding = false;
        }

        if (pickedObject != null && isHolding)
        {
            // ������Ʈ�� ���콺 ��ġ�� �̵�
            MoveObjectWithMouse();
        }

        // �� �����Ӹ��� ����ĳ��Ʈ�� �ð������� ǥ��
        Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.forward * pickUpRange, Color.red);
    }

    void PickUpObject()
    {
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Ray ����

        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            Debug.Log("Raycast hit: " + hit.collider.name);
            if (hit.collider != null && hit.collider.GetComponent<Rigidbody>() != null)
            {
                pickedObject = hit.collider.gameObject;
                Rigidbody rb = pickedObject.GetComponent<Rigidbody>();
                rb.isKinematic = true; // ���� ȿ�� ���
                pickedObject.transform.position = holdPoint.position; // ������Ʈ�� HoldPoint�� �̵�
                pickedObject.transform.SetParent(holdPoint); // ������Ʈ�� HoldPoint�� �θ�� ����
            }
        }
        else
        {
            Debug.Log("����");
        }
    }

    void MoveObjectWithMouse()
    {
        // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = holdPoint.localPosition.z;
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);

        // ������Ʈ�� ���콺 ��ġ�� �̵�
        pickedObject.transform.position = worldPos;
    }

    void ThrowObject()
    {
        Rigidbody rb = pickedObject.GetComponent<Rigidbody>();
        pickedObject.transform.SetParent(null); // �θ� ���� ����
        rb.isKinematic = false;
        rb.useGravity = true; // ���� �� �߷� ����

        // �巡�� ���� ���
        Vector3 dragVector = dragEndPos - dragStartPos;
        Vector3 throwDirection = new Vector3(dragVector.x, dragVector.y, dragVector.y) * dragSensitivity;

        // ���� ���Ͽ� ������
        rb.AddForce(throwDirection * throwForce, ForceMode.VelocityChange);

        pickedObject = null;
    }

    // ��ư�� Ŭ���Ǿ��� �� ȣ��� �޼���
    public void ReturnToOrigin()
    {
        if (objectToPick != null)
        {
            // ���� ���� ��ġ�� ���ư��� ������ ǥ��
            isReturning = true;

            // ���� �ӵ��� 0���� �����Ͽ� ����
            Rigidbody rb = objectToPick.GetComponent<Rigidbody>();
            if (objectRigidbody != null)
            {
                rb.isKinematic = true; // �������� �ʵ��� ����
            }

            // ������Ʈ�� ���� ��ġ�� �̵�
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

            // �߷� ����
            if (objectRigidbody != null)
            {
                objectRigidbody.useGravity = false;
            }

            // �̺�Ʈ ȣ��
            OnReturnToOrigin.Invoke();

            // ���� ���� ��ġ�� ���ƿ����Ƿ� ���� �÷��� ����
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
