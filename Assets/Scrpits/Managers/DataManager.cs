using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;

    public static DataManager Instance
    {
        get
        {
            if(null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // 펫의 이름
    public string Petname = null;

    // 각 상태 바의 최대 및 현재 값
    public float maxFriendShip = 500f;
    public float curFriendShip = 100f;

    public float maxHunger = 100f;
    public float curHunger = 100f;

    public float maxHygiene = 100f;
    public float curHygiene = 100f;

    public float maxMentality = 100f;
    public float curMentality = 100f;

    public int maxMoney = 9999999;
    public int curMoney = 9999999;

    public void OnDestroy()
    {
        Destroy(this.gameObject);
    }
}
