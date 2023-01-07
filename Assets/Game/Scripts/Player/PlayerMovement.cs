using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float moveSpeed = 5f; 

    private Rigidbody2D _rigidbody;
    private Vector3 _movementInput;

    private bool IsMoving => _movementInput != Vector3.zero;

    private bool _movingLeft;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _movementInput = new Vector3( Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        _movementInput.z = 0;
        _movementInput.Normalize();

        if (_movementInput.x != 0)
        {
            _movingLeft = Mathf.Sign(Input.GetAxisRaw("Horizontal")) < 0;
            spriteRenderer.flipX = _movingLeft;
        }


        animator.SetBool("IsMoving", IsMoving);
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _movementInput * moveSpeed;
    }

}
