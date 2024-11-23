using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CameraManager : MonoBehaviour
{
    private RaycastHit hit; // ray�� �浹 ������ �����ϴ� ����ü
    private Ray ray;

    public int maxRaycastDistance = 10; // Raycast�� �ִ� �Ÿ�
    public GameObject Pet; // �ֿϵ��� ������Ʈ
    public GameObject FriendBar; // UI �г�
    public GameObject HomeUI; // UI �г�
    public Camera perspectiveCamera; // �۽���Ƽ�� ī�޶�
    public Camera orthographicCamera; // ���ұ׷��� ī�޶�

    private void Start()
    {
        perspectiveCamera.gameObject.SetActive(false);
        orthographicCamera.gameObject.SetActive(true);
    }

    void Update()
    {
        // ���콺 ���� ��ư Ŭ�� ��
        if (Input.GetMouseButtonDown(0) && OnUI.Instance.canTouch == true)
        {
            ObjHit(); // Raycast ����
        }
    }

    // ������Ʈ Ŭ�� �� ����
    private void ObjHit()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Ray ����

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
