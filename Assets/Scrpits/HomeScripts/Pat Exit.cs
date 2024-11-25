using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatExit : MonoBehaviour
{

    public Camera perspectiveCamera; // 퍼스펙티브 카메라
    public Camera orthographicCamera; // 오소그래픽 카메라
    public GameObject FriendBar; // 애완동물 오브젝트
    public GameObject HomeUI; // 애완동물 오브젝트

    // 나가기 동작
    public void Exit()
    {
        FriendBar.SetActive(false);
        HomeUI.SetActive(true);

        perspectiveCamera.gameObject.SetActive(false);

        orthographicCamera.gameObject.SetActive(true);
    }
}
