using Player;
using UnityEngine;

[CreateAssetMenu(menuName =  "Power-Up/HealthBuff")]
public class HealthBuff : PickedUp
{
    public float amount;

    public override void Apply()
    {
        SharedPlayersLife.Instance._currentHealth += amount;
        SharedPlayersLife.Instance._healthBar.fillAmount = SharedPlayersLife.Instance._currentHealth / SharedPlayersLife.Instance._maxHealth;
    }
}
