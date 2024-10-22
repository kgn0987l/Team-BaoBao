using UnityEngine;
using UnityEngine.UI;

public class ObjectToggle : MonoBehaviour
{
    public GameObject[] objects;  // 3���� ������Ʈ���� ����ִ� �迭
    public Button[] buttons;      // 3���� UI ��ư��

    void Start()
    {
        // ������ ��ư�� Ŭ�� �̺�Ʈ ����
        buttons[0].onClick.AddListener(() => ToggleObject(0));
        buttons[1].onClick.AddListener(() => ToggleObject(1));
        buttons[2].onClick.AddListener(() => ToggleObject(2));
    }

    // Ư�� �ε����� ������Ʈ�� ����ϴ� �Լ�
    void ToggleObject(int index)
    {
        if (objects[index].activeSelf)
        {
            // ���õ� ������Ʈ�� �̹� �����ִٸ� ��Ȱ��ȭ(�� �����)
            objects[index].SetActive(false);
        }
        else
        {
            // ���õ� ������Ʈ�� �Ѱ� �������� ��
            for (int i = 0; i < objects.Length; i++)
            {
                if (i == index)
                {
                    objects[i].SetActive(true);  // ���õ� ������Ʈ�� ��
                }
                else
                {
                    objects[i].SetActive(false);  // ������ ������Ʈ�� ��
                }
            }
        }
    }
}