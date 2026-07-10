using UnityEngine;
using UnityEngine.UI;

public class PlayerHPUI : MonoBehaviour
{
    [SerializeField] Image hpBarFill;

    public void UpdateHp(float currentHp, float maxHp)
    {
        hpBarFill.fillAmount = (float)currentHp / maxHp;
    }
}
