using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    /// <summary>
    /// Singleton 디자인 패턴 Instance 부분
    /// </summary>
    static private SceneChangeManager instance; // 본체 instance
    static public SceneChangeManager Instance   // 실사용 Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject(nameof(SceneChangeManager)).AddComponent<SceneChangeManager>(); // instance가 없으면 새롭게 생성합니다.
            }
            return instance; // instnace 반환
        }
    }

    private Slider loadingSlider = null;                    // LodingScene의 loadingSlider를 받아옵니다. 받아오는 과정은 아래 참조.

    static private Coroutine sceneChangeCoroutine = null;   // 코루틴을 Static으로 받아옵니다. 최적화를 위한 코드입니다.

    const float time = 2.5f;                                // LoadingScene에서의 로딩시간을 정하는 상수입니다. 사용하지 않는다면 0.0f로 변경해주세요.

    /// <summary>
    /// Singleton 디자인 패턴 Awake부분
    /// </summary>
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
    }

    /// <summary>
    /// 씬 전환 함수. 로딩씬을 거쳐 전환됩니다.
    /// </summary>
    /// <param name="sceneNum">에 해당하는 씬으로 전환합니다.</param>
    public void ChangeScene(int sceneNum)
    {
        sceneChangeCoroutine = StartCoroutine(ChangeSceneCor(sceneNum));    // 코루틴을 실행합니다.
        SceneManager.LoadScene("LoadingScene");                             // 로딩씬을 불러옵니다.
    }

    /// <summary>
    /// 씬 전환 코루틴.
    /// </summary>
    /// <param name="sceneNum">에 해당하는 씬으로 전환합니다.</param>
    /// <returns></returns>
    private IEnumerator ChangeSceneCor(int sceneNum)
    {
        yield return null;
        AsyncOperation asyncOper = SceneManager.LoadSceneAsync(sceneNum);   // 목표하는 씬을 비동기로 불러옵니다.
        float timer = 0.0f;                                                 // 타이머 선언                  
        asyncOper.allowSceneActivation = false;                             // 비동기 씬의 로딩이 완료되더라도 바로 전환되지 못하게 합니다.

        if (GameObject.Find("loadingSlider").GetComponent<Slider>())        // loadingSlider에 슬라이더를 대입합니다.
        {
            loadingSlider = GameObject.Find("loadingSlider").GetComponent<Slider>();
        }

        while (asyncOper.progress < 0.9f || timer <= time)                  // 로딩이 완료되고, 지정한 시간이 완료될 때까지 반복합니다.
        {
            yield return null;
            timer += Time.deltaTime;
            Instance.LoadingUIUpdate(asyncOper.progress);                   // LoadingUIUpdate를 통해 슬라이더의 값을 변경합니다. 슬라이더의 값을 바꾸고 싶다면, 매개변수를 수정해주세요.
        }
        asyncOper.allowSceneActivation = true;                              // 비동기 씬으로 전환시킵니다.
        StopCoroutine(sceneChangeCoroutine);                                // 본 코루틴을 종료합니다.
    }

    /// <summary>
    /// loaingSlider의 값을 변경하는 함수입니다.
    /// </summary>
    /// <param name="value">에 loadingSlider의 값을 넣어주세요.</param>
    private void LoadingUIUpdate(float value)
    {
        if (loadingSlider == null)
        {
            return;
        }
        loadingSlider.value = value;
    }
}
