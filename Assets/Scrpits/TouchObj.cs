using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class TouchObj : MonoBehaviour
{
    private RaycastHit hit; // ray의 충돌정보를 저장하는 구조체
    private Ray ray;

    public int maxRaycastDistance = 10; // Raycast의 최대 거리
    public Slider FeedBar;
    public Slider WaterBar;

    public Animator FeedAnim;
    public Animator WaterAnim;
    public Animator petAnim;
    public GameObject FeedBox, WaterBox, FeedBowl, WaterBowl, Bowls, myCamera, feedBar, waterBar, testfeed, testwater, Clear, FeedEffectobj, WaterEffectobj, MainCamera, SubCamera;

    public ParticleSystem FeedEffect;
    public ParticleSystem WaterEffect;

    private Vector3 startMousePosition;
    private bool isDragging = false;
    private float rotationSpeed = 0.1f; // 회전 속도 조절 변수

    private bool isClick = false;
    private bool FeedBoxClick = false;
    private bool WaterBoxClick = false;
    private bool FullFeed = false;
    private bool FullFeedExecuted = false; // FullFeed가 실행되었는지 여부를 추적하는 플래그
    private bool FullWater = false;
    private bool FullWaterExecuted = false; // FullWater가 실행되었는지 여부를 추적하는 플래그

    private float maxFeed = 100;
    private float curFeed = 0;

    private float maxWater = 100;
    private float curWater = 0;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ObjHit();
        }

        if (Input.GetMouseButton(0) && isClick)
        {
            rotateObj();
        }

        if(FullFeed && !FullFeedExecuted)
        {
            FeedBox.SetActive(false);
            feedBar.SetActive(false);
            if (!FullWater)
            {
                WaterBox.SetActive(true);
                waterBar.SetActive(true);
            }
            else
            {
                WaterBox.SetActive(false);
                waterBar.SetActive(false);
            }
            FeedBowl.SetActive(false);
            Bowls.SetActive(true);

            isClick = false;
            FeedBoxClick = false;
            FullFeedExecuted = true; // FullFeed 블록이 한 번만 실행되도록 설정

            myCamera.transform.position = new Vector3(0, 2, -3);
            testfeed.SetActive(true);

            if(FullFeed && FullWater)
            {
                MainCamera.SetActive(false);
                SubCamera.SetActive(true);
                petAnim.Play("Puddle_eat");
                Invoke("OnClear", 2f);
                DataManager.Instance.curHunger += 30;
                Invoke("SceneChangeMethod", 3.5f);
                return;
            }
        }

        if (FullWater && !FullWaterExecuted)
        {
            WaterBox.SetActive(false);
            waterBar.SetActive(false);
            if (!FullFeed)
            {
                FeedBox.SetActive(true);
                feedBar.SetActive(true);
            }
            else
            {
                FeedBox.SetActive(false);
                feedBar.SetActive(false);
                
            }
            WaterBowl.SetActive(false);
            Bowls.SetActive(true);

            isClick = false;
            WaterBoxClick = false;
            FullWaterExecuted = true; // FullFeed 블록이 한 번만 실행되도록 설정
            myCamera.transform.position = new Vector3(0, 2, -3);
            testwater.SetActive(true);

            if (FullFeed && FullWater)
            {
                MainCamera.SetActive(false);
                SubCamera.SetActive(true) ;
                petAnim.Play("Puddle_eat");
                Invoke("OnClear", 2f); 
                DataManager.Instance.curHunger += 30;
                Invoke("SceneChangeMethod", 3.5f);
                return;
            }
        }

        UpdateBars();
        FeedAnim.Play("Feed", -1, FeedBar.normalizedValue);
        WaterAnim.Play("Feed", -1, WaterBar.normalizedValue);
    }

    private void ObjHit()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Ray 생성

        Debug.DrawRay(ray.origin, ray.direction * maxRaycastDistance, Color.red, 1f);

        if (Physics.Raycast(ray, out hit, maxRaycastDistance))
        {
            if (hit.collider.gameObject.tag == "Feed Box" && !isClick)
            {
                FeedBowl.SetActive(true);
                feedBar.SetActive(true);
                WaterBox.SetActive(false);
                waterBar.SetActive(false);
                Bowls.SetActive(false);
                testwater.SetActive(false);
                FeedBox.transform.position = new Vector3(-1.6f, 2, 0);
                FeedBox.transform.localScale = new Vector3(1.4f, 2, 0.5f);
                myCamera.transform.position = new Vector3(0, 3, -4.5f);

                isClick = true;
                FeedBoxClick = true;
            }

            if (hit.collider.gameObject.tag == "Water Box" && !isClick)
            {
                WaterBowl.SetActive(true);
                waterBar.SetActive (true);
                FeedBox.SetActive(false);
                feedBar.SetActive(false);
                Bowls.SetActive(false);
                testfeed.SetActive(false);
                WaterBox.transform.position = new Vector3(-1.6f, 2, 0);
                WaterBox.transform.localScale = new Vector3(1.4f, 2, 0.5f);
                myCamera.transform.position = new Vector3(0, 3, -4.5f);

                isClick = true;
                WaterBoxClick = true;
            }
        }
    }

    private void rotateObj()
    {
        // 마우스 버튼을 클릭했을 때
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 시작 위치 저장
            startMousePosition = Input.mousePosition;
            isDragging = true;
        }

        // 마우스 버튼을 떼었을 때
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // 드래그 중일 때
        if (isDragging)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            float yDifference = currentMousePosition.y - startMousePosition.y;

            // 회전 각도 계산 (드래그 거리에 비례하게 조정)
            float rotationAmount = yDifference * rotationSpeed;

            // 현재 회전 각도를 Vector3로 가져오기
            Vector3 currentFeedRotation = FeedBox.transform.rotation.eulerAngles;
            Vector3 currentWaterRotation = WaterBox.transform.rotation.eulerAngles;

            // 새로운 회전 값 계산 (각 오브젝트에 대해 별도로 처리)
            float newFeedZRotation = currentFeedRotation.z + rotationAmount;
            float newWaterZRotation = currentWaterRotation.z + rotationAmount;

            // Unity의 오일러 각도는 0~360도 범위이므로 음수로 변환
            if (newFeedZRotation > 180) newFeedZRotation -= 360;
            if (newWaterZRotation > 180) newWaterZRotation -= 360;

            // 회전 각도를 0도에서 -90도 사이로 제한
            newFeedZRotation = Mathf.Clamp(newFeedZRotation, -90f, 0f);
            newWaterZRotation = Mathf.Clamp(newWaterZRotation, -90f, 0f);

            // 새로운 회전 값 적용 (각 오브젝트에 대해 별도로 처리)
            if (FeedBoxClick)
            {
                FeedBox.transform.rotation = Quaternion.Euler(currentFeedRotation.x, currentFeedRotation.y, newFeedZRotation);
            }
            else if (WaterBoxClick)
            {
                WaterBox.transform.rotation = Quaternion.Euler(currentWaterRotation.x, currentWaterRotation.y, newWaterZRotation);
            }

            // 마우스 위치 업데이트
            startMousePosition = currentMousePosition;

            // 회전 값이 -90도인지 확인하고 curFeed 또는 curWater 증가
            if (Mathf.Approximately(newFeedZRotation, -90f))
            {
                // 정확히 -90도일 때 이펙트를 재생
                if (!FeedEffect.isPlaying)
                {
                    FeedEffect.Play();
                }

                if (curFeed < maxFeed)
                {
                    curFeed += 1f; // 현재 사료 양 증가
                }

                if (curFeed >= maxFeed)
                {
                    FullFeed = true; // 사료가 가득 찼음을 표시
                    FeedEffectobj.SetActive(false);
                }
            }
            else
            {
                // -90도가 아닐 때 이펙트를 멈춤
                if (FeedEffect.isPlaying)
                {
                    FeedEffect.Stop();
                }
            }

            if (Mathf.Approximately(newWaterZRotation, -90f))
            {
                // 정확히 -90도일 때 이펙트를 재생
                if (!WaterEffect.isPlaying)
                {
                    WaterEffect.Play();
                }

                if (curWater < maxWater)
                {
                    curWater += 1f; // 현재 물 양 증가
                }

                if (curWater >= maxWater)
                {
                    FullWater = true; // 물이 가득 찼음을 표시
                    WaterEffectobj.SetActive(false);
                }
            }
            else
            {
                // -90도가 아닐 때 이펙트를 멈춤
                if (WaterEffect.isPlaying)
                {
                    WaterEffect.Stop();
                }
            }
        }
    }

    void UpdateBars()
    {
        FeedBar.value = curFeed / maxFeed;
        WaterBar.value = curWater / maxWater;
    }

    public void SceneChangeMethod()
    {
        SceneChangeManager.Instance.ChangeScene(3);
    }

    public void OnClear()
    {
        Clear.SetActive(true);
    }
}
