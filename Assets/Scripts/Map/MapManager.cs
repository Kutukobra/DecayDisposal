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

    [SerializeField]
    private TileBase wallTile;

    public Vector2Int startPosition;
    public int walkLength;
    public int iterationCount;
    public bool iterationStartRandom = false;

    void Start()
    {
        var generatedTiles = RandomWalk.GenerateRandomWalk(startPosition, walkLength, iterationCount, iterationStartRandom);

        foreach(var tile in generatedTiles)
        {
            floorTileMap.SetTile(floorTileMap.WorldToCell((Vector3Int)tile), floorTile);
        }

        var walls = WallGenerator.FindWallPositions(generatedTiles);
        
        foreach(var wall in walls)
        {
            floorTileMap.SetTile(floorTileMap.WorldToCell((Vector3Int)wall), wallTile);
        }
    }
}
