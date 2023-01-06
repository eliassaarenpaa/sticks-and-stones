using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float moveSpeed = 5f; 

    private Rigidbody _rigidbody;
    private Camera _camera;
    private Vector3 _movementInput;

    private bool IsMoving => _movementInput != Vector3.zero;

    private bool _movingLeft;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _camera = Camera.main;
    }

    private void Update()
    {
        Vector3 cameraUpDirecition = _camera.transform.up;
        Vector3 cameraRightDirection = _camera.transform.right;
        _movementInput = cameraUpDirecition * Input.GetAxisRaw("Vertical") + cameraRightDirection * Input.GetAxisRaw("Horizontal");
        _movementInput.y = 0;
        _movementInput.Normalize();

        if (_movementInput.x != 0)
        {
            _movingLeft = Mathf.Sign((cameraRightDirection * Input.GetAxisRaw("Horizontal")).x) < 0;
            spriteRenderer.flipX = _movingLeft;
        }

        animator.SetBool("IsMoving", IsMoving);
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _movementInput * moveSpeed;
    }

}
