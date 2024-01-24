using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : RecycleObject
{
    /// 실습_240115
    /// 파워업 아이템
    /// 플레이어가 먹었을 때
    /// - 플레이어가 1단계에서 먹었을 때 : 플레이어의 총알이 2개씩 나간다.
    /// - 플레이어가 2단계에서 먹었을 때 : 플레이어의 총알이 3개씩 나간다.
    /// - 플레이어가 3단계에서 먹었을 때 : 보너스 점수가 1000점 올라간다.
    /// 파워업 아이템은 랜덤한 방향으로 움직인다.
    /// - 일정한 시간 간격으로 이동 방향이 변경된다.
    /// - 높은 확률로 플레이어 반대쪽 방향을 선택한다.
    /* // 내가 써본 코드
    int playerLevel = 0;

    /// <summary>
    /// 이동 속도
    /// </summary>
    public float moveSpeed = 5.0f;

    /// <summary>
    /// 이동 방향
    /// </summary>
    Vector3 direction = Vector3.zero;

    /// <summary>
    /// 아이템 스폰 위치 범위
    /// </summary>
    float minRange = -5.0f;
    float maxRange = 5.0f;

    public void SetDestination(Vector3 itemSpawn)
    {
        float spawnHeight = Random.Range(minRange, maxRange);
        Vector3 destination = new Vector3 (-10.0f, spawnHeight, 0.0f);
        direction = (destination - itemSpawn).normalized;
    }

    private void Update()
    {
        SetDestination(transform.position);
        transform.Translate(Time.deltaTime * moveSpeed * direction, Space.World); // direction 방향으로 이동
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerLevel += 1;

            switch (playerLevel)
            {
                case 1:
                    Debug.Log("총알 2개 발사");
                    Destroy(gameObject); // 자기 자신은 무조건 삭제
                    break;
                case 2:
                    Debug.Log("총알 3개 발사");
                    Destroy(gameObject); // 자기 자신은 무조건 삭제
                    break;
                default:
                    Debug.Log("보너스 점수 +1000");
                    Destroy(gameObject); // 자기 자신은 무조건 삭제
                    break;
            }
        }
    }
    */

    /// 실습_240117
    /// 1. 파워업 아이템은 최대 횟수만큼만 방향 전환을 할 수 있다. (벽에 부딪쳐서 방향이 전환된 것도 1회로 취급)
    /// 2. 애니메이터를 이용해서 남아있는 방향 전환 횟수에 비례해서 빠르게 깜박인다.

    /// <summary>
    /// 이동 속도
    /// </summary>
    public float moveSpeed = 2.0f;

    /// <summary>
    /// 방향이 전환되는 시간 간격
    /// </summary>
    public float dirChangeInterval = 1.0f;

    /// <summary>
    /// 방향 전환이 가능한 횟수 (최대치)
    /// </summary>
    public int dirChangeCountMax = 5;

    /// <summary>
    /// 남아있는 방향 전환 횟수
    /// </summary>
    int dirChangeCount = 5;

    /// <summary>
    /// 방향 전환 횟수 설정 및 확인용 프로퍼티
    /// </summary>
    int DirChangeCount
    {
        get => dirChangeCount;
        set
        {
            dirChangeCount = value; // 값을 변경시키고
            animator.SetInteger("Count", dirChangeCount); // 애니메이터 파라메터 수정

            StopAllCoroutines(); // 이전에 돌아가던 코루틴 정지 (벽에 부딪쳤을 때 필요)

            if (dirChangeCount > 0 && gameObject.activeSelf) // 방향 전환 횟수가 남아있고 활성화 중이면
            {
                //Debug.Log($"방향 전환 남은 횟수 : {DirChangeCount}");
                StartCoroutine(DirectionChange()); // 일정 시간 후에 방향을 전환하는 코루틴 실행
            }
        }
    }

    /// <summary>
    /// 현재 이동 방향
    /// </summary>
    Vector3 direction;

    /// <summary>
    /// 플레이어의 트랜스폼
    /// </summary>
    Transform playerTransform;

    /// <summary>
    /// 애니메이터
    /// </summary>
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        StopAllCoroutines(); // 혹시나 실행되고 있을지도 모르는 모든 코루틴 정지

        playerTransform = GameManager.Instance.Player.transform;

        direction = Vector3.zero; // 방향 0로 해서 안움직이게
        DirChangeCount = dirChangeCountMax; // 방향 전환 횟수 초기화
        //StartCoroutine(DirectionChange()); // 코루틴 실행
    }

    /// <summary>
    /// 주기적으로 방향을 전환하는 코루틴
    /// </summary>
    IEnumerator DirectionChange()
    {
        yield return new WaitForSeconds(dirChangeInterval);

        // 약 70% 확률로 플레이어 반대 방향으로 움직임
        if (Random.value < 0.4f)
        {
            // 플레이어 반대 방향
            Vector2 playerToPowerUp = transform.position - playerTransform.position; // 방향 벡터 구하고
            direction = Quaternion.Euler(0, 0, Random.Range(-90.0f, 90.0f)) * playerToPowerUp; // +-90도 사이로 회전
        }
        else
        {
            direction = Random.insideUnitCircle; // 반지름 1짜리 원 내부의 랜덤한 지점으로 가는 방향 저장
            // 모든 방향이 50% 확률로 플레이어 반대 방향
        }

        direction.Normalize(); // 구한 방향의 크기를 1로 설정
                               //dir = Vector3.up; // 테스트 코드

        DirChangeCount--; // 방향 전환 횟수 감소
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * moveSpeed * direction); // 항상 direction 방향으로 이동
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (DirChangeCount > 0 && collision.gameObject.CompareTag("Border")) // 보더랑 부딪치면
        {
            direction = Vector2.Reflect(direction, collision.contacts[0].normal); // 이동 방향 반사시키기
            DirChangeCount--;
        }
    }
}
