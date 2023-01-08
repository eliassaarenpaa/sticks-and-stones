using DG.Tweening;
using System.Collections;
using UnityEngine;

public class EasyEnemy : Enemy
{
    [SerializeField] private AudioSource spawnSource;
    [SerializeField] private AudioSource hurtSource;
    [SerializeField] private AudioSource shootSource;

    [SerializeField] private Animator weaponChargeAnimator;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float distanceFromPlayerToShoot;
    [SerializeField] private float chargeDuration;
    [SerializeField] private float shootRecoverDuration;
    [SerializeField] private float stunDuration;
    [SerializeField] private EnemyBullet bulletPrefab;
    [SerializeField] private Transform bulletSpawnPos;
    [SerializeField] private ParticleSystem shootParticle;

    private Transform _player;

    private bool IsInShootRange => Vector2.Distance(transform.position, _player.position) <= distanceFromPlayerToShoot;
    private Vector3 DirectionToPlayer => (_player.position - transform.position).normalized;

    private bool _isShooting;
    private bool _canShoot;

    protected override void Awake()
    {
        base.Awake();
        _player = GameObject.FindWithTag("Player").transform;
    }
    private void Start()
    {
        _canShoot = true;
    }

    private void OnEnable()
    {
        spawnSource.Play();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    protected override void Update()
    {
        base.Update();
        
        transform.localScale = Mathf.Abs(transform.localScale.x) * new Vector3(DirectionToPlayer.x < 0 ? -1 : 1, 1, 1);


        if (_isShooting)
        {
            return;
        }

        if (IsInShootRange && _canShoot)
        {
            StartCoroutine(ShootSequence());
        }
        else
        {
            WalkTowardsPlayer();
        }
    }



    private void WalkTowardsPlayer()
    {
        transform.position += DirectionToPlayer * movementSpeed *  Time.deltaTime;
    }


    private IEnumerator ShootSequence()
    {
        _isShooting = true;

        weaponChargeAnimator.transform.DOKill();
        weaponChargeAnimator.transform.localScale = Vector3.zero; 
        weaponChargeAnimator.transform.DOScale(Vector3.one * 2, chargeDuration).SetEase(Ease.Linear);

        weaponChargeAnimator.gameObject.SetActive(true);

        
        yield return new WaitForSeconds(chargeDuration);

        var bullet = Instantiate(bulletPrefab);

        if (bullet)
        {
            bullet.transform.position = bulletSpawnPos.transform.position;
            bullet.SetDireciton(DirectionToPlayer);
            bullet.gameObject.SetActive(true);
            shootSource.Play();
        }

        shootParticle.Play();
        weaponChargeAnimator.gameObject.SetActive(false);

        yield return new WaitForSeconds(shootRecoverDuration);

        _isShooting = false;
    }

    public override void Defend(IAttacker attacker, Vector2 attackDir, Vector2 contactPoint)
    {
        base.Defend(attacker, attackDir, contactPoint);

        hurtSource.Play();

        if (Health > 0)
        {
            GetStunned();
        }
    }

    private void GetStunned()
    {
        _canShoot = false;
        _isShooting = false;
        col.enabled = false;
        StopAllCoroutines();
        weaponChargeAnimator.gameObject.SetActive(false);

        Invoke(nameof(RecoverFromStun), stunDuration);
    }

    private void RecoverFromStun()
    {
        col.enabled = true;
        _canShoot = true;
    }

    protected override void Die()
    {
        base.Die();

        StopAllCoroutines();
        _canShoot = false;
        _isShooting = false;
        weaponChargeAnimator.gameObject.SetActive(false);
    }

}
