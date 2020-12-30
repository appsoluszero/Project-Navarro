using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ReturnPosition : MonoBehaviour
{
    [SerializeField] private GridLayout grid;
    [SerializeField] private Tilemap tilemap;

    void Update()
    {
        Vector3 LookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Vector3Int thatGrid = grid.WorldToCell(LookDirection);
        Vector3 currentPos = grid.CellToWorld(grid.WorldToCell(LookDirection));
        currentPos.x += grid.cellSize.x/2f;
        currentPos.y += grid.cellSize.y/2f;
        Debug.Log(currentPos);
        //tilemap.SetTile(thatGrid, )
    }
}
