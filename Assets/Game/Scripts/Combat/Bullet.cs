using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    private Vector3 _moveDirection;

    [SerializeField] private float timeUntilDestruct;
    [SerializeField] private float moveSpeed;

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
        if(_moveDirection == Vector3.zero) return;

        transform.position += _moveDirection * moveSpeed * Time.deltaTime;
    }

    private void Destroy()
    {
        if (gameObject)
        {
            Destroy(gameObject);
        }
    }

}
