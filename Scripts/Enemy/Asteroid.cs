using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

    //public float moveSpeed = 3.0f;

    /// <summary>
    /// 회전 속도
    /// </summary>
    public float rotateSpeed = 360.0f;

    /// <summary>
    /// 이동 방향
    /// </summary>
    Vector3 direction = Vector3.zero;

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
}
