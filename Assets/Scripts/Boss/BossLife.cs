using Boss;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using UnityEngine.Tilemaps;

public class BossLife : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _flashTime = 0.3f;
    [SerializeField] private AnimationCurve _flashAnimationCurve = AnimationCurve.EaseInOut(0,0,1,1);
    [SerializeField] private BossHealthUI healthUI;
    [SerializeField] private GameObject VictoryUI;
    [SerializeField] private GameObject TransitionUI;
    private Collider2D _collider;
    public Material mat;
    private float currentIntensity = 0f;
    private float _timerIntensity;
    public ParticleSystem _deathEffect;
    public ParticleSystem _finaldeathExplosion;
    public int explosionCount = 8;
    public float delayBetweenExplosions = 0.2f;
    public Animator _animator;
    public BulletSpawner _shooter;
    private bool _checkIfDead = false;
        
    private void Start()
    {
        _shooter.enabled = false;
        _animator.SetBool("IsDead", false);
        _animator.SetBool("IsEntering", false);
        //mat = GetComponent<SpriteRenderer>().material;
        _collider = GetComponent<PolygonCollider2D>();
    }

    public void StartBoss()
    {
        healthUI.Show();
        _shooter.enabled = true;
        _animator.SetBool("IsEntering", true);
    }
    private void Update()
    {
        if (BossSharedLife.Instance.IsDead() &&  !_checkIfDead)
        {
             Die();
             
        }
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
        healthUI.Hide();
        _shooter.StopFire();
        StartCoroutine(ExplosionSequence());
        _checkIfDead = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("BulletBlue") || collision.collider.CompareTag("BulletRed"))
        {
            //DoFeedback();
            if(!BossSharedLife.Instance.IsAlive)return;
            BossSharedLife.Instance.TakeDamage(1);
            if (BossSharedLife.Instance.IsDead())
            {
                Die();
            }
        }
        if (collision.collider.CompareTag("ChargedBulletBlue") || collision.collider.CompareTag("ChargedBulletRed"))
        {
            //DoFeedback();
            if(!BossSharedLife.Instance.IsAlive)return;
            BossSharedLife.Instance.TakeDamage(5);
            if (BossSharedLife.Instance.IsDead())
            {
                Die();
            }
        }
    }
    Vector2 GetRandomPointInBounds(Bounds bounds)
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector2(x, y);
    }
    IEnumerator ExplosionSequence()
    {
        for (int i = 0; i < explosionCount; i++)
        {
            Vector2 randomPos = GetRandomPointInBounds(_collider.bounds);

            ParticleSystem clone = Instantiate(_deathEffect, randomPos, Quaternion.identity);
            clone.Play();
            Destroy(clone.gameObject, 3);
            
            yield return new WaitForSeconds(delayBetweenExplosions);
        }
        _animator.SetBool("IsDead", true);
        yield return new WaitForSeconds(3f);
        Vector2 center = _collider.bounds.center;

        _finaldeathExplosion.Play();
        yield return new WaitForSeconds(7f);
        VictoryUI.SetActive(true);
        yield return new WaitForSeconds(3f);
        TransitionUI.SetActive(true);
    }
}
