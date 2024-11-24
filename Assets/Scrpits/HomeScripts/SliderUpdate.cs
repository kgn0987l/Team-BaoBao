using UnityEngine;
using UnityEngine.UI;

public class SliderUpdate : MonoBehaviour
{
    // ���� �� UI ��ҵ�
    public Slider FriendShipBar2;
    public Slider HungerBar;
    public Slider HygieneBar;
    public Slider MentalityBar;

    // �����̴��� Fill ���� �̹��� ����
    public Image FriendShipBarFill;
    public Image HungerBarFill;
    public Image HygieneBarFill;
    public Image MentalityBarFill;

    // ���� ���ݰ� Ÿ�̸�
    private float decreaseInterval = 5f;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        // �ֱ������� �� ���� ����
        if (timer >= decreaseInterval)
        {
            DecreaseStat(ref DataManager.Instance.curFriendShip, DataManager.Instance.maxFriendShip, FriendShipBar2);
            DecreaseStat(ref DataManager.Instance.curHunger, DataManager.Instance.maxHunger);
            DecreaseStat(ref DataManager.Instance.curHygiene, DataManager.Instance.maxHygiene);
            DecreaseStat(ref DataManager.Instance.curMentality, DataManager.Instance.maxMentality);
            timer = 0f; // Ÿ�̸� �ʱ�ȭ
        }

        UpdateBars();
    }

    // ���� �� ������Ʈ �� ���� ����
    void UpdateBars()
    {
        UpdateBar(FriendShipBar2, DataManager.Instance.curFriendShip, DataManager.Instance.maxFriendShip, FriendShipBarFill);
        UpdateBar(HungerBar, DataManager.Instance.curHunger, DataManager.Instance.maxHunger, HungerBarFill);
        UpdateBar(HygieneBar, DataManager.Instance.curHygiene, DataManager.Instance.maxHygiene, HygieneBarFill);
        UpdateBar(MentalityBar, DataManager.Instance.curMentality, DataManager.Instance.maxMentality, MentalityBarFill);
    }

    // ���� ���� �� ������Ʈ �� ���� ����
    void UpdateBar(Slider bar, float currentValue, float maxValue, Image fillImage)
    {
        float percentage = currentValue / maxValue;
        bar.value = percentage;

        // ���¿� ���� ���� ����
        if (percentage <= 0.2f)
        {
            fillImage.color = Color.red;  // 20% ���� ������
        }
        else if (percentage <= 0.6f)
        {
            fillImage.color = new Color(1f, 0.65f, 0f);  // ��Ȳ�� (RGB: 255, 165, 0)
        }
        else
        {
            fillImage.color = Color.green;  // 60% �ʰ� �ʷϻ�
        }
    }

    // ���� ���� �Լ� (�����̴� ����)
    void DecreaseStat(ref float currentValue, float maxValue, Slider bar)
    {
        currentValue = Mathf.Max(0, currentValue - 1); // ���� �� ���� (�ּҰ��� 0)
        bar.value = currentValue / maxValue; // UI�� �ݿ�
    }

    // ���� ���� �Լ� (�����̴� ����)
    void DecreaseStat(ref float currentValue, float maxValue)
    {
        currentValue = Mathf.Max(0, currentValue - 1); // ���� �� ���� (�ּҰ��� 0)
    }
}
