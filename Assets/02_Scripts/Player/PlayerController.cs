using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // InputAction 
    private InputSystem_Actions _inputActions;
    // MoveAction
    private InputAction _moveAction;

    #region 유니티 생명주기
    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
        _moveAction = _inputActions.Player.Move;
    }

    private void OnEnable()
    {
        _moveAction.performed += OnMove;
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }
    #endregion
    
    private void OnMove(InputAction.CallbackContext ctx)
    {
        Debug.Log($"Move {ctx.ReadValue<Vector2>()}");
    }
    
}
