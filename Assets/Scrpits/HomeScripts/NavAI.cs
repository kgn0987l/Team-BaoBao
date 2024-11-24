using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class NavAI : MonoBehaviour
{
    public float updateInterval = 3f; // ��ǥ ��ġ�� ������ �ð� ���� (��)
    public float idleDurationMin = 2f; // �ּ� Idle �ִϸ��̼� ��� �ð� (��)
    public float idleDurationMax = 3f; // �ִ� Idle �ִϸ��̼� ��� �ð� (��)

    private NavMeshAgent agent; // NavMeshAgent�� ������ ����
    private float timeSinceLastUpdate; // ���������� ��ǥ ��ġ�� �����ߴ� �ð�
    private Animator animator; // Animator�� ������ ����

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // NavMeshAgent ������Ʈ�� �����ɴϴ�.
        animator = GetComponentInChildren<Animator>(); // Animator ������Ʈ�� �����ɴϴ�.
        timeSinceLastUpdate = updateInterval; // �ʱ⿡ ��ǥ ��ġ�� �����ϱ� ���� �ð� ���� �����մϴ�.

        StartCoroutine(RandomMovementCoroutine()); // �����ϰ� �̵��ϴ� �ڷ�ƾ ����
    }

    void Update()
    {
        timeSinceLastUpdate += Time.deltaTime; // �ð� ���� �����մϴ�.
    }

    Vector3 GetRandomPositionOnNavMesh()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 20f; // ���ϴ� ���� ���� ������ ���� ���͸� �����մϴ�.
        randomDirection += transform.position; // ���� ���� ���͸� ���� ��ġ�� ���մϴ�.

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, 20f, NavMesh.AllAreas)) // ���� ��ġ�� NavMesh ���� �ִ��� Ȯ���մϴ�.
        {
            return hit.position; // NavMesh ���� ���� ��ġ�� ��ȯ�մϴ�.
        }
        else
        {
            return transform.position; // NavMesh ���� ���� ��ġ�� ã�� ���� ��� ���� ��ġ�� ��ȯ�մϴ�.
        }
    }

    private IEnumerator RandomMovementCoroutine()
    {
        while (true) // ���� ����
        {
            // ���� ��ġ�� �̵�
            Vector3 randomPosition = GetRandomPositionOnNavMesh(); // NavMesh ���� ������ ��ġ�� �����ɴϴ�.
            agent.SetDestination(randomPosition); // NavMeshAgent�� ��ǥ ��ġ�� ���� ��ġ�� �����մϴ�;

            // ������Ʈ�� ��ǥ ��ġ�� ������ ������ ���
            while (agent.remainingDistance > agent.stoppingDistance)
            {
                animator.SetBool("isWalking", true); // Walking �ִϸ��̼� ����
                yield return null; // ������ ���
            }

            animator.SetBool("isWalking", false); // Walking �ִϸ��̼� ����

            // Idle �ִϸ��̼� ���
            float idleDuration = Random.Range(idleDurationMin, idleDurationMax); // Idle �ִϸ��̼� ��� �ð� ����
            animator.SetBool("isWalking", false); // Idle �ִϸ��̼� ����
            agent.isStopped = true; // ������Ʈ ����

            yield return new WaitForSeconds(idleDuration); // Idle �ִϸ��̼� ��� �ð� ���

            agent.isStopped = false; // ������Ʈ�� �ٽ� �̵� �����ϰ� ����
        }
    }
}
