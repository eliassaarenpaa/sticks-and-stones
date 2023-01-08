using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField] private PlayerWeaponSystem playerWeaponSystem;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Animator weaponAnimator;
    [SerializeField] private Transform weaponParent;
    [SerializeField] private SpriteRenderer weapon;

    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5.23f;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(weaponParent.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        weaponParent.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        var activeWeapon = playerWeaponSystem.GetActiveWeapon();

        if (activeWeapon)
        {
            if (activeWeapon.useFlip)
            {
                weapon.flipY = weapon.transform.position.x <= transform.position.x;
            }

            weapon.sortingOrder = weapon.transform.position.y > transform.position.y ? -1 : 1;

            if (Input.GetMouseButtonDown(0))
            {
                weaponAnimator.Play("Attack");
                audioSource.Play();
                Vector2 direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));
                playerWeaponSystem.GetActiveWeapon()?.OnAttack(weaponParent.transform.position, direction.normalized);
            }
        }

    }
}
