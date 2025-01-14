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

    public int enemyPerRoom = 5;

    private HashSet<Vector2Int> floorTiles = new();
    private HashSet<Vector2Int> walls;

    public RangedMonster enemy;
    public Waste waste;

    [SerializeField]
    private AudioSource generate_map;

    public void GenerateMap(Vector2Int position)
    {
        generate_map.Play();
        floorTileMap.ClearAllTiles();

        var currentPosition = startPosition;
        
        for (int i = 0; i < roomCount; i++)
        {
            var room = RandomWalk.GenerateSquare(currentPosition, roomWidth, roomHeight, roomSizeVariation);

            room.UnionWith(RandomWalk.GenerateRandomWalk(currentPosition, 10, 20, true));

            GenerateEntities(room);

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
        for (int j = 0; j < enemyPerRoom; j++)
        {
            var spawnPosition = floorTileMap.WorldToCell((Vector3Int)room.ElementAt(Random.Range(0, room.Count())));
            Instantiate(enemy, (Vector3)spawnPosition, Quaternion.identity);
        }

        var wastePosition = floorTileMap.WorldToCell((Vector3Int)room.ElementAt(Random.Range(0, room.Count())));
        Instantiate(waste, (Vector3)wastePosition, Quaternion.identity);
    }
}
