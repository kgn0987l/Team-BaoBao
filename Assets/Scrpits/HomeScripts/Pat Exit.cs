using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatExit : MonoBehaviour
{

    public Camera perspectiveCamera; // �۽���Ƽ�� ī�޶�
    public Camera orthographicCamera; // ���ұ׷��� ī�޶�
    public GameObject FriendBar; // �ֿϵ��� ������Ʈ
    public GameObject HomeUI; // �ֿϵ��� ������Ʈ

    // ������ ����
    public void Exit()
    {
        FriendBar.SetActive(false);
        HomeUI.SetActive(true);

        perspectiveCamera.gameObject.SetActive(false);

        orthographicCamera.gameObject.SetActive(true);
    }
}
