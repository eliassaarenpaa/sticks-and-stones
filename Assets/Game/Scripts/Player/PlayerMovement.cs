using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private AudioSource dodgeSource;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float _dodgeSpeed = 5f;
    [SerializeField] private float _dodgeDistance = 5f;
    [SerializeField] private Ease _dodgeEase;
    [SerializeField] private float _dodgeCooldown = 2f;

    private float _dodgeCooldownTimer;

    [SerializeField] private AnimationCurve _dodgeSpeedModifierCurve;
    [SerializeField] private AnimationCurve _dodgeMovementInfluenceCurve;

    private Rigidbody2D _rigidbody;
    private Vector3 _movementInput;

    private bool IsMoving => _movementInput != Vector3.zero;
    private bool IsDodging => _dodgeRoutine != null;

    private bool _movingLeft;

    private Coroutine _dodgeRoutine;
    private float _dodgeMovementInfluenceMultiplier;
    private Tween _dodgeTween;
    private Vector3 _dodgeMovement;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _dodgeCooldownTimer = 0;
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

        if (IsMoving && Input.GetMouseButtonDown(1)) 
        {
            if(_dodgeCooldownTimer <= 0)
            {
                _dodgeCooldownTimer = _dodgeCooldown;
                Dodge();
            }
        }

        _dodgeCooldownTimer -= Time.deltaTime;

        animator.SetBool("IsMoving", IsMoving);
    }

    private void Dodge()
    {
        dodgeSource.Play();
        
        if (_dodgeRoutine != null)
        {
            StopCoroutine(_dodgeRoutine);
        }

        _dodgeRoutine = StartCoroutine(DodgeRoutine());

        IEnumerator DodgeRoutine()
        {
            float dodgeDuration = _dodgeDistance / _dodgeSpeed;
            var awaitDodgeTravel = new WaitForSeconds(dodgeDuration);
            var dodgeDir = _movementInput;

            var currentDodgeTime = 0f;

            _dodgeTween?.Kill();
            _dodgeTween = DOTween.To(() => currentDodgeTime, x => currentDodgeTime = x, dodgeDuration, dodgeDuration).OnUpdate(() =>
            {
                var speedModifier = _dodgeSpeedModifierCurve.Evaluate(Mathf.InverseLerp(0, dodgeDuration, currentDodgeTime));
                _dodgeMovementInfluenceMultiplier = _dodgeMovementInfluenceCurve.Evaluate(Mathf.InverseLerp(0, dodgeDuration, currentDodgeTime));
                _dodgeMovement = dodgeDir.normalized * _dodgeSpeed * speedModifier;
            }).SetEase(_dodgeEase);

            yield return awaitDodgeTravel;

            _dodgeRoutine = null;
        }
    }

    private void FixedUpdate()
    {
        var movement = _movementInput.normalized * moveSpeed;
        var dodgeMovement = Vector3.Lerp(Vector3.zero, _dodgeMovement, 1 - _dodgeMovementInfluenceMultiplier) + Vector3.Lerp(Vector3.zero, movement, _dodgeMovementInfluenceMultiplier);

        _rigidbody.velocity = IsDodging ? dodgeMovement : movement;
    }

}
