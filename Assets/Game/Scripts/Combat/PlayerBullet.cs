using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour, IAttacker
{
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private LayerMask wallMask;

    private Vector3 _moveDirection;

    [SerializeField] private float timeUntilDestruct;
    [SerializeField] private float moveSpeed;

    public Transform Transform => throw new System.NotImplementedException();

    public int Damage { get => 1; set { } }

    public void SetDireciton(Vector2 moveDirection)
    {
        _moveDirection = moveDirection.normalized;


        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnEnable()
    {
        Invoke(nameof(Destroy), timeUntilDestruct);
    }

    private void Update()
    {
        if (_moveDirection == Vector3.zero) return;

        transform.position += _moveDirection * moveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (1 << collision.gameObject.layer == enemyMask)
        {
            var defender = collision.gameObject.GetComponent<IDefender>();
            var dirToEnemy = (collision.transform.position - transform.position).normalized;
            var combat = new Combat(this, defender, dirToEnemy, transform.position);
            combat.Fight();
            Destroy();
        }
        else if (1 << collision.gameObject.layer == wallMask)
        {
            Destroy();
        }

    }
    private void Destroy()
    {
        if (gameObject)
        {
            Destroy(gameObject);
        }
    }
}
