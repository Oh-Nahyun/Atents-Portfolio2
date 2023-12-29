using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // InputManager : 기존의 유니티 입력 방식
    // 장점 : 간단하다.
    // 단점 : Busy-wait이 발생할 수 밖에 없고(배터리 사용 증가), 인풋랙이 있을 수 있다.

    // InputSystem : 유니티의 새로운 입력 방식
    // Event-driven 방식 적용

    PlayerInputActions inputActions;

    /// <summary>
    /// 시작 함수 1 (실행 순서 1번)
    /// </summary>
    // 이 스크립트가 포함된 게임 오브젝트가 생성 완료되면 호출된다.
    private void Awake()
    {
        inputActions = new PlayerInputActions(); // 인풋 액션 생성
    }

    /// <summary>
    /// 시작 함수 2 (실행 순서 2번)
    /// </summary>
    // 이 스크립트가 포함된 게임 오브젝트가 활성화되면 호출된다.
    private void OnEnable()
    {
        inputActions.Player.Enable(); // 활성화될 때 Player 액션맵을 활성화
        inputActions.Player.Fire.performed += OnFire; // Player 액션맵의 Fire 액션에 OnFire 함수를 연결 (눌렀을 때만 연결된 함수 실행)
        inputActions.Player.Fire.canceled += OnFire; // Player 액션맵의 Fire 액션에 OnFire 함수를 연결 (땠을 때만 연결된 함수 실행)
        //inputActions.Player.Fire.started // 콘솔 게임에서 조이스틱 관련 부분

        inputActions.Player.Fire.performed += OnBoost;
        inputActions.Player.Fire.canceled += OnBoost;
    }

    // 이 스크립트가 포함된 게임 오브젝트가 비활성화되면 호출된다.
    private void OnDisable()
    {
        inputActions.Player.Fire.canceled -= OnBoost;
        inputActions.Player.Fire.performed -= OnBoost;

        inputActions.Player.Fire.canceled -= OnFire; // Player 액션맵의 Fire 액션에 OnFire 함수를 연결 (땠을 때만 연결된 함수 실행)
        inputActions.Player.Fire.performed -= OnFire; // Player 액션맵의 Fire 액션에서 OnFire 함수를 연결 해제
        inputActions.Player.Disable(); // Player 액션맵을 비활성화
    }

    /// <summary>
    /// Fire 액션이 발동했을 때, 실행 시킬 함수
    /// </summary>
    /// <param name="context">입력 관련 정보가 들어있는 구조체 변수</param>
    public void OnFire(InputAction.CallbackContext context)
    {
        if(context.performed) // 지금 입력이 눌렀다.
        {
            Debug.Log("OnFire : 눌러짐");
        }

        if(context.canceled) // 지금 입력이 떨어졌다.
        {
            Debug.Log("OnFire : 떨어짐");
        }
    }

    /// 실습_231229
    /// Boost 액션과 OnBoost 함수 연결하기 (Shift-Key)
    /// Boost 액션으로 눌러졌는지 떨어졌는지 출력하기
    public void OnBoost(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("OnBoost : 눌러짐");
        }

        if (context.canceled)
        {
            Debug.Log("OnBoost : 떨어짐");
        }
    }

    /// <summary>
    /// 시작 함수 3 (실행 순서 3번)
    /// </summary>
    // 이 스크립트가 포함된 게임 오브젝트의 첫번째 Update 함수가 실행되기 직전에 호출된다.
    private void Start()
    {
        
    }

    private void Update()
    {
        /// 인풋매니저 방식
        //if(Input.GetKeyDown(KeyCode.A))
        //{
        //    Debug.Log("A키가 눌러졌습니다.");
        //}

        //if (Input.GetKeyUp(KeyCode.A))
        //{
        //    Debug.Log("A키가 떨어졌습니다.");
        //}
    }

    //public void OnFire()
    //{
    //    Debug.Log("OnFire");
    //}
}
