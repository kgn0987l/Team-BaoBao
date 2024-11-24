using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnUI : MonoBehaviour
{
    private static OnUI instance;

    public static OnUI Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public bool canTouch = true;
}
