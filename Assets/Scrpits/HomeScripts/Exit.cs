using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public void ExitGame()
    {

        DataManager.Instance.OnDestroy();
        OnUI.Instance.OnDestroy ();
        SceneManager.LoadScene("TitleScene");
/*#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif*/
    }
}
