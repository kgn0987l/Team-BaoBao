using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 유니티 버튼 호환용 스크립트.
/// </summary>
public class SceneChangeButton : MonoBehaviour
{
    public void SceneChangeMethod(int num)
    {
        SceneChangeManager.Instance.ChangeScene(num);
    }
}
