using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] GameObject expGemPrefab;
    [SerializeField] GameObject deathEffectPrefab;
    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] Color hitColor = new Color(1f, 0.85f, 0.35f, 1f);
    [SerializeField] float maxHp = 3f;
    [SerializeField] float hitFlashTime = 0.08f;

    Color originalColor;
    float currentHp;
    bool isDead;

    private void Awake()
    {
        originalColor = spriteRenderer.color;
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
        else
        {
            StartCoroutine(HitFlashRoutine());
        }
    }

    public void ResetHealth()
    {
        currentHp = maxHp;
        isDead = false;
        spriteRenderer.color = originalColor;
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;

        GameManager.instance.AddKillCount();

        Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        Instantiate(expGemPrefab, transform.position, Quaternion.identity);

        PoolManager.instance.ReturnEnemy(gameObject);
    }
    
    IEnumerator HitFlashRoutine()
    {
        spriteRenderer.color = hitColor;

        yield return new WaitForSeconds(hitFlashTime);

        spriteRenderer.color = originalColor;
    }
}
