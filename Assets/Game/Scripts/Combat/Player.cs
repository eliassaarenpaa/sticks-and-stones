using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDefender
{
    [SerializeField] private PlayerHealthEventChannel playerHealthEventChannel;
    [SerializeField] private GameStateEventChannel gameStateEventChannel;

    [SerializeField] private int initialHealth;
    private int _currentHealth;

    private void OnEnable()
    {
        gameStateEventChannel.onGameStateChanged.AddListener(OnGameStateChanged);
    }

    private void OnDisable()
    {
        gameStateEventChannel.onGameStateChanged.RemoveListener(OnGameStateChanged);
    }

    private void OnGameStateChanged(GameState state)
    {
        if(state == GameState.Game)
        {
            Health = 3;
        }
    }

    public int Health { get => _currentHealth; set {
            _currentHealth = value;
            playerHealthEventChannel.UpdateHealth(_currentHealth);
        } }

    public void Defend(IAttacker attacker, Vector2 attackDir, Vector2 contactPoint)
    {
        Health -= 1;

        //PlayHitEffects(contactPoint);
        //TakeKnockBack(attacker);

        if (Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //Destroy(gameObject);
    }


}

