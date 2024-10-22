using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReturnButton : MonoBehaviour
{
    public Button returnButton; // 공을 원래 위치로 돌려주는 버튼
    public Ball ball; // Ball 스크립트 연결
    public TextMeshProUGUI countText;

    int Count = 0;

    void Start()
    {
        // 버튼 클릭 이벤트 설정
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
