using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    [SerializeField] MapBounds mapBounds;
    [SerializeField] float playerRadius = 0.35f;

    Rigidbody2D rb;  
    PlayerControls controls; // Input Actions Asset에서 생성한 클래스

    Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new PlayerControls();

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += _ => moveInput = Vector2.zero;
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
        Vector2 direction = moveInput.normalized;

        Vector2 nextPosition = rb.position + direction * moveSpeed * Time.fixedDeltaTime;

        if (mapBounds != null)
        {
            float minX = mapBounds.Min.x + playerRadius;
            float maxX = mapBounds.Max.x - playerRadius;

            float minY = mapBounds.Min.y + playerRadius;
            float maxY = mapBounds.Max.y - playerRadius;

            nextPosition.x = Mathf.Clamp(nextPosition.x, minX, maxX);
            nextPosition.y = Mathf.Clamp(nextPosition.y, minY, maxY);
        }

        rb.MovePosition(nextPosition);
    }

    public void IncreaseMoveSpeed(float value)
    {
        moveSpeed += value;
    }
}
