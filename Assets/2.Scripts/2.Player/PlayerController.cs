using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    Rigidbody2D rb;  
    PlayerControls controls; // Input Actions Asset에서 생성한 클래스

    Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new PlayerControls();

        // Move 액션 이벤트 연결
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void FixedUpdate()
    {
        // 이동
        rb.velocity = moveInput.normalized * moveSpeed;
    }

    public void IncreaseMoveSpeed(float value)
    {
        moveSpeed += value;
    }
}
