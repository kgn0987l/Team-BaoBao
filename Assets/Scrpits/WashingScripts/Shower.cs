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
    private RaycastHit hit;  //ray�� �浹������ �����ϴ� ����ü

    public int maxRaycastDistance = 10; // Raycast�� �ִ� �Ÿ�

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
        // Ray�� �������� ���� ����
        Vector3 origin = transform.position;  // ������Ʈ�� �߾� ��ġ
        Vector3 direction = transform.forward;  // ������Ʈ�� �ٶ󺸴� ����

        ray = new Ray(origin, direction);

        // Debug�� Ray �ð�ȭ
        Debug.DrawRay(ray.origin, ray.direction * maxRaycastDistance, Color.red, 1f);

        // Ray�� �浹�ߴ��� Ȯ��
        if (Physics.Raycast(ray, out hit, maxRaycastDistance))
        {
            string hitTag = hit.collider.gameObject.tag;  // �浹�� ������Ʈ�� �±� ĳ��

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
        Debug.Log("�ı����");

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
        WashingBar.value = curWashing / maxWashing;  // �����̴� ������Ʈ
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
