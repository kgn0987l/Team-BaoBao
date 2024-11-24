using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Shower : MonoBehaviour
{
    [SerializeField] private GameObject pet;
    [SerializeField] public Slider WashingBar;
    [SerializeField] private GameObject Clear;
    [SerializeField] private Animator anim;

    public ParticleSystem Watereffect;

    private Ray ray;
    private RaycastHit hit;  //ray의 충돌정보를 저장하는 구조체

    public int maxRaycastDistance = 10; // Raycast의 최대 거리

    private float maxWashing = 100f;
    private float curWashing = 0f;

    public bool canShower = false;
    private bool isClearActivated = false;

    void Update()
    {
        Pat();
        UpdateWashingBar();
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

            if (hitTag == "Pet")
            {
                ProcessPat();
                Watereffect.Play();
                anim.SetBool("Washing", true);
            }
            else
            {
                Watereffect.Stop();
                anim.SetBool("Washing", false);
            }
        }
    }

    private void ProcessPat()
    {
        Debug.Log("씻기기중");

        if (curWashing >= maxWashing && !isClearActivated)
        {
            isClearActivated = true;
            Clear.SetActive(true);
            DataManager.Instance.curHygiene += 30;
            Invoke("HideClear", 1f);
            Invoke("SceneChangeMethod", 1.5f);
            return;
        }
        canShower = true;
        curWashing += 1f;
        Debug.Log(curWashing);
    }

    private void UpdateWashingBar()
    {
        WashingBar.value = curWashing / maxWashing;  // 슬라이더 업데이트
    }

    private void HideClear()
    {
        Clear.SetActive(false);
    }

    public void SceneChangeMethod()
    {
        SceneChangeManager.Instance.ChangeScene(3);
    }
}
