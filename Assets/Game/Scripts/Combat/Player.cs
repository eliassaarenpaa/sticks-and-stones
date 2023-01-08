using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IDefender
{
    [SerializeField] private AudioSource deathSource;
    [SerializeField] private AudioSource hitSource;

    [SerializeField] private Animator playerAnimator;
    [SerializeField] private PlayerEventChannel playerEventChannel;
    [SerializeField] private PlayerHealthEventChannel playerHealthEventChannel;
    [SerializeField] private GameStateEventChannel gameStateEventChannel;
    [SerializeField] private LevelEventChannel levelEventChannel;

    private int _currentHealth;

    public int Health { get => _currentHealth; set {
            _currentHealth = value;
            playerHealthEventChannel.UpdateHealth(_currentHealth);
        } }

    public void Defend(IAttacker attacker, Vector2 attackDir, Vector2 contactPoint)
    {
        Health -= 1;

        hitSource.Play();
        //PlayHitEffects(contactPoint);
        //TakeKnockBack(attacker);

        if (Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerWeaponController>().enabled = false;
        playerAnimator.Play("Death");
        deathSource.Play();
        StartCoroutine(DestroyPlayer());
        Time.timeScale = 0;
    }

    private IEnumerator DestroyPlayer()
    {
        yield return new WaitForSecondsRealtime(1);
        Time.timeScale = 1;
        playerEventChannel.DestroyPlayer();
        levelEventChannel.StopLevel();
        gameStateEventChannel.ChangeState(3);
    }


}

