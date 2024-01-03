using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 7.0f;

    /////public int i = 1;
    /////int j = Math.Pow((-1), i);
    /////Vector3 moveDir = new Vector3 (1, j, 0);

    private void Update()
    {
        /// 시작하자마자 계속 오른쪽으로 초속 7로 움직이게 만들기
        transform.Translate(Time.deltaTime * moveSpeed * Vector2.right); // 총 곱한 수? 3번
        //transform.Translate(Vector2.right * Time.deltaTime * moveSpeed); // 총 곱한 수? 4번


        /////transform.Translate(Time.deltaTime * moveSpeedB * moveDir);
    }
}
