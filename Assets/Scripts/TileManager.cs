using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public static TileManager _Instance {get; private set;}

    [SerializeField]
    private Tilemap _defaultTileMap;

    public Vector3 _TileOffset { get; private set; } = new Vector3(0.5f,0.5f,0);

    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    /// <summary>
    /// contains the methods needed to know if you can place a gameobject
    /// to place it, or round your position to the center of a tile.
    /// </summary>
    /// <param name="TileMap">the Tilemap where you want to create the object</param>
    /// <param name="TowerPrefab">the object you want to create</param>
    /// <param name="WorldPosition"> the position you are in the world</param>
    /// <param name="Rotation">the rotation of the object you want to create</param>
    public GameObject Place(Tilemap TileMap, GameObject TowerPrefab, Vector3 WorldPosition)
    {
        return Instantiate(TowerPrefab, RoundToCell(TileMap, WorldPosition), quaternion.identity,TileMap.transform);
    }

    public GameObject Place(GameObject StructPrefab, Vector3 WorldPosition)
    {
        if (_defaultTileMap != null)
        {
            return Place(_defaultTileMap, StructPrefab, WorldPosition);
        }
        return null;
    }

    public Vector3 RoundToCell(Tilemap TileMap,Vector3 WorldPosition)
    {
        //return the world position of the cell the sent position is in
        return (TileMap.CellToWorld(TileMap.WorldToCell(WorldPosition)));
    }

    public Vector3 RoundToCell(Vector3 WorldPosition)
    {
        //return the world position of the cell the sent position is in
        return RoundToCell(_defaultTileMap, WorldPosition);
    }

    public bool CanPlace(Vector3 WorldPosition)
    {
        //return true if you can place a GameObject at the desired location
     
        Vector2 position = RoundToCell(WorldPosition);
        
        List<Collider2D> colliders = Physics2D.OverlapBoxAll(position,Vector2.one/2,0).ToList();

        return (colliders.Where(x => x.GetComponentInParent<Tower>() != null && x.transform.parent.parent.GetComponent<Tilemap>() == _defaultTileMap).Count() == 0);
    }
}
