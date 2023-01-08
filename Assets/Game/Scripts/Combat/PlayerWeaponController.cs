using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
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

        weapon.flipY = weapon.transform.position.x <= transform.position.x;
        //var directionToMouse = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle), Mathf.Cos(Mathf.Deg2Rad * angle)).normalized;

        weapon.sortingOrder = weapon.transform.position.y > transform.position.y ? -1 : 1;

        //weapon.transform.position = transform.position + -directionToMouse * 2f;

        if (Input.GetMouseButtonDown(0))
        {
            weaponAnimator.Play("Attack");
            audioSource.Play();
        }

    }
}
