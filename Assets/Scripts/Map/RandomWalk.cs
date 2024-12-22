using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;

public class RandomWalk
{
    public static HashSet<Vector2Int> GenerateRandomWalk(Vector2Int startPosition, int walkLength = 5, int iterationCount = 1, bool iterationStartRandom = false)
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        for (int i = 0; i < iterationCount; i++)
        {
            floorPositions.UnionWith(GenerateOneWalk(startPosition, walkLength));
            if (iterationStartRandom)
            {
                startPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }
            else
            {
                startPosition = floorPositions.Last();
            }
        }
        return floorPositions;
    }

    // Generate one path
    public static HashSet<Vector2Int> GenerateOneWalk(Vector2Int startPosition, int walkLength)
    {
        var currentPosition = startPosition;
        var newPosition = new Vector2Int();

        var walk = new HashSet<Vector2Int>();
        for (int i = 0; i < walkLength; i++)
        {
            walk.Add(currentPosition);
            
            newPosition = currentPosition + GetRandomDirection();

            currentPosition = newPosition;
        }

        return walk;
    }

    // Random up, right, left, or down randomly
    private static Vector2Int GetRandomDirection()
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
}
