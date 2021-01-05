using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ReturnPosition : MonoBehaviour
{
    [SerializeField] private Transform actualPosition;
    [SerializeField] private GridLayout grid;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tile changeToTile;
    [SerializeField] private Tile unwalkableTile;
    void Update()
    {
        Vector3 LookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        LookDirection.z = 0f;
        Vector3Int thatGrid = grid.WorldToCell(LookDirection);
        if(Input.GetMouseButtonDown(0))
        {
            if(tilemap.GetTile(thatGrid) == unwalkableTile)
            {
                Debug.Log("FALSE");
            }
            else if(tilemap.GetTile(thatGrid) == changeToTile)
            {
                Debug.Log("TRUE");
            }
        }
        Vector3 currentPos = grid.CellToWorld(grid.WorldToCell(actualPosition.position));
        currentPos.x += grid.cellSize.x/2f;
        currentPos.y += grid.cellSize.y/2f;
        //Debug.Log(grid.WorldToCell(actualPosition.position));
    }
}
