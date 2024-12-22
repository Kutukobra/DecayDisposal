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

    public int rootLength;
    public int rootWidth;

    public int roomCount;

    public int roomSizeVariation;

    public int roomWidth;
    public int roomHeight;
    public bool iterationStartRandom = false;

    private HashSet<Vector2Int> floorTiles = new();
    private HashSet<Vector2Int> walls;

    public EnemyAI enemies;

    void Start()
    {
        GenerateMap();

    }

    void GenerateMap()
    {
        floorTileMap.ClearAllTiles();

        var currentPosition = new Vector2Int();
        
        for (int i = 0; i < roomCount; i++)
        {
            var room = RandomWalk.GenerateSquare(currentPosition, roomWidth, roomHeight, roomSizeVariation);

            floorTiles.UnionWith(room);

            var corridor = RandomWalk.GenerateCorridor(currentPosition, rootLength, rootWidth);
            floorTiles.UnionWith(corridor);

            currentPosition = corridor.ElementAt(corridor.Count() - 1);
        }

        walls = WallGenerator.FindWallPositions(floorTiles);
        
        foreach(var wall in walls)
        {
            floorTileMap.SetTile(floorTileMap.WorldToCell((Vector3Int)wall), wallTile);
        }

        foreach(var floor in floorTiles)
        {
            floorTileMap.SetTile(floorTileMap.WorldToCell((Vector3Int)floor), floorTile);
        }
    }

    void GenerateEntities(HashSet<Vector2Int> room)
    {
        
    }
}
