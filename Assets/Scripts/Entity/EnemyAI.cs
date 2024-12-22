using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Vector2 startingPosition;

    public float roamRadius = 10;

    Rigidbody2D rigidBody;

    public void Start()
    {
        startingPosition = transform.position;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private Vector2 GetRoamingPosition()
    {
        return startingPosition + Direction2D.GetRandomDirectionf() * Random.Range(roamRadius / 10, roamRadius);
    }

    public void Update()
    {

    }
}
