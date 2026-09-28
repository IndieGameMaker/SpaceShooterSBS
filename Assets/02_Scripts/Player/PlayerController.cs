using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // InputAction 
    private InputSystem_Actions _inputActions;
    // MoveAction
    private InputAction _moveAction;
    
    // 이동 방향 벡터
    private Vector3 _moveDir;

    #region 유니티 생명주기
    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
        _moveAction = _inputActions.Player.Move;
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
    #endregion
    
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
        Debug.Log($"Move {_moveDir}");
    }
}
