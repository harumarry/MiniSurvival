using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHp = 3f;
    [SerializeField] GameObject expGemPrefab;

    float currentHp;
    bool isDead;

    private void Awake()
    {
        ResetHealth();
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHp -= damage;

        if (currentHp <= 0)
        {
            Die();
        }
    }

    public void ResetHealth()
    {
        currentHp = maxHp;
        isDead = false;
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;
        GameManager.instance.AddKiillCount();
        PoolManager.instance.ReturnEnemy(gameObject);

        Instantiate(expGemPrefab, transform.position, Quaternion.identity);
    }
    
}
