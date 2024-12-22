using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class RangedMonster : MonoBehaviour
{
    HealthComponent healthComponent;
    Animator animator;
    Rigidbody2D rigidBody;

    private SpriteRenderer sprite;

    [SerializeField]
    private Projectile projectile;

    public Transform target;

    public float attackSpeed;
    private float nextAttackTime;

    public Transform projectileSpawnPoint;

    private Vector2 targetDirection;

    private bool isShooting = false;

    [Header("Projectile Pool")]
    private IObjectPool<Projectile> objectPool;

    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 20;
    private readonly int maxSize = 20;

    void Awake()
    {
        objectPool = new ObjectPool<Projectile>(CreateBullet, OnGetFromPool, OnReleaseFromPool, OnDestroyPooledObject, collectionCheck, defaultCapacity, maxSize);

        healthComponent = GetComponent<HealthComponent>();
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        targetDirection = (target.position - projectileSpawnPoint.position).normalized;
        sprite.flipX = targetDirection.x < 0;

        animator.SetBool("dead", healthComponent.isDead);

        Shoot();
    }

    void Shoot()
    {
        if (Time.time < nextAttackTime)
            return;

        nextAttackTime = Time.time + 1 / attackSpeed;
            
        Projectile projectileObject = objectPool.Get();
        if (projectileObject == null)
            return;

        projectileObject.transform.SetPositionAndRotation(projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        projectileObject.direction = targetDirection;
    }

    Projectile CreateBullet()
    {
        Projectile projectileInstance = Instantiate(projectile);
        projectileInstance.objectPool = objectPool;
        return projectileInstance;
    }

    void OnGetFromPool(Projectile pooledProjectile)
    {
        pooledProjectile.gameObject.SetActive(true);
    }

    void OnReleaseFromPool(Projectile pooledProjectile)
    {
        pooledProjectile.gameObject.SetActive(false);
    }

    void OnDestroyPooledObject(Projectile pooledProjectile)
    {
        Destroy(pooledProjectile.gameObject);
    }
}
