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
    private RaycastHit hit;  //ray�� �浹������ �����ϴ� ����ü

    public int maxRaycastDistance = 10; // Raycast�� �ִ� �Ÿ�

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
        Debug.Log("�ı����");
        PatOnOff = false;  // �� ���� �۵��ϵ���

        Debug.Log(curBoble);

        // ��ǰ ���� �� ���� ������Ʈ
        if (curBoble < maxBoble && !canShower)
        {
            curBoble += 5;
            UpdateBubbleBar();

            // �ִ�ġ�� �����ϸ� ��ư Ȱ��ȭ
            if (curBoble >= maxBoble)
            {
                btn.interactable = true;
            }
        }
    }

    private void UpdateBubbleBar()
    {
        BobleBar.value = curBoble / maxBoble;  // �����̴� ������Ʈ
    }

    private void ResetPatOnOff()
    {
        PatOnOff = true;  // Ray�� "Pet"�� ���� ������ �ٽ� Ȱ��ȭ
    }

    void UpdateBars()
    {
        BobleBar.value = curBoble / maxBoble;
    }
}
