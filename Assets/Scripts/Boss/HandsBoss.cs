using UnityEngine;

namespace Boss
{
    public class HandsBoss : MonoBehaviour
    {
        [SerializeField] private BossLife _bossLife;
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("BulletBlue") || collision.collider.CompareTag("BulletRed"))
            {
                _bossLife.DoFeedback();
                if (!BossSharedLife.Instance.IsAlive) return;
                BossSharedLife.Instance.TakeDamage(1);
                if (BossSharedLife.Instance.IsDead()) _bossLife.Die();
            }
        }
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("ChargedBulletBlue") || other.CompareTag("ChargedBulletRed"))
            {
                _bossLife.DoFeedback();
                if (!BossSharedLife.Instance.IsAlive) return;
                BossSharedLife.Instance.TakeDamage(3);
                if (BossSharedLife.Instance.IsDead()) _bossLife.Die();
            }
        }
    }
}
