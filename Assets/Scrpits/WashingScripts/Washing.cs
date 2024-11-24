using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Washing : MonoBehaviour
{
    [SerializeField] private float mouseSpeed = 8f; // ȸ���ӵ�
    [SerializeField] private GameObject pet;
    [SerializeField] private GameObject Clear;
    private Button btn;

    [SerializeField] public Slider BobleBar;
    [SerializeField] public Slider WashingBar;

    private float mouseX = 0f; // �¿� ȸ������ ���� ����

    private RaycastHit hit; // ray�� �浹������ �����ϴ� ����ü
    private Ray ray;

    public int maxRaycastDistance = 10; // Raycast�� �ִ� �Ÿ�

    private bool PatOnOff = true; // �ı�� ������ ����
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

        // ���콺 ���� ��ư Ŭ�� ��
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

        // ���콺 ���� ��ư ������ ���� ��
        if (Input.GetMouseButton(0) && canPat)
        {
            Pat(); // �ֿϵ����� �ı�� ����
        }

        UpdateBars();
    }

    // ������ ����
    private void Exit()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Ray ����

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
        ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Ray ����

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

    // �ֿϵ����� �ı�� ����
    private void Pat()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Ray ����

        Debug.DrawRay(ray.origin, ray.direction * maxRaycastDistance, Color.red, 1f);

        if (Physics.Raycast(ray, out hit, maxRaycastDistance))
        {
            if (hit.collider.gameObject.tag == "Pet")
            {
                if (PatOnOff == true)
                {
                    Debug.Log("�ı����");
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