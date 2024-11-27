using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource backmusic;

    private static MusicManager instance;

    public static MusicManager Instance
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

    void Awake()
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

        if (backmusic.isPlaying) return; //배경음악이 재생되고 있다면 패스
        else
        {
            backmusic.Play();
        }
    }
}
