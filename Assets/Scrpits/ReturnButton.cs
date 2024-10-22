using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReturnButton : MonoBehaviour
{
    public Button returnButton; // ���� ���� ��ġ�� �����ִ� ��ư
    public Ball ball; // Ball ��ũ��Ʈ ����
    public TextMeshProUGUI countText;

    int Count = 0;

    void Start()
    {
        // ��ư Ŭ�� �̺�Ʈ ����
        if (returnButton != null && ball != null)
        {
            returnButton.onClick.AddListener(ball.ReturnToOrigin);
            countText.text = Count.ToString();
            //ball.objectToPick.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public void CountScore()
    {
        Count += 1;
        countText.text = Count.ToString();
    }
}
