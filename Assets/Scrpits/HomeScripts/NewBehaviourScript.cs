using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public Camera perspectiveCamera; // �۽���Ƽ�� ī�޶�
    public Camera orthographicCamera; // ���ұ׷��� ī�޶�
    public GameObject FriendBar; // �ֿϵ��� ������Ʈ
    public GameObject HomeUI; // �ֿϵ��� ������Ʈ

    private RaycastHit hit; // ray�� �浹������ �����ϴ� ����ü
    private Ray ray;

    public int maxRaycastDistance = 10;

    private bool PatOnOff = true; // ���ٵ�� ������ ����

    private int PatCount = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Exit();

            perspectiveCamera.gameObject.SetActive(false);

            orthographicCamera.gameObject.SetActive(true);
        }

        // ���콺 ���� ��ư ������ ���� ��
        if (Input.GetMouseButton(0))
        {
            Pat(); // �ֿϵ����� ���ٵ�� ����
        }
    }

    // �ֿϵ����� ���ٵ�� ����
    private void Pat()
    {
        ray = perspectiveCamera.ScreenPointToRay(Input.mousePosition); // Ray ����

        Debug.DrawRay(ray.origin, ray.direction * maxRaycastDistance, Color.red, 1f);

        if (Physics.Raycast(ray, out hit, maxRaycastDistance))
        {
            if (hit.collider.gameObject.tag == "Pet")
            {
                if (PatOnOff == true)
                {
                    Debug.Log("���ٵ����");
                    PatCount++;
                    PatOnOff = false;
                    if (PatCount == 10)
                    {
                        DataManager.Instance.curFriendShip += 10;
                        PatCount = 0;
                    }
                }
            }
            else
            {
                PatOnOff = true;
            }
        }
    }

    // ������ ����
    private void Exit()
    {
        FriendBar.SetActive(false);
        HomeUI.SetActive(true);
    }
}
