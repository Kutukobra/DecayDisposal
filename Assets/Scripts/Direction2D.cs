using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction2D
{
    // Random up, right, left, or down randomly
    public static Vector2Int GetRandomDirection()
    {
        int directionIndex = Random.Range(0, 4);

        var direction = new Vector2Int();
        switch(directionIndex)
        {
            case 0:
                direction = Vector2Int.up;
                break;

            case 1:
                direction = Vector2Int.right;
                break;

            case 2:
                direction = Vector2Int.down;
                break;

            case 3:
                direction = Vector2Int.left;
                break;
        }
        return direction;
    }

    public static List<Vector2Int> cardinalDirectionList = new List<Vector2Int>
    {
        new Vector2Int(0, 1),  // Up
        new Vector2Int(1, 0),  // Right
        new Vector2Int(0, -1), // Down
        new Vector2Int(-1, 0)  // Left
    };

    public static List<Vector2Int> directionList = new List<Vector2Int>
    {
        new Vector2Int(0, 1),   // Up
        new Vector2Int(1, 0),   // Right
        new Vector2Int(0, -1),  // Down
        new Vector2Int(-1, 0),  // Left
        new Vector2Int(1, 1),   // Up-Right
        new Vector2Int(1, -1),  // Down-Right
        new Vector2Int(-1, -1), // Down-Left
        new Vector2Int(-1, 1)   // Up-Left
    };
}
