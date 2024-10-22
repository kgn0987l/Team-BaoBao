using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneChangeMethod();
        }
    }

    public void SceneChangeMethod()
    {
        SceneChangeManager.Instance.ChangeScene(1);
    }
}
