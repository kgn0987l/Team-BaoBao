using UnityEngine;
using UnityEngine.UI;

public class ObjectToggle : MonoBehaviour
{
    public GameObject[] objects;  // 3개의 오브젝트들이 들어있는 배열
    public Button[] buttons;      // 3개의 UI 버튼들

    void Start()
    {
        // 각각의 버튼에 클릭 이벤트 연결
        buttons[0].onClick.AddListener(() => ToggleObject(0));
        buttons[1].onClick.AddListener(() => ToggleObject(1));
        buttons[2].onClick.AddListener(() => ToggleObject(2));
    }

    // 특정 인덱스의 오브젝트를 토글하는 함수
    void ToggleObject(int index)
    {
        if (objects[index].activeSelf)
        {
            // 선택된 오브젝트가 이미 켜져있다면 비활성화(옷 벗기기)
            objects[index].SetActive(false);
        }
        else
        {
            // 선택된 오브젝트를 켜고 나머지를 끔
            for (int i = 0; i < objects.Length; i++)
            {
                if (i == index)
                {
                    objects[i].SetActive(true);  // 선택된 오브젝트를 켬
                }
                else
                {
                    objects[i].SetActive(false);  // 나머지 오브젝트를 끔
                }
            }
        }
    }
}