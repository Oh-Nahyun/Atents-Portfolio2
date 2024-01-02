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
    /// 마지막으로 입력된 방향을 기록하는 변수
    /// </summary>
    Vector3 inputDir = Vector3.zero;

    /// <summary>
    /// 플레이어의 이동 속도
    /// public 멤버 변수는 인스팩터 창에서 확인이 가능하다.
    /// </summary>
    // [Range(0.0f, 1.0f)] // 스크롤 바를 이용해 값을 조절할 수 있다.
    public float moveSpeed = 0.0001f;
    
    /*
    [SerializeField] // public이 아닌 경우에도 인스팩터 창에서 확인이 가능해진. (권장하지 않음(성능상 문제 있음))
    float test = 1.0f;
    */

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

        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
    }

    // 이 스크립트가 포함된 게임 오브젝트가 비활성화되면 호출된다.
    private void OnDisable()
    {
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Move.performed -= OnMove;

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
        if (context.performed) // 지금 입력이 눌렀다.
        {
            Debug.Log("OnFire : 눌러짐");
        }

        if (context.canceled) // 지금 입력이 떨어졌다.
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

    public void OnMove(InputAction.CallbackContext context)
    {
        // scope : 변수나 함수의 사용 가능한 범위
        inputDir = context.ReadValue<Vector2>();
        //Debug.Log($"OnMove : ({dir})");

        //this.transform.position = new Vector3(1, 0, 0); // 이 게임 오브젝트의 위치를 (1,0,0)으로 보내라는 의미

        //transform.position += new Vector3(1, 0, 0); // 방법 1 // 이 게임 오브젝트의 위치를 현재 위치에서 (1,0,0)만큼 움직여라
        //transform.position += Vector3.right; //방법 2 // 이 게임 오브젝트의 위치를 현재 위치에서 (1,0,0)만큼 움직여라
        
        //transform.position += (Vector3)dir; // 이 게임 오브젝트의 위치를 현재 위치에서 inputDir 방향으로 움직여/
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

        /// 실습_240102
        /// 누르고 있으면 [계속] 그쪽 방향으로 이동하게 만들어보기
        //transform.position += inputDir; // 방법 1

        // Time.deltaTime : 프레임 간의 시간 간격 (가변적)
        transform.Translate(Time.deltaTime * moveSpeed * inputDir); // 방법 2 // 1초당 moveSpeed만큼의 속도로, inputDir 방향으로 움직여라
    }

    //public void OnFire()
    //{
    //    Debug.Log("OnFire");
    //}
}