using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Knockback : MonoBehaviour
{
    public bool knockedBack;

    public float knockbackTime;

    Rigidbody2D rigidBody;
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void GetKnockback(Transform source, float thrust)
    {
        knockedBack = true;
        var force = thrust * rigidBody.mass * (transform.position - source.position).normalized;
        rigidBody.AddForce(force, ForceMode2D.Impulse);
        StartCoroutine(KnockbackRoutine());
    }

    private IEnumerator KnockbackRoutine()
    {
        yield return new WaitForSeconds(knockbackTime);
        rigidBody.velocity = Vector2.zero;
        knockedBack = false;
    }
}
