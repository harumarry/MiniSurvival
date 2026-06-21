using UnityEngine;

public class ExpGem : MonoBehaviour
{
    [SerializeField] int expAmount = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerLevel playerLevel = collision.GetComponent<PlayerLevel>();
            playerLevel.AddExp(expAmount);
            Destroy(gameObject);
        }
    }
}
