using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����Ƽ ��ư ȣȯ�� ��ũ��Ʈ.
/// </summary>
public class SceneChangeButton : MonoBehaviour
{
    public void SceneChangeMethod(int num)
    {
        SceneChangeManager.Instance.ChangeScene(num);
    }
}
