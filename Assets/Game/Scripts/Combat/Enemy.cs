using DG.Tweening;
using UnityEngine;

public class Enemy : MonoBehaviour, IDefender
{
    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private ParticleSystem hitParticle;
    [SerializeField] private float knockbackMoveForce;
    [SerializeField] private float knockbackJumpForce;
    [SerializeField] private float knockbackDuration;
    [SerializeField] private int initialHealth;
    [SerializeField] protected Collider2D col;
    [SerializeField] private LayerMask wallLayer;

    private int _currentHealth;

    public int Health { get => _currentHealth; set => _currentHealth = value; }


    protected virtual void Awake()
    {
        Health = initialHealth;
    }

    protected virtual void Update()
    {
    }

    public virtual void Defend(IAttacker attacker, Vector2 attackDir, Vector2 contactPoint)
    {
        Health -= attacker.Damage;

        PlayHitEffects(contactPoint);
        TakeKnockBack(attackDir);

        if (Health <= 0)
        {
            Die();
        }
    }

    private void TakeKnockBack(Vector3 attackDir)
    {
        transform.DOKill();
        transform.DOJump(transform.position + attackDir * knockbackMoveForce, knockbackJumpForce, 1, knockbackDuration);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(1<<collision.gameObject.layer == wallLayer)
        {
            transform.DOKill();
        }
    }

    private void PlayHitEffects(Vector3 contactPoint)
    {
        hitParticle.Stop();
        hitParticle.transform.position = contactPoint;
        hitParticle.Play();
        enemyAnimator.Play("Hit");
    }

    protected virtual void Die()
    {
        Destroy(col);
        enemyAnimator.Play("Death");
        Destroy(gameObject, 1);
    }
}
