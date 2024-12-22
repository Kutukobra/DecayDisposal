using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTileMap;

    [SerializeField]
    private TileBase floorTile;

    public Vector2Int startPosition;
    public int iterationCount;
    public int walkLength;
    public bool iterationStartRandom = false;

    void Start()
    {
        var generatedTiles = new HashSet<Vector2Int>();
        for (int i = 0; i < iterationCount; i++)
        {
            var trunk = RandomWalk.GenerateOneWalk(startPosition, walkLength);
            generatedTiles.UnionWith(trunk);
            var branchPoint = trunk.ElementAt(trunk.Count() - 1);
            generatedTiles.UnionWith(RandomWalk.GenerateRandomWalk(branchPoint, 3, 30, true));
            startPosition = branchPoint;
        }

        foreach(var tile in generatedTiles)
        {
            floorTileMap.SetTile(floorTileMap.WorldToCell((Vector3Int)tile), floorTile);
        }
    }
}
