using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // InputManager : ������ ����Ƽ �Է� ���
    // ���� : �����ϴ�.
    // ���� : Busy-wait�� �߻��� �� �ۿ� ����(���͸� ��� ����), ��ǲ���� ���� �� �ִ�.

    // InputSystem : ����Ƽ�� ���ο� �Է� ���
    // Event-driven ��� ����

    PlayerInputActions inputActions;

    /// <summary>
    /// ���� �Լ� 1 (���� ���� 1��)
    /// </summary>
    // �� ��ũ��Ʈ�� ���Ե� ���� ������Ʈ�� ���� �Ϸ�Ǹ� ȣ��ȴ�.
    private void Awake()
    {
        inputActions = new PlayerInputActions(); // ��ǲ �׼� ����
    }

    /// <summary>
    /// ���� �Լ� 2 (���� ���� 2��)
    /// </summary>
    // �� ��ũ��Ʈ�� ���Ե� ���� ������Ʈ�� Ȱ��ȭ�Ǹ� ȣ��ȴ�.
    private void OnEnable()
    {
        inputActions.Player.Enable(); // Ȱ��ȭ�� �� Player �׼Ǹ��� Ȱ��ȭ
        inputActions.Player.Fire.performed += OnFire; // Player �׼Ǹ��� Fire �׼ǿ� OnFire �Լ��� ���� (������ ���� ����� �Լ� ����)
        inputActions.Player.Fire.canceled += OnFire; // Player �׼Ǹ��� Fire �׼ǿ� OnFire �Լ��� ���� (���� ���� ����� �Լ� ����)
        //inputActions.Player.Fire.started // �ܼ� ���ӿ��� ���̽�ƽ ���� �κ�
    }

    // �� ��ũ��Ʈ�� ���Ե� ���� ������Ʈ�� ��Ȱ��ȭ�Ǹ� ȣ��ȴ�.
    private void OnDisable()
    {
        inputActions.Player.Fire.canceled -= OnFire; // Player �׼Ǹ��� Fire �׼ǿ� OnFire �Լ��� ���� (���� ���� ����� �Լ� ����)
        inputActions.Player.Fire.performed -= OnFire; // Player �׼Ǹ��� Fire �׼ǿ��� OnFire �Լ��� ���� ����
        inputActions.Player.Disable(); // Player �׼Ǹ��� ��Ȱ��ȭ
    }

    /// <summary>
    /// Fire �׼��� �ߵ����� ��, ���� ��ų �Լ�
    /// </summary>
    /// <param name="context">�Է� ���� ������ ����ִ� ����ü ����</param>
    public void OnFire(InputAction.CallbackContext context)
    {
        if(context.performed) // ���� �Է��� ������.
        {
            Debug.Log("OnFire : ������");
        }

        if(context.canceled) // ���� �Է��� ��������.
        {
            Debug.Log("OnFire : ������");
        }
    }

    /// <summary>
    /// ���� �Լ� 3 (���� ���� 3��)
    /// </summary>
    // �� ��ũ��Ʈ�� ���Ե� ���� ������Ʈ�� ù��° Update �Լ��� ����Ǳ� ������ ȣ��ȴ�.
    private void Start()
    {
        
    }

    private void Update()
    {
        /// ��ǲ�Ŵ��� ���
        //if(Input.GetKeyDown(KeyCode.A))
        //{
        //    Debug.Log("AŰ�� ���������ϴ�.");
        //}

        //if (Input.GetKeyUp(KeyCode.A))
        //{
        //    Debug.Log("AŰ�� ���������ϴ�.");
        //}
    }

    //public void OnFire()
    //{
    //    Debug.Log("OnFire");
    //}
}
