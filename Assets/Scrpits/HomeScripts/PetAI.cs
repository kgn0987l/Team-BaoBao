using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PetAI : MonoBehaviour
{

    public enum State
    {
        CREATE, IDLE, ROAMING
    }

    public State myState = State.CREATE;
    public Animator myanim;

    private Vector3 pos; // ������ ���� �߰�
    public float speed = 1.0f; // �̵� �ӵ� ���� �߰�

    private void Awake()
    {
        ChangeState(State.IDLE);
    }

    void ChangeState(State s) // State ���� 
    {

        if (myState == s) return; // ���� �� state �� �Է¹��� state �� ���ٸ� return 
        myState = s; // �ƴ϶�� �� state �� �Է¹��� s ���� 

        switch (myState)
        {
            case State.CREATE:
                break;
            case State.IDLE:
                ChangeState(State.ROAMING);
                break;
            case State.ROAMING:
                StartCoroutine(Roaming());
                break;
        }
    }

    IEnumerator Roaming()
    {
        pos = new Vector3(); //������ ����
        pos.x = Random.Range(-3f, 3f); //������ x ���� -3~3 ���� ������
        pos.z = Random.Range(-3f, 3f); // ������ z ���� -3~3 ���� ������


        myanim.SetBool("IsWalking", true); // ó���� �����̴ϱ� Ʈ�� 
        while (true)
        {

            var dir = (pos - this.transform.position).normalized; // pos ������ - ������������ normalized ���� 
            this.transform.LookAt(pos); // �������� �̵��Ҷ� �̵��ϴ°� �ٶ󺸰��ϱ����� 
            this.transform.position += dir * speed * Time.deltaTime; // ���������ǿ� normalize * ������ speed * Time.deltaTime �����ֱ� 

            float distance = Vector3.Distance(transform.position, pos); // �������� �ɾ �Ÿ�
            if (distance <= 0.1f) // 0.1 ���϶�� 
            {
                myanim.SetBool("IsWalking", false); // �ȴ� ��� false �� Idle ������ 
                yield return new WaitForSeconds(Random.Range(1f, 3f)); // �������� 1~3 �ʱ�ٸ��� 
                myanim.SetBool("IsWalking", true); // �ٽ� �Ȱ��ϰ� 
                pos.x = Random.Range(-3f, 3f);// ��ġ����
                pos.z = Random.Range(-3f, 3f);//��ġ���� 
            }
            yield return null;
        }
    }
}
