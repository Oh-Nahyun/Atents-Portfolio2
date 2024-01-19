using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    /// 실습_240103
    /// 1. 일정한 시간 간격으로 한마리씩 적을 스폰한다.
    /// 2. 랜덤한 높이로 생성된다. (y : +4 ~ -4)

    // 내가 쓴 코드_미완성본(1)
    /*
    private void Awake()
    {
        float rand = Random.Range(-4.0f, 4.0f); // 랜덤으로 -4 ~ 4
        Vector3 enemyDir = new Vector3(10, rand, 0);
    }

    private void Update()
    {
        ///// 적 스폰 위치 설정 방법 미숙
        // Time.deltaTime을 누적시키키 == 시간측정
        if (Time.deltaTime % 10 == 0)
        {
            transform.Translate(enemyDir);
        }
    }
    */

    //public GameObject enemyPrefab;
    public float interval = 0.5f;

    protected const float MinY = -4.0f;
    protected const float MaxY = 4.0f;

    protected const float MinX = -0.3f;
    protected const float MaxX = 0.3f;

    //float elapsedTime = 0.0f;

    //int spawnCounter = 0;

    private void Awake()
    {
        
    }

    private void Start()
    {
        //spawnCounter = 0;
        //elapsedTime = 0.0f;

        StartCoroutine(SpawnCoroutine()); // SpawnCoroutine 코루틴 실행하기
    }

    IEnumerator SpawnCoroutine()
    {
        while(true) // 무한 반복
        {
            yield return new WaitForSeconds(interval); // interval만큼 기다린 후
            Spawn(); // Spawn 실행
        }
    }

    /*
    private void Update()
    {
        // Time.deltaTime을 누적시키키 == 시간측정
        elapsedTime += Time.deltaTime; // 시간 측정하기

        if (elapsedTime > Interval)
        {
            elapsedTime = 0.0f;
            Spawn();
        }
    }
    */

    /// <summary>
    /// 적을 하나 스폰하는 함수
    /// </summary>
    protected virtual void Spawn()
    {
        //GameObject obj = Instantiate(enemyPrefab, GetSpawnPosition(), Quaternion.identity); // (동적) 생성
        //obj.transform.SetParent(transform); // 부모 설정
        //obj.name = $"Enemy_{spawnCounter}"; // 게임 오브젝트 이름 바꾸기
        //spawnCounter++;

        Factory.Instance.GetEnemyWave(GetSpawnPosition());
        //Enemy enemy = Factory.Instance.GetEnemy(GetSpawnPosition());
        //enemy.transform.SetParent(transform);
    }

    /// <summary>
    /// 스폰할 위치를 리턴하는 함수
    /// </summary>
    /// <returns>스폰할 위치</returns>
    protected Vector3 GetSpawnPosition()
    {
        Vector3 pos = transform.position;
        pos.y += Random.Range(MinY, MaxY); // 현재 위치에서 높이만 (-4 ~ +4) 변경

        return pos;
    }

    protected virtual void OnDrawGizmos()
    {
        //Gizmos.color = Color.green; // 색깔 지정
        Gizmos.color = new Color(1f, 1f, 0f);
        Vector3 p0 = transform.position + Vector3.up * MaxY; // 선의 시작점 계산
        Vector3 p1 = transform.position + Vector3.up * MinY; // 선의 도착점 계산
        Gizmos.DrawLine(p0, p1); // 시작점에서 도착점으로 선을 긋는다.
    }

    protected virtual void OnDrawGizmosSelected()
    {
        // 이 오브젝트를 선택했을 때, 사각형 그리기 (색상 변경하기)
        Gizmos.color = Color.red;
        Vector3 p0 = transform.position + Vector3.up * MaxY + Vector3.right * MinX;
        Vector3 p1 = transform.position + Vector3.up * MinY + Vector3.right * MinX;
        Vector3 p2 = transform.position + Vector3.up * MaxY + Vector3.right * MaxX;
        Vector3 p3 = transform.position + Vector3.up * MinY + Vector3.right * MaxX;
        Gizmos.DrawLine(p0, p1);
        Gizmos.DrawLine(p0, p2);
        Gizmos.DrawLine(p1, p3);
        Gizmos.DrawLine(p2, p3);
    }
}
