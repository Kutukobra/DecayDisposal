using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class RangedWeapon : MonoBehaviour
{
    [Header("Projectile Pool")]
    private IObjectPool<Projectile> objectPool;

    [Header("Projectile")]
    public Projectile projectile;
    [SerializeField] private Transform projectileSpawnPoint;

    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;



    void Awake()
    {
        objectPool = new ObjectPool<Projectile>(CreateProjectile, OnGetFromPool, OnReleaseFromPool, OnDestroyPooledObject, collectionCheck, defaultCapacity, maxSize);
    }

    public void Shoot(Vector2 targetDirection)
    {       
        Projectile projectileObject = objectPool.Get();
        if (projectileObject == null)
            return;

        projectileObject.transform.SetPositionAndRotation(projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        projectileObject.direction = targetDirection;    
        projectileObject.Deactivate();
    }

    Projectile CreateProjectile()
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
