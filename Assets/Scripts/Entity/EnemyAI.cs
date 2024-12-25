using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Vector2 startingPosition;

    public float roamRadius = 10;
    public float movementSpeed = 5f;

    Rigidbody2D rigidBody;

    private Vector2 roamingPosition;
    public float roamInterval = 10f;

    public void Start()
    {
        startingPosition = transform.position;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private Vector2 GetRoamingPosition()
    {
        return startingPosition + Direction2D.GetRandomDirectionf() * Random.Range(roamRadius / 10, roamRadius);
    }

    private float nextTime = 0f;
    public void Update()
    {
        if (Time.time > nextTime)
        {
            roamingPosition = GetRoamingPosition();
            nextTime = Time.time + roamInterval;
        }

        if (Vector2.Distance(transform.position, roamingPosition) < 2)
        {
            rigidBody.velocity = Vector2.zero;
        }
        else
        {
            rigidBody.velocity = (roamingPosition - (Vector2)transform.position).normalized * movementSpeed;
        }
    }
}
