using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WarningUI : MonoBehaviour
{
    public GameObject Warning;

    private float timer;
    private float hungerTimer;
    private float hygieneTimer;
    private float mentalityTimer;

    void Start()
    {
        timer = 60f;
        hungerTimer = 60f;
        hygieneTimer = 60f;
        mentalityTimer = 60f;
    }

    void Update()
    {
        UpdateTimer();

        if (DataManager.Instance.curHunger <= 20f)
        {
            if(hungerTimer > 60)
            {
                ShowWarning("���� ���ָ��� �ֽ��ϴ�.", ref hungerTimer);
            }
        }

        if (DataManager.Instance.curHygiene <= 20f)
        {
            if(hygieneTimer > 60)
            {
                ShowWarning("���� �ſ� �������ϴ�.", ref hygieneTimer);
            }
        }

        if (DataManager.Instance.curMentality <= 20f)
        {
            if(mentalityTimer > 60)
            {
                ShowWarning("���� �ɰ��� ��Ʈ������ �ô޸��� �ֽ��ϴ�.", ref mentalityTimer);
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            DataManager.Instance.curHunger = 20;
            Debug.Log(DataManager.Instance.curHunger);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            DataManager.Instance.curHunger = 50;
            Debug.Log(DataManager.Instance.curHunger);
        }
    }

    void UpdateTimer()
    {
        timer += Time.deltaTime;
        hungerTimer += Time.deltaTime;
        hygieneTimer += Time.deltaTime;
        mentalityTimer += Time.deltaTime;
    }

    void ShowWarning(string message, ref float timer)
    {
        Warning.SetActive(true);
        TMP_Text warningText = Warning.GetComponentInChildren<TMP_Text>();
        if (warningText != null)
        {
            warningText.text = message;
        }
        StartCoroutine(CloseUI(timer));
        timer = 0; // Ÿ�̸� �ʱ�ȭ
    }

    IEnumerator CloseUI(float timer)
    {
        yield return new WaitForSeconds(1.0f);
        Warning.SetActive(false);
    }
}
