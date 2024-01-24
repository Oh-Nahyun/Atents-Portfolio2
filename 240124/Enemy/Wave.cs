using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : EnemyBase
{
    [Header("Wave 데이터")]

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

    ///////public GameObject effectPrefab;

    /// <summary>
    /// 이동 속도
    /// </summary>
    //public float speed = 1.0f;

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

    /*
    /// <summary>
    /// 적의 HP
    /// </summary>
    public int hp = 3;
    // int hpMax = 3;

    private int HP
    {
        get => hp;
        set
        {
            hp = value;
            if(hp <= 0) // HP가 0 이하가 되면 죽는다.
            {
                hp = 0;
                OnDie();
            }
        }
    }
    */

    /// <summary>
    /// 이 적을 해치웠을 때 플레이어가 얻는 점수
    /// </summary>
    ///////public int score = 10;

    /// <summary>
    /// 터질 때 나올 이팩트
    /// </summary>
    //public GameObject explosionPrefab;

    /// <summary>
    /// 적이 죽을 때 실행될 델리게이트
    /// </summary>
    //Action onDie;

    //Player player;

    protected override void OnEnable()
    {
        base.OnEnable();
        //OnInitialize(); // 적 초기화 작업

        /////원래 Start()에 있던 코드
        // 초기화
        spawnY = transform.position.y;
        elapsedTime = 0.0f;
        /// <summary>
        ///  람다, 람다함수(Lambda)
        ///  익명 함수
        ///  한 줄짜리 임시 함수, 1회용
        ///  </summary>
        //Action aaa = () => Debug.Log("람다함수"); // 파라메터 없는 람다식
        //Action<int> bbb = (x) => Debug.Log($"람다함수 {x}"); // 파라메터가 하나인 람다식
        //Func<int> ccc = () => 10; // 파라메터 없고 항상 10을 리턴하는 람다식
        ///Player player = FindAnyObjectByType<Player>(); // 시작할 때 플레이어 찾아서
        ///onDie += () => player.AddScore(score); // 죽을 때 플레이어의 AddScore 함수에 파라메터로 score를 넣고 실행하도록 등록
    }

    /*
    protected override void OnDisable()
    {
        if (player != null)
        {
            onDie -= PlayerAddScore;
            onDie = null;
            player = null;
        }

        base.OnDisable();
    }
    */

    /*
    void PlayerAddScore()
    {
        player.AddScore(score);
    }
    */

    /* // OnMoveUpdate로 이동
    private void Update()
    {
        //elapsedTime += Time.deltaTime; // 시작부터 진행된 시간 측정
        elapsedTime += Time.deltaTime * frequency; // sin 그래프의 진행을 더 빠르게 만들기

        transform.position = new Vector3(transform.position.x - Time.deltaTime * speed, // 계속 왼쪽으로 진행
                            spawnY + Mathf.Sin(elapsedTime) * amplitude, // sin 그래프에 따라 높이 변동하기
                            0.0f);
    }
    */

    // 실습_240104
    // 1. 적에게 HP 추가 (3대를 맞으면 폭발)
    // 2. 적이 폭발할 때 explosionEffect 생성
    // 3. 플레이어 총알 발사할 때 flash 잠깐 보이기
    // 4. 연사처리
    // 내가 쓴 코드_미완성본(3)
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(effectPrefab, transform.position, Quaternion.identity);
    }
    */

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet")
            || collision.gameObject.CompareTag("Player")) // 총알이 부딪치거나 플레이어가 부딪치면 HP가 1 감소한다.
        {
            HP--;
        }
    }
    */

    /*
    private void OnInitialize()
    {
        if (player == null)
        {
            player = GameManager.Instance.Player;
        }

        if (player != null)
        {
            //onDie += () => player.AddScore(score); // 죽을 때 플레이어의 AddScore 함수에 파라메터로 score를 넣고 실행하도록 등록
            onDie += PlayerAddScore;
        }
    }

    private void OnDie()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        //Player player = FindAnyObjectByType<Player>();
        //player.AddScore(score);
        onDie?.Invoke();

        //Destroy(gameObject); // 자기 자신 삭제
        gameObject.SetActive(false); // 자기 자신 비활성화
    }
    */

    /// <summary>
    /// 시작 위치 설정을 위한 함수
    /// </summary>
    /// <param name="position">시작 위치</param>
    public void SetStartPosition(Vector3 position)
    {
        //transform.position = position;
        spawnY = position.y;
    }

    protected override void OnMoveUpdate(float deltaTime)
    {
        //elapsedTime += Time.deltaTime // 시작부터 진행된 시간 측정
        elapsedTime += deltaTime * frequency; // sin 그래프의 진행을 더 빠르게 만들기

        transform.position = new Vector3(transform.position.x - deltaTime * moveSpeed, // 계속 왼쪽으로 진행
                            spawnY + Mathf.Sin(elapsedTime) * amplitude, // sin 그래프에 따라 높이 변동하기
                            0.0f);
    }
}
