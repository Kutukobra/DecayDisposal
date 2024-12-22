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

    [SerializeField]
    private float timeoutDelay = 10f;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rigidBody.velocity = direction * speed;
    }

    IEnumerator DeactivateRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Reset Physics
        rigidBody.velocity = Vector2.zero;
        rigidBody.angularVelocity = 0; 

        objectPool.Release(this); 
    }

    public void Deactivate()
    {
        StartCoroutine(DeactivateRoutine(timeoutDelay));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        StopAllCoroutines();
        objectPool.Release(this);
    }
}
