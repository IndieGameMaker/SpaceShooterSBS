using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // InputAction 
    private InputSystem_Actions _inputActions;
    // MoveAction
    private InputAction _moveAction;
    // Mouse Delta Action
    private InputAction _mouseDeltaAction;
    
    // 이동 방향 벡터
    private Vector3 _moveDir;
    
    // 이동 속도
    [SerializeField] private float _moveSpeed = 6.0f;
    // 회전 속도
    [SerializeField] private float _turnSpeed = 50.0f;

    private float _maxHp = 100f;
    private float _currHp = 100f;
    
    // 컴포넌트
    private Animator _animator;
    // 애니메이터 해시 추출
    private readonly int hashForward = Animator.StringToHash("Forward");
    private readonly int hashStrafe = Animator.StringToHash("Strafe");

    // 이벤트 정의
    public static event Action OnPlayerDie; 
    
    #region 유니티 생명주기
    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
        _moveAction = _inputActions.Player.Move;
        _mouseDeltaAction = _inputActions.Player.Look;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        
        // 마우스 락처리
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }

    private void OnEnable()
    {
        _moveAction.performed += OnMove;
        _moveAction.canceled += OnMove;
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _moveAction.performed -= OnMove;
        _moveAction.canceled -= OnMove;
        _inputActions.Disable();
    }

    private void Update()
    {
        Movement();
        PlayAnimation();
    }
    #endregion

    private void PlayAnimation()
    {
        _animator.SetFloat(hashForward, _moveDir.normalized.z);
        _animator.SetFloat(hashStrafe, _moveDir.normalized.x); 
    }

    private void Movement()
    {
        // (이동 방향 * 이동 속도 * 시간보정
        transform.Translate(_moveDir * (_moveSpeed * Time.deltaTime));
        
        // 회전 로직
        var mouseDir = _mouseDeltaAction.ReadValue<Vector2>().x;
        transform.Rotate(0, mouseDir * _turnSpeed * Time.deltaTime, 0);
        // Debug.Log($"Mouse Delta X : {mouseDir}");
    }

    
    private void OnMove(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
        {
            var dir = ctx.ReadValue<Vector2>();         // (x, y)
            _moveDir = new Vector3(dir.x, 0, dir.y);    // (x, y, z)
        }
        else if (ctx.phase == InputActionPhase.Canceled)
        {
            _moveDir = Vector3.zero; // (0, 0, 0)
        }
        // Debug.Log($"Move {_moveDir}");
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("PUNCH"))
        {
            _currHp -= 10f;
            if (_currHp <= 0f)
            {
                OnPlayerDie?.Invoke();
                // PlayerDie();
            }
        }
    }

    private void PlayerDie()
    {
        // 스테이지에 있는 모든 몬스터를 추출
        // GameObject[] monsters = GameObject.FindGameObjectsWithTag("MONSTER");
        //
        // foreach (GameObject monster in monsters)
        // {
        //     monster.SendMessage("YouWin", SendMessageOptions.DontRequireReceiver);   
        // }
    }
}
