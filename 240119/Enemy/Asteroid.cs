using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : EnemyBase
{
    [Header("큰 운석 데이터")]

    /// 실습_240112
    /// 운석
    /// 1. 운석은 만들어졌을 때 지정되는 도착점을 향해서 움직인다.
    /// 2. 운석은 계속 회전한다.
    /// 3. 운석은 오브젝트 풀에서 관리되어야 한다.

    /// 스포너
    /// 1. 운석 생성용 스포너가 있어야 한다.
    /// 2. 운석을 생성하고 시작점과 도착점을 지정한다.
    /// 3. 도착점의 범위가 씬 창에서 보여야 한다.

    /// 스포너에서 생성할 때 스포너의 자식이 되는 문제 있음

    /* // 내가 작성한 코드
    public float moveSpeed = 7.0f;

    public float startPointY;
    public float endPointY;

    private void Start()
    {
        startPointY = -5.0f;
        endPointY = 5.0f;
    }

    void Update()
    {
        Vector3 way = new Vector3(-10.0f, endPointY, 0.0f) - new Vector3(10.0f, startPointY, 0.0f);
        way.Normalize();

        transform.position = transform.position + (Time.deltaTime * moveSpeed * way);
        transform.Rotate(0, 0, 1);
    }
    */



    /// 실습_240115
    /// 이동 속도가 랜덤해야 한다.
    /// 회전 속도도 랜덤이어야 한다.
    /// 큰 운석은 수명을 가진다. (수명이 다되면 죽는다)
    /// 수명도 랜덤이다.
    /// 큰 운석은 죽을 때 작은 운석을 랜덤한 개수를 생성한다.
    /// 모든 작은 운석은 서로 같은 사이각을 가진다. (작은 운석이 6개 생성 = 사이각 60도)
    /// criticalRate 확률로 작은 운석을 20개 생성한다.

    public float minMoveSpeed = 2.0f;
    public float maxMoveSpeed = 4.0f;

    public float minRotateSpeed = 30.0f;
    public float maxRotateSpeed = 360.0f;

    float lifeTime;
    public float minLifeTime = 4.0f;
    public float maxLifeTime = 7.0f;

    float miniCount;
    public int minMiniCount = 3;
    public int maxMiniCount = 8;

    [Range(0f, 1f)]
    public float criticalRate = 0.05f;
    public int criticalMiniCount = 20;



    //public float moveSpeed = 3.0f;

    /// <summary>
    /// 회전 속도
    /// </summary>
    float rotateSpeed = 360.0f;

    /// <summary>
    /// 이동 방향
    /// </summary>
    Vector3 direction = Vector3.zero;

    /// <summary>
    /// 원래 점수 (자폭했을 때 점수를 안주기 위해 필요)
    /// </summary>
    int originalScore;

    private void Awake()
    {
        originalScore = score;
    }

    protected override void OnInitialize()
    {
        base.OnInitialize();

        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed); // 이동 속도 랜덤
        rotateSpeed = Random.Range(minRotateSpeed, maxRotateSpeed); // 회전 속도 랜덤
        score = originalScore;

        StartCoroutine(SelfCrush());
    }

    /// <summary>
    /// 목적지를 이용해 방향을 결정하는 함수
    /// </summary>
    /// <param name="destination"></param>
    public void SetDestination(Vector3 destination)
    {
        /// 방법 1
        direction = (destination - transform.position).normalized;

        /// 방법 2
        //Vector3 vec = destination - transform.position;
        //direction = vec.normalized;

    }

    protected override void OnMoveUpdate(float deltaTime)
    {
        transform.Translate(Time.deltaTime * moveSpeed * direction, Space.World); // direction 방향으로 이동하기 (월드 기준)
        transform.Rotate(0, 0, Time.deltaTime * rotateSpeed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + direction); // 진행 방향 표시
    }

    IEnumerator SelfCrush()
    {
        float lifeTime = Random.Range(minLifeTime, maxLifeTime); // 큰 운석 수명 랜덤
        yield return new WaitForSeconds(lifeTime);
        score = 0;
        OnDie();
    }

    protected override void OnDie()
    {
        // 작은 운석 만들기
        int count = criticalMiniCount;

        if (Random.value > criticalRate)
        {
            count = Random.Range(minMiniCount, maxMiniCount); // 작은 운석 개수 랜덤
        }

        float angle = 360.0f / count;
        float startAngle = Random.Range(0, 360.0f);
        for (int i = 0; i < count; i++)
        {
            Factory.Instance.GetAsteroidMini(transform.position, startAngle + angle * i);
        }

        base.OnDie();
    }
}
