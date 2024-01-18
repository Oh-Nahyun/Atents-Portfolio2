using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Enemies : TestBase
{
    Transform spawmPoint;

    private void Start()
    {
        spawmPoint = transform.GetChild(0);
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        // 보너스 적
        Factory.Instance.GetBonus(spawmPoint.position);
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        // 커브 적
        Factory.Instance.GetCurve(spawmPoint.position);
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        // 보스 (총알)
        Factory.Instance.GetBossBullet(spawmPoint.position);
    }

    protected override void OnTest4(InputAction.CallbackContext context)
    {
        // 보스 (미사일)
    }

    protected override void OnTest5(InputAction.CallbackContext context)
    {
        // 보스 (본체)
    }
}
