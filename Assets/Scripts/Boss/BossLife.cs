using System.Collections;
using __Workspaces.Baptiste.scripts;
using Boss;
using UnityEngine;
using UnityEngine.Audio;
public class BossLife : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private float _flashTime = 0.3f;
    [SerializeField] private AnimationCurve _flashAnimationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] private BossHealthUI _healthUI;
    [SerializeField] private GameObject _victoryUI;
    [SerializeField] private GameObject _transitionUI;
    [SerializeField] private AudioClip _win;
    [SerializeField] private float _currentIntensity;
    
    public Material Mat;
    public ParticleSystem DeathEffect;
    public ParticleSystem FinaldeathExplosion; 
    public int ExplosionCount = 8;
    public float DelayBetweenExplosions = 0.2f;
    public Animator Animator;
    public BulletSpawner Shooter;
    
    private bool _checkIfDead;
    private Collider2D _collider;
    private float _timerIntensity;

    private void Start()
    {
        Shooter.enabled = false;
        Animator.SetBool("IsDead", false);
        Animator.SetBool("IsEntering", false);
        //mat = GetComponent<SpriteRenderer>().material;
        _collider = GetComponent<PolygonCollider2D>();
    }

    private void Update()
    {
        if (BossSharedLife.Instance.IsDead() && !_checkIfDead) Die();
        if (_timerIntensity > 0f)
        {
            _timerIntensity -= Time.deltaTime;
            var t = _timerIntensity / _flashTime;
            Mat.SetFloat("_Hit_intensity", _flashAnimationCurve.Evaluate(t));

            if (_timerIntensity <= 0f)
            {
                _timerIntensity = 0;
                Mat.SetFloat("_Hit_intensity", 0);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("BulletBlue") || collision.collider.CompareTag("BulletRed"))
        {
            //DoFeedback();
            if (!BossSharedLife.Instance.IsAlive) return;
            BossSharedLife.Instance.TakeDamage(1);
            if (BossSharedLife.Instance.IsDead()) Die();
        }

        // if (collision.collider.CompareTag("ChargedBulletBlue") || collision.collider.CompareTag("ChargedBulletRed"))
        // {
        //     //DoFeedback();
        //     if (!BossSharedLife.Instance.IsAlive) return;
        //     BossSharedLife.Instance.TakeDamage(5);
        //     if (BossSharedLife.Instance.IsDead()) Die();
        // }
    }
    
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.GetComponent<Collider>().CompareTag("BulletBlue") || other.GetComponent<Collider>().CompareTag("BulletRed"))
    //     {
    //         //DoFeedback();
    //         if (!BossSharedLife.Instance.IsAlive) return;
    //         BossSharedLife.Instance.TakeDamage(15);
    //         if (BossSharedLife.Instance.IsDead()) Die();
    //     }
    //     
    // }

    public void StartBoss()
    {
        _healthUI.Show();
        Shooter.enabled = true;
        Animator.SetBool("IsEntering", true);
    }

    public void DoFeedback()
    {
        _timerIntensity = _flashTime;
    }

    public void Die()
    {
        _healthUI.Hide();
        Shooter.StopFire();
        StartCoroutine(ExplosionSequence());
        _checkIfDead = true;
    }

    private Vector2 GetRandomPointInBounds(Bounds bounds)
    {
        var x = Random.Range(bounds.min.x, bounds.max.x);
        var y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector2(x, y);
    }

    private IEnumerator ExplosionSequence()
    {
        for (var i = 0; i < ExplosionCount; i++)
        {
            var randomPos = GetRandomPointInBounds(_collider.bounds);

            var clone = Instantiate(DeathEffect, randomPos, Quaternion.identity);
            clone.Play();
            Destroy(clone.gameObject, 3);

            yield return new WaitForSeconds(DelayBetweenExplosions);
        }

        Animator.SetBool("IsDead", true);
        yield return new WaitForSeconds(3f);
        Vector2 center = _collider.bounds.center;

        FinaldeathExplosion.Play();
        yield return new WaitForSeconds(7f);
        _victoryUI.SetActive(true);
        SoundFXManager.Instance.PlaySoundFXClip(_win, SoundGroups.Sfx);
        yield return new WaitForSeconds(3f);
        _transitionUI.SetActive(true);
    }
}