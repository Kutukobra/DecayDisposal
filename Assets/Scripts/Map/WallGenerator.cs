using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class WallGenerator
{
    public static HashSet<Vector2Int> FindWallPositions(HashSet<Vector2Int> floorPositions)
    {
        var wallPositions = new HashSet<Vector2Int>();
        foreach(var floor in floorPositions)
        {
            foreach (var direction in Direction2D.directionList)
            {
                var neighbour = floor + direction;
                if (floorPositions.Contains(neighbour))
                    continue;

                wallPositions.Add(neighbour);
            }
        }

        return wallPositions;
    }
}
