using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    public float damage;
    public float speed;
    public Vector2 direction;

    public IObjectPool<Projectile> objectPool;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rigidBody.velocity = direction * speed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        HealthComponent collisionHealth = col.gameObject.GetComponent<HealthComponent>();
        collisionHealth?.TakeDamage(damage);
        objectPool.Release(this);
    }
}
