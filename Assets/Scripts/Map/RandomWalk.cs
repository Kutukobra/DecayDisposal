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
            
            newPosition = currentPosition + Direction2D.GetRandomDirection();

            currentPosition = newPosition;
        }

        return walk;
    }

}
