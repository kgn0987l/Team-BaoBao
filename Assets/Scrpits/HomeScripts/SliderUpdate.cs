using UnityEngine;
using UnityEngine.UI;

public class SliderUpdate : MonoBehaviour
{
    // 상태 바 UI 요소들
    public Slider FriendShipBar2;
    public Slider HungerBar;
    public Slider HygieneBar;
    public Slider MentalityBar;

    // 슬라이더의 Fill 영역 이미지 참조
    public Image FriendShipBarFill;
    public Image HungerBarFill;
    public Image HygieneBarFill;
    public Image MentalityBarFill;

    // 감소 간격과 타이머
    private float decreaseInterval = 5f;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        // 주기적으로 각 상태 감소
        if (timer >= decreaseInterval)
        {
            DecreaseStat(ref DataManager.Instance.curFriendShip, DataManager.Instance.maxFriendShip, FriendShipBar2);
            DecreaseStat(ref DataManager.Instance.curHunger, DataManager.Instance.maxHunger);
            DecreaseStat(ref DataManager.Instance.curHygiene, DataManager.Instance.maxHygiene);
            DecreaseStat(ref DataManager.Instance.curMentality, DataManager.Instance.maxMentality);
            timer = 0f; // 타이머 초기화
        }

        UpdateBars();
    }

    // 상태 바 업데이트 및 색상 변경
    void UpdateBars()
    {
        UpdateBar(FriendShipBar2, DataManager.Instance.curFriendShip, DataManager.Instance.maxFriendShip, FriendShipBarFill);
        UpdateBar(HungerBar, DataManager.Instance.curHunger, DataManager.Instance.maxHunger, HungerBarFill);
        UpdateBar(HygieneBar, DataManager.Instance.curHygiene, DataManager.Instance.maxHygiene, HygieneBarFill);
        UpdateBar(MentalityBar, DataManager.Instance.curMentality, DataManager.Instance.maxMentality, MentalityBarFill);
    }

    // 개별 상태 바 업데이트 및 색상 설정
    void UpdateBar(Slider bar, float currentValue, float maxValue, Image fillImage)
    {
        float percentage = currentValue / maxValue;
        bar.value = percentage;

        // 상태에 따른 색상 변경
        if (percentage <= 0.2f)
        {
            fillImage.color = Color.red;  // 20% 이하 빨간색
        }
        else if (percentage <= 0.6f)
        {
            fillImage.color = new Color(1f, 0.65f, 0f);  // 주황색 (RGB: 255, 165, 0)
        }
        else
        {
            fillImage.color = Color.green;  // 60% 초과 초록색
        }
    }

    // 상태 감소 함수 (슬라이더 포함)
    void DecreaseStat(ref float currentValue, float maxValue, Slider bar)
    {
        currentValue = Mathf.Max(0, currentValue - 1); // 현재 값 감소 (최소값은 0)
        bar.value = currentValue / maxValue; // UI에 반영
    }

    // 상태 감소 함수 (슬라이더 없음)
    void DecreaseStat(ref float currentValue, float maxValue)
    {
        currentValue = Mathf.Max(0, currentValue - 1); // 현재 값 감소 (최소값은 0)
    }
}
