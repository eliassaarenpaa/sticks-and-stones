using Assets.Game.Scripts.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowItem : WeaponItem
{
    [SerializeField] private PlayerBullet bulletPrefab;
    [SerializeField] private ParticleSystem shootParticle;
    public override void OnAttack(Vector2 attackPosition, Vector2 attackDirection)
    {
        base.OnAttack(attackPosition, attackDirection);

        var bullet = Instantiate(bulletPrefab);

        if (bullet)
        {
            bullet.transform.position = attackPosition;
            bullet.SetDireciton(attackDirection);
            bullet.gameObject.SetActive(true);
            //shootParticle?.Play();
        }

    }
}
