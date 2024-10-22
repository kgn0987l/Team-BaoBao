using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hand : MonoBehaviour
{
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 0f;
        transform.position = mousePosition;
    }
}
