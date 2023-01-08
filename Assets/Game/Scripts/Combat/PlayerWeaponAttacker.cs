using UnityEngine;

public class PlayerWeaponAttacker : MonoBehaviour, IAttacker
{
    [SerializeField] private Transform player;
    [SerializeField] private PlayerWeaponSystem playerWeaponSystem;
    [SerializeField] private LayerMask enemyLayer;

    public int Damage { get => playerWeaponSystem.GetWeaponDamage(); set { } }

    public Transform Transform => player.transform;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (1 << collision.gameObject.layer == enemyLayer)
        {
            IDefender defender = collision.gameObject.GetComponent<IDefender>();

            if (defender != null)
            {
                var attackDir = (transform.position - player.transform.position).normalized;
                var contact  = collision.contacts[0];
                var combat = new Combat(this, defender, attackDir, contact.point);
                combat.Fight();
            }
        }
    }
}
