using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class NavAI : MonoBehaviour
{
    public float updateInterval = 3f; // 목표 위치를 갱신할 시간 간격 (초)
    public float idleDurationMin = 2f; // 최소 Idle 애니메이션 재생 시간 (초)
    public float idleDurationMax = 3f; // 최대 Idle 애니메이션 재생 시간 (초)

    private NavMeshAgent agent; // NavMeshAgent를 저장할 변수
    private float timeSinceLastUpdate; // 마지막으로 목표 위치를 갱신했던 시간
    private Animator animator; // Animator를 저장할 변수

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // NavMeshAgent 컴포넌트를 가져옵니다.
        animator = GetComponentInChildren<Animator>(); // Animator 컴포넌트를 가져옵니다.
        timeSinceLastUpdate = updateInterval; // 초기에 목표 위치를 설정하기 위해 시간 값을 설정합니다.

        StartCoroutine(RandomMovementCoroutine()); // 랜덤하게 이동하는 코루틴 시작
    }

    void Update()
    {
        timeSinceLastUpdate += Time.deltaTime; // 시간 값을 갱신합니다.
    }

    Vector3 GetRandomPositionOnNavMesh()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 20f; // 원하는 범위 내의 랜덤한 방향 벡터를 생성합니다.
        randomDirection += transform.position; // 랜덤 방향 벡터를 현재 위치에 더합니다.

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, 20f, NavMesh.AllAreas)) // 랜덤 위치가 NavMesh 위에 있는지 확인합니다.
        {
            return hit.position; // NavMesh 위의 랜덤 위치를 반환합니다.
        }
        else
        {
            return transform.position; // NavMesh 위의 랜덤 위치를 찾지 못한 경우 현재 위치를 반환합니다.
        }
    }

    private IEnumerator RandomMovementCoroutine()
    {
        while (true) // 무한 루프
        {
            // 랜덤 위치로 이동
            Vector3 randomPosition = GetRandomPositionOnNavMesh(); // NavMesh 위의 랜덤한 위치를 가져옵니다.
            agent.SetDestination(randomPosition); // NavMeshAgent의 목표 위치를 랜덤 위치로 설정합니다;

            // 에이전트가 목표 위치에 도달할 때까지 대기
            while (agent.remainingDistance > agent.stoppingDistance)
            {
                animator.SetBool("isWalking", true); // Walking 애니메이션 실행
                yield return null; // 프레임 대기
            }

            animator.SetBool("isWalking", false); // Walking 애니메이션 중지

            // Idle 애니메이션 재생
            float idleDuration = Random.Range(idleDurationMin, idleDurationMax); // Idle 애니메이션 재생 시간 설정
            animator.SetBool("isWalking", false); // Idle 애니메이션 시작
            agent.isStopped = true; // 에이전트 멈춤

            yield return new WaitForSeconds(idleDuration); // Idle 애니메이션 재생 시간 대기

            agent.isStopped = false; // 에이전트를 다시 이동 가능하게 설정
        }
    }
}
