using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;   
    [HideInInspector] public Transform player;

    Rigidbody2D rb;

    Vector2 moveDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (player == null) return;

        // 플레이어 위치까지 방향 계산
        moveDirection = (player.position - transform.position).normalized;
        // 이동
        rb.velocity = moveDirection * moveSpeed;
    }

    private void OnDisable()
    {
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
    }
}
