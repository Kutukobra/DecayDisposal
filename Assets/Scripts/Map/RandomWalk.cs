using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

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

    public static HashSet<Vector2Int> GenerateCorridor(Vector2Int startPosition, int walkLength, int width, int variation = 0)
    {
        var currentPosition = startPosition;
        var direction = Direction2D.GetRandomDirection();

        walkLength += Random.Range(-variation, variation + 1);

        var corridor = new HashSet<Vector2Int>();

        for (int i = 0; i < walkLength; i++)
        {
            var corridorSegment = currentPosition;

            for (int j = 0; j < width; j++)
            {
                corridor.Add(corridorSegment);
                corridorSegment += Vector2Int.one - new Vector2Int(Mathf.Abs(direction.x), Mathf.Abs(direction.y));
            }
            currentPosition += direction;
        }

        return corridor;
    }

    public static HashSet<Vector2Int> GenerateSquare(Vector2Int startPosition, int width, int height, int variation = 0)
    {
        var square = new HashSet<Vector2Int>();

        width += Random.Range(-variation, variation + 1);
        height += Random.Range(-variation, variation + 1);

        for (int i = -width/2; i < width/2; i++)
        {
            for (int j = -height/2; j < height/2; j++)
            {
                square.Add(startPosition + new Vector2Int(i, j));
            }
        }
        return square;
    }
}
