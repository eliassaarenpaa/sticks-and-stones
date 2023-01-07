using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IDefender
{
    [SerializeField] private float knockbackMoveForce;
    [SerializeField] private float knockbackJumpForce;
    [SerializeField] private float knockbackDuration;

    [SerializeField] private UnityEvent onHit;
    [SerializeField] private int initialHealth;
    private int _currentHealth;

    public int Health { get => _currentHealth; set => _currentHealth = value; }

    private void Awake()
    {
        Health = initialHealth;
    }

    public virtual void Defend(IAttacker attacker)
    {
        Health -= attacker.Damage;

        TakeKnockBack(attacker);

        onHit?.Invoke();

        if (Health <= 0)
        {
            Die();
        }
    }

    private void TakeKnockBack(IAttacker attacker)
    {
        var dir = (transform.position - attacker.Transform.position).normalized;
        //_rigidbody2D.AddForce(dir * 5f, ForceMode2D.Impulse);
        transform.DOKill();
        transform.DOJump(transform.position + dir * knockbackMoveForce, knockbackJumpForce, 1, knockbackDuration);
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
