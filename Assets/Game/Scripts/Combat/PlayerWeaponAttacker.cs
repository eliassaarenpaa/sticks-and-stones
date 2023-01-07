using UnityEngine;

public class PlayerWeaponAttacker : MonoBehaviour, IAttacker
{
    [SerializeField] private Transform player;
    [SerializeField] private PlayerWeaponSystem playerWeaponSystem;
    [SerializeField] private LayerMask enemyLayer;

    public int Damage { get => playerWeaponSystem.GetWeaponDamage(); set { } }

    public Transform Transform => player.transform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("HIt");

        if(1<<collision.gameObject.layer == enemyLayer)
        {
            IDefender defender = collision.GetComponent<IDefender>();
            
            if (defender != null)
            {
                var combat = new Combat(this, defender);
                combat.Fight();
            }
        }
    }
}
