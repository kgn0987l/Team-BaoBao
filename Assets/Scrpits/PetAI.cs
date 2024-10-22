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

    private Vector3 pos; // 목적지 변수 추가
    public float speed = 1.0f; // 이동 속도 변수 추가

    private void Awake()
    {
        ChangeState(State.IDLE);
    }

    void ChangeState(State s) // State 변경 
    {

        if (myState == s) return; // 지금 내 state 가 입력받은 state 와 같다면 return 
        myState = s; // 아니라면 내 state 에 입력받은 s 대입 

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
        pos = new Vector3(); //목적지 생성
        pos.x = Random.Range(-3f, 3f); //목적지 x 값은 -3~3 사이 랜덤값
        pos.z = Random.Range(-3f, 3f); // 목적지 z 값은 -3~3 사이 랜덤값


        myanim.SetBool("IsWalking", true); // 처음에 움직이니까 트루 
        while (true)
        {

            var dir = (pos - this.transform.position).normalized; // pos 포지션 - 강아지포지션 normalized 한후 
            this.transform.LookAt(pos); // 강아지가 이동할때 이동하는곳 바라보게하기위해 
            this.transform.position += dir * speed * Time.deltaTime; // 현재포지션에 normalize * 설정한 speed * Time.deltaTime 더해주기 

            float distance = Vector3.Distance(transform.position, pos); // 강아지가 걸어갈 거리
            if (distance <= 0.1f) // 0.1 이하라면 
            {
                myanim.SetBool("IsWalking", false); // 걷는 모션 false 후 Idle 들어갈예정 
                yield return new WaitForSeconds(Random.Range(1f, 3f)); // 랜덤으로 1~3 초기다린후 
                myanim.SetBool("IsWalking", true); // 다시 걷게하고 
                pos.x = Random.Range(-3f, 3f);// 위치설정
                pos.z = Random.Range(-3f, 3f);//위치설정 
            }
            yield return null;
        }
    }
}
