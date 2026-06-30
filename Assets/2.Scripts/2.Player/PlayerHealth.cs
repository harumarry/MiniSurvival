using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] HUDUI hudUI;

    [SerializeField] Color hitColor = new Color(1f, 0.35f, 0.35f, 1f);
    [SerializeField] float invincibleTime = 0.5f;
    [SerializeField] float hitFlashTime = 0.1f;
    [SerializeField] int maxHp = 100;

    Color originalColor;
    int currentHp;
    bool isInvincible = false;

    private void Start()
    {
        currentHp = maxHp;
        originalColor = spriteRenderer.color;
        hudUI.UpdateHp(currentHp, maxHp);
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        currentHp -= damage;
        currentHp = Mathf.Max(currentHp, 0);
        hudUI.UpdateHp(currentHp, maxHp);

        if (currentHp <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibleCoroutine());
            StartCoroutine(HitFlashRoutine());
        }
    }

    void Die()
    {
        GameManager.instance.GameOver();
    }

    IEnumerator InvincibleCoroutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }

    IEnumerator HitFlashRoutine()
    {
        spriteRenderer.color = hitColor;
        yield return new WaitForSeconds(hitFlashTime);
        spriteRenderer.color = originalColor;
    }
}
