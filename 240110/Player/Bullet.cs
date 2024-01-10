using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : RecycleObject
{
    /// <summary>
    /// 총알의 이동 속도
    /// </summary>
    public float moveSpeed = 7.0f;

    /////public int i = 1;
    /////int j = Math.Pow((-1), i);
    /////Vector3 moveDir = new Vector3 (1, j, 0);

    /// <summary>
    /// 총알 터지는 이팩트용 프리팹
    /// </summary>
    public GameObject effectPrefab;

    //public float enemyHP = 100.0f;

    /// <summary>
    /// 총알의 수명
    /// </summary>
    public float lifeTime = 10.0f;

    //private void Start() // Start는 한 번만 실행되기 때문에 재사용할 때 문제가 된다.
    //{
    //    //Destroy(gameObject, lifeTime); // 총알 수명 // lifeTime 이후에 스스로 사라지기
    //    StartCoroutine(LifeOver(lifeTime));
    //}

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(LifeOver(lifeTime));
    }

    private void Update()
    {
        /// 시작하자마자 계속 오른쪽으로 초속 7로 움직이게 만들기
        transform.Translate(Time.deltaTime * moveSpeed * Vector2.right); // 총 곱한 수? 3번
        //transform.Translate(Vector2.right * Time.deltaTime * moveSpeed); // 총 곱한 수? 4번

        /////transform.Translate(Time.deltaTime * moveSpeedB * moveDir);
    }

    // 실습_240104
    // 1. bullet 프리팹에 필요한 컴포넌트 추가하고 설정하기
    // 2. 총알은 다른 오브젝트와 부딪치면 자기 자신을 삭제한다.
    // 3. 총알은 "Enemy" 태그를 가진 오브젝트와 부딪치면 부딪친 대상을 삭제한다.
    // 4. Hit 스프라이트를 이용해 HitEffect라는 프리팹 만들기
    // 5. 총알이 부딪친 위치에 HitEffect 생성하기
    // 6. HitEffect는 한 번만 재생된 후 사라진다.

    // 내 코드
    /*
    void HitEffect()
    {
        GameObject obj = Instantiate(hitPrefab, transform.position, Quaternion.identity); // (동적) 생성
    }
    */

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 내가 쓴 코드_미완성본(3)
        /*
        // 충돌이 시작했을 때 실행
        Debug.Log($"OnCollisionEnter2D : {collision.gameObject.name}");

        if (collision.gameObject.CompareTag("Enemy")) // collision의 게임 오브젝트가 "Enemy"라는 태그를 가지는지 확인하는 함수
        {
            if (enemyHP > 0)
            {
                enemyHP -= 35.0f;
                Debug.Log($"{enemyHP}");
            }

            else
            {
                Destroy(collision.gameObject); // 충돌한 대상을 제거하기
            }
        }

        Instantiate(effectPrefab, transform.position, Quaternion.identity); // hit 이팩트 생성
        Destroy(gameObject); // 자기 자신은 무조건 삭제
        */

        // 이제 부딪친 쪽에서 처리
        Instantiate(effectPrefab, transform.position, Quaternion.identity); // hit 이팩트 생성
        //Destroy(gameObject); // 자기 자신은 무조건 삭제
        gameObject.SetActive(false); // 비활성화 -> 풀로 되돌리기
    }
}
