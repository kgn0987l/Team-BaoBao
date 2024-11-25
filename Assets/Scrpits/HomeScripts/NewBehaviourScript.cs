using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public Camera perspectiveCamera; // 퍼스펙티브 카메라

    private RaycastHit hit; // ray의 충돌정보를 저장하는 구조체
    private Ray ray;

    public int maxRaycastDistance = 10;

    private bool PatOnOff = true; // 쓰다듬는 중인지 여부

    private int PatCount = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 마우스 왼쪽 버튼 누르고 있을 때
        if (Input.GetMouseButton(0))
        {
            Pat(); // 애완동물을 쓰다듬는 동작
        }
    }

    // 애완동물을 쓰다듬는 동작
    private void Pat()
    {
        ray = perspectiveCamera.ScreenPointToRay(Input.mousePosition); // Ray 생성

        Debug.DrawRay(ray.origin, ray.direction * maxRaycastDistance, Color.red, 1f);

        if (Physics.Raycast(ray, out hit, maxRaycastDistance))
        {
            if (hit.collider.gameObject.tag == "Pet")
            {
                if (PatOnOff == true)
                {
                    Debug.Log("쓰다듬기중");
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
}
