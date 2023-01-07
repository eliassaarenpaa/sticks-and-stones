using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDefender
{
    [SerializeField] private int initialHealth;
    private int _currentHealth;

    public int Health { get => _currentHealth; set => _currentHealth = value; }

    public void Defend(IAttacker attacker, Vector2 attackDir, Vector2 contactPoint)
    {
        Health -= attacker.Damage;

        //PlayHitEffects(contactPoint);
        //TakeKnockBack(attacker);

        if (Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }


}

