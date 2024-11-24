using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Sponge : MonoBehaviour
{
    [SerializeField] private GameObject pet;
    [SerializeField] public Slider BobleBar;
    [SerializeField] private Animator anim;

    public ParticleSystem Bubleeffect;

    private Button btn;

    private Ray ray;
    private RaycastHit hit;  //ray의 충돌정보를 저장하는 구조체

    public int maxRaycastDistance = 10; // Raycast의 최대 거리

    private float maxBoble = 100f;
    private float curBoble = 0f;

    private bool PatOnOff = true;
    public bool canShower = false;

    void Start()
    {
        btn = GameObject.Find("Shower Btn").GetComponent<Button>();
        btn.interactable = false;
    }

    void Update()
    {
        Pat();
        UpdateBars();
    }

    private void Pat()
    {
        // Ray의 시작점과 방향 설정
        Vector3 origin = transform.position;  // 오브젝트의 중앙 위치
        Vector3 direction = transform.forward;  // 오브젝트가 바라보는 방향

        ray = new Ray(origin, direction);

        // Debug용 Ray 시각화
        Debug.DrawRay(ray.origin, ray.direction * maxRaycastDistance, Color.red, 1f);

        // Ray가 충돌했는지 확인
        if (Physics.Raycast(ray, out hit, maxRaycastDistance))
        {
            string hitTag = hit.collider.gameObject.tag;  // 충돌한 오브젝트의 태그 캐싱

            if (hitTag == "Pet" && PatOnOff)
            {
                ProcessPat();
                Bubleeffect.Play();
                anim.SetBool("Washing", true);
            }
            else if (hitTag != "Pet")
            {
                ResetPatOnOff();
                Bubleeffect.Stop();
                anim.SetBool("Washing", false);
            }
        }
    }

    private void ProcessPat()
    {
        Debug.Log("씻기기중");
        PatOnOff = false;  // 한 번만 작동하도록

        Debug.Log(curBoble);

        // 거품 증가 및 상태 업데이트
        if (curBoble < maxBoble && !canShower)
        {
            curBoble += 5;
            UpdateBubbleBar();

            // 최대치에 도달하면 버튼 활성화
            if (curBoble >= maxBoble)
            {
                btn.interactable = true;
            }
        }
    }

    private void UpdateBubbleBar()
    {
        BobleBar.value = curBoble / maxBoble;  // 슬라이더 업데이트
    }

    private void ResetPatOnOff()
    {
        PatOnOff = true;  // Ray가 "Pet"에 닿지 않으면 다시 활성화
    }

    void UpdateBars()
    {
        BobleBar.value = curBoble / maxBoble;
    }
}
