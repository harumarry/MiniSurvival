using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float invincibleTime = 0.5f;

    public HUDUI hudUI;

    [SerializeField] int maxHp = 100;
    int currentHp;

    bool isInvincible = false;

    private void Start()
    {
        currentHp = maxHp;
        hudUI.UpdateHp(currentHp, maxHp);
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        currentHp -= damage;
        hudUI.UpdateHp(currentHp, maxHp);

        if (currentHp <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibleCoroutine());
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
}
