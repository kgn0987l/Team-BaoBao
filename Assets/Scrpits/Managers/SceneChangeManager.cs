using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    /// <summary>
    /// Singleton ������ ���� Instance �κ�
    /// </summary>
    static private SceneChangeManager instance; // ��ü instance
    static public SceneChangeManager Instance   // �ǻ�� Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject(nameof(SceneChangeManager)).AddComponent<SceneChangeManager>(); // instance�� ������ ���Ӱ� �����մϴ�.
            }
            return instance; // instnace ��ȯ
        }
    }

    private Slider loadingSlider = null;                    // LodingScene�� loadingSlider�� �޾ƿɴϴ�. �޾ƿ��� ������ �Ʒ� ����.

    static private Coroutine sceneChangeCoroutine = null;   // �ڷ�ƾ�� Static���� �޾ƿɴϴ�. ����ȭ�� ���� �ڵ��Դϴ�.

    const float time = 2.5f;                                // LoadingScene������ �ε��ð��� ���ϴ� ����Դϴ�. ������� �ʴ´ٸ� 0.0f�� �������ּ���.

    /// <summary>
    /// Singleton ������ ���� Awake�κ�
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
    /// �� ��ȯ �Լ�. �ε����� ���� ��ȯ�˴ϴ�.
    /// </summary>
    /// <param name="sceneNum">�� �ش��ϴ� ������ ��ȯ�մϴ�.</param>
    public void ChangeScene(int sceneNum)
    {
        sceneChangeCoroutine = StartCoroutine(ChangeSceneCor(sceneNum));    // �ڷ�ƾ�� �����մϴ�.
        SceneManager.LoadScene("LoadingScene");                             // �ε����� �ҷ��ɴϴ�.
    }

    /// <summary>
    /// �� ��ȯ �ڷ�ƾ.
    /// </summary>
    /// <param name="sceneNum">�� �ش��ϴ� ������ ��ȯ�մϴ�.</param>
    /// <returns></returns>
    private IEnumerator ChangeSceneCor(int sceneNum)
    {
        yield return null;
        AsyncOperation asyncOper = SceneManager.LoadSceneAsync(sceneNum);   // ��ǥ�ϴ� ���� �񵿱�� �ҷ��ɴϴ�.
        float timer = 0.0f;                                                 // Ÿ�̸� ����                  
        asyncOper.allowSceneActivation = false;                             // �񵿱� ���� �ε��� �Ϸ�Ǵ��� �ٷ� ��ȯ���� ���ϰ� �մϴ�.

        if (GameObject.Find("loadingSlider").GetComponent<Slider>())        // loadingSlider�� �����̴��� �����մϴ�.
        {
            loadingSlider = GameObject.Find("loadingSlider").GetComponent<Slider>();
        }

        while (asyncOper.progress < 0.9f || timer <= time)                  // �ε��� �Ϸ�ǰ�, ������ �ð��� �Ϸ�� ������ �ݺ��մϴ�.
        {
            yield return null;
            timer += Time.deltaTime;
            Instance.LoadingUIUpdate(asyncOper.progress);                   // LoadingUIUpdate�� ���� �����̴��� ���� �����մϴ�. �����̴��� ���� �ٲٰ� �ʹٸ�, �Ű������� �������ּ���.
        }
        asyncOper.allowSceneActivation = true;                              // �񵿱� ������ ��ȯ��ŵ�ϴ�.
        StopCoroutine(sceneChangeCoroutine);                                // �� �ڷ�ƾ�� �����մϴ�.
    }

    /// <summary>
    /// loaingSlider�� ���� �����ϴ� �Լ��Դϴ�.
    /// </summary>
    /// <param name="value">�� loadingSlider�� ���� �־��ּ���.</param>
    private void LoadingUIUpdate(float value)
    {
        if (loadingSlider == null)
        {
            return;
        }
        loadingSlider.value = value;
    }
}
