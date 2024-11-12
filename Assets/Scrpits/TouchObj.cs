using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class TouchObj : MonoBehaviour
{
    private RaycastHit hit; // ray�� �浹������ �����ϴ� ����ü
    private Ray ray;

    public int maxRaycastDistance = 10; // Raycast�� �ִ� �Ÿ�
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
    private float rotationSpeed = 0.1f; // ȸ�� �ӵ� ���� ����

    private bool isClick = false;
    private bool FeedBoxClick = false;
    private bool WaterBoxClick = false;
    private bool FullFeed = false;
    private bool FullFeedExecuted = false; // FullFeed�� ����Ǿ����� ���θ� �����ϴ� �÷���
    private bool FullWater = false;
    private bool FullWaterExecuted = false; // FullWater�� ����Ǿ����� ���θ� �����ϴ� �÷���

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
            FullFeedExecuted = true; // FullFeed ����� �� ���� ����ǵ��� ����

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
            FullWaterExecuted = true; // FullFeed ����� �� ���� ����ǵ��� ����
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
        ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Ray ����

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
        // ���콺 ��ư�� Ŭ������ ��
        if (Input.GetMouseButtonDown(0))
        {
            // ���콺 ���� ��ġ ����
            startMousePosition = Input.mousePosition;
            isDragging = true;
        }

        // ���콺 ��ư�� ������ ��
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // �巡�� ���� ��
        if (isDragging)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            float yDifference = currentMousePosition.y - startMousePosition.y;

            // ȸ�� ���� ��� (�巡�� �Ÿ��� ����ϰ� ����)
            float rotationAmount = yDifference * rotationSpeed;

            // ���� ȸ�� ������ Vector3�� ��������
            Vector3 currentFeedRotation = FeedBox.transform.rotation.eulerAngles;
            Vector3 currentWaterRotation = WaterBox.transform.rotation.eulerAngles;

            // ���ο� ȸ�� �� ��� (�� ������Ʈ�� ���� ������ ó��)
            float newFeedZRotation = currentFeedRotation.z + rotationAmount;
            float newWaterZRotation = currentWaterRotation.z + rotationAmount;

            // Unity�� ���Ϸ� ������ 0~360�� �����̹Ƿ� ������ ��ȯ
            if (newFeedZRotation > 180) newFeedZRotation -= 360;
            if (newWaterZRotation > 180) newWaterZRotation -= 360;

            // ȸ�� ������ 0������ -90�� ���̷� ����
            newFeedZRotation = Mathf.Clamp(newFeedZRotation, -90f, 0f);
            newWaterZRotation = Mathf.Clamp(newWaterZRotation, -90f, 0f);

            // ���ο� ȸ�� �� ���� (�� ������Ʈ�� ���� ������ ó��)
            if (FeedBoxClick)
            {
                FeedBox.transform.rotation = Quaternion.Euler(currentFeedRotation.x, currentFeedRotation.y, newFeedZRotation);
            }
            else if (WaterBoxClick)
            {
                WaterBox.transform.rotation = Quaternion.Euler(currentWaterRotation.x, currentWaterRotation.y, newWaterZRotation);
            }

            // ���콺 ��ġ ������Ʈ
            startMousePosition = currentMousePosition;

            // ȸ�� ���� -90������ Ȯ���ϰ� curFeed �Ǵ� curWater ����
            if (Mathf.Approximately(newFeedZRotation, -90f))
            {
                // ��Ȯ�� -90���� �� ����Ʈ�� ���
                if (!FeedEffect.isPlaying)
                {
                    FeedEffect.Play();
                }

                if (curFeed < maxFeed)
                {
                    curFeed += 1f; // ���� ��� �� ����
                }

                if (curFeed >= maxFeed)
                {
                    FullFeed = true; // ��ᰡ ���� á���� ǥ��
                    FeedEffectobj.SetActive(false);
                }
            }
            else
            {
                // -90���� �ƴ� �� ����Ʈ�� ����
                if (FeedEffect.isPlaying)
                {
                    FeedEffect.Stop();
                }
            }

            if (Mathf.Approximately(newWaterZRotation, -90f))
            {
                // ��Ȯ�� -90���� �� ����Ʈ�� ���
                if (!WaterEffect.isPlaying)
                {
                    WaterEffect.Play();
                }

                if (curWater < maxWater)
                {
                    curWater += 1f; // ���� �� �� ����
                }

                if (curWater >= maxWater)
                {
                    FullWater = true; // ���� ���� á���� ǥ��
                    WaterEffectobj.SetActive(false);
                }
            }
            else
            {
                // -90���� �ƴ� �� ����Ʈ�� ����
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
