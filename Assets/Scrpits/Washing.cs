using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Washing : MonoBehaviour
{
    [SerializeField] private float mouseSpeed = 8f; // 회전속도
    [SerializeField] private GameObject pet;
    [SerializeField] private GameObject Clear;
    private Button btn;

    [SerializeField] public Slider BobleBar;
    [SerializeField] public Slider WashingBar;

    private float mouseX = 0f; // 좌우 회전값을 담을 변수

    private RaycastHit hit; // ray의 충돌정보를 저장하는 구조체
    private Ray ray;

    public int maxRaycastDistance = 10; // Raycast의 최대 거리

    private bool PatOnOff = true; // 씻기는 중인지 여부
    private bool canRot;
    private bool canUP;
    private bool canPat;
    public bool canShower = false;

    private float maxBoble = 100f;
    private float curBoble = 0f;

    private float maxWashing = 100f;
    private float curWashing = 0f;

    private int PatCount = 0;

    void Start()
    {
        btn = GameObject.Find("Shower Btn").GetComponent<Button>();
        btn.interactable = false;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && canRot && !canPat)
        {
            mouseX += Input.GetAxis("Mouse X") * mouseSpeed;
            //pet.transform.rotation = Quaternion.Euler(0, mouseX, 0);
        }

        // 마우스 왼쪽 버튼 클릭 시
        if (Input.GetMouseButtonDown(0))
        {
            if (!canRot && canUP && canPat)
            {
                ObjHit();
            }
            else if (!canUP && !canPat)
            {
                Exit();
            }
        }

        // 마우스 왼쪽 버튼 누르고 있을 때
        if (Input.GetMouseButton(0) && canPat)
        {
            Pat(); // 애완동물을 씻기는 동작
        }

        UpdateBars();
    }

    // 나가기 동작
    private void Exit()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Ray 생성

        Debug.DrawRay(ray.origin, ray.direction * maxRaycastDistance, Color.red, 1f);

        if (Physics.Raycast(ray, out hit, maxRaycastDistance))
        {
            if (hit.collider.gameObject.tag == "Pet")
            {
                //pet.transform.position = new Vector3(0, 0.5f, -0.4f);
                canUP = true;
                canPat = true;
                canRot = false;
            }
        }
    }

    private void ObjHit()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Ray 생성

        Debug.DrawRay(ray.origin, ray.direction * maxRaycastDistance, Color.red, 1f);

        if (Physics.Raycast(ray, out hit, maxRaycastDistance))
        {
            if (hit.collider.gameObject.tag == "Pet")
            {
                //pet.transform.position = new Vector3(0, 1.2f, -0.4f);
                canRot = true;
                canUP = false;
                canPat = false;
            }
        }
    }

    // 애완동물을 씻기는 동작
    private void Pat()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Ray 생성

        Debug.DrawRay(ray.origin, ray.direction * maxRaycastDistance, Color.red, 1f);

        if (Physics.Raycast(ray, out hit, maxRaycastDistance))
        {
            if (hit.collider.gameObject.tag == "Pet")
            {
                if (PatOnOff == true)
                {
                    Debug.Log("씻기기중");
                    PatCount++;
                    PatOnOff = false;

                    if (curBoble < maxBoble && !canShower)
                    {
                        curBoble += 5;
                        if(curBoble == maxBoble)
                        {
                            btn.interactable = true;
                        }
                    }
                    else if(canShower)
                    {
                        if (curWashing == maxWashing)
                        {
                            Clear.SetActive(true);
                            Invoke("HideClear", 1f);
                            Invoke("SceneChangeMethod",1.5f);
                            return;
                        }
                        curWashing += 5;
                        Debug.Log(curWashing);
                    }
                    Debug.Log(canShower);
                }
            }
            else
            {
                PatOnOff = true;
            }
        }
    }

    private void HideClear()
    {
        Clear.SetActive(false);
    }

    void UpdateBars()
    {
        BobleBar.value = curBoble / maxBoble;
        WashingBar.value = curWashing / maxWashing;
    }

    public void SceneChangeMethod()
    {
        SceneChangeManager.Instance.ChangeScene(3);
    }
}