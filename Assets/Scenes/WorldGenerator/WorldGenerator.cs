using QFSW.QC;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGenerator : MonoBehaviour
{
    public Grid grid;           // assign in the Inspector
    public int tilesVertically = 16;
    public float pixelsPerUnit = 32f;   // must match your sprite import setting
    public float cellSize = 1f;
    
    public Tilemap tilemap; // assign in the Inspector
    public Tile tilePrefab; // assign in the Inspector

    void Awake ()
    {
        Camera.main.orthographicSize = ((float)tilesVertically / 2);
        
        /*tilemap.SetTile(new Vector3Int(0, 0, 0), tilePrefab);
        tilemap.GetTile(new Vector3Int(0, 0, 0));
        tilemap.Get;*/
    }
}
