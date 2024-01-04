using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /// 실습_240103
    /// 1. 적은 위아래로 파도치듯이 움직인다.
    /// 2. 적은 계속 왼쪽 방향으로 이동한다.

    // 내가 쓴 코드_미완성본(2)
    /*
    public float moveSpeed = 5.0f;

    private void Awake()
    {
        float sin = Mathf.Sin(90.0f * Mathf.Def2Rad); // 90도를 라디안으로 변경해서 sin 결과 구하기
        Vector3 enemyDir = new Vector2.y(sin);
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * moveSpeed * Vector2.left * enemyDir);
    }
     */

    public GameObject effectPrefab;

    /// <summary>
    /// 이동 속도
    /// </summary>
    public float speed = 1.0f;

    /// <summary>
    /// 위 아래로 움직이는 정도
    /// </summary>
    public float amplitude = 3.0f;

    /// <summary>
    /// sin 그래프가 한번 왕복하는데 걸리는 시간 증폭용
    /// </summary>
    public float frequency = 2.0f;

    /// <summary>
    /// 적이 스폰된 높이
    /// </summary>
    float spawnY = 0.0f;

    /// <summary>
    /// 전체 경과 시간 (frequency에 의해 증폭)
    /// </summary>
    float elapsedTime = 0.0f;

    private void Start()
    {
        // 초기화
        spawnY = transform.position.y;
        elapsedTime = 0.0f;
    }

    private void Update()
    {
        //elapsedTime += Time.deltaTime; // 시작부터 진행된 시간 측정
        elapsedTime += Time.deltaTime * frequency; // sin 그래프의 진행을 더 빠르게 만들기

        transform.position = new Vector3(transform.position.x - Time.deltaTime * speed, // 계속 왼쪽으로 진행
            spawnY + Mathf.Sin(elapsedTime) * amplitude, // sin 그래프에 따라 높이 변동하기
            0.0f);
    }

    // 실습_240104
    // 1. 적에게 HP 추가 (3대를 맞으면 폭발)
    // 2. 적이 폭발할 때 explosionEffect 생성
    // 3. 플레이어 총알 발사할 때 flash 잠깐 보이기
    // 4. 연사처리
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(effectPrefab, transform.position, Quaternion.identity);
    }
}
