using Boss;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BossLife : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _flashTime = 0.3f;
    [SerializeField] private AnimationCurve _flashAnimationCurve = AnimationCurve.EaseInOut(0,0,1,1);
    private Collider2D _collider;
    public Material mat;
    private float currentIntensity = 0f;
    private float _timerIntensity;
        
    private void Start()
    {
        //mat = GetComponent<SpriteRenderer>().material;
        _collider = GetComponent<Collider2D>();
    }
    private void Update()
    {
        if (_timerIntensity > 0f) {
            _timerIntensity -= Time.deltaTime;
            float t = _timerIntensity/_flashTime;
            mat.SetFloat("_Hit_intensity", _flashAnimationCurve.Evaluate(t));

            if (_timerIntensity <= 0f) {
                _timerIntensity = 0;
                mat.SetFloat("_Hit_intensity", 0);
            }
        }
    }
    public void DoFeedback()
    {
        _timerIntensity = _flashTime;
    }
    public void Die()
    {
        SceneManager.LoadScene("Victory");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("BulletBlue") || collision.collider.CompareTag("BulletRed"))
        {
            //DoFeedback();
            BossSharedLife.Instance.TakeDamage(1);
            if (BossSharedLife.Instance.IsDead())
            {
                Die();
            }
        }
    }
}
