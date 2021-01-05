using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridInitialize : MonoBehaviour
{
    [SerializeField] private int gridSizeX;
    [SerializeField] private int gridSizeY;
    [Header("Walkable")]
    [SerializeField] private Tilemap walkableIndicator;
    [SerializeField] private Tile walkable;
    [SerializeField] private Tile unwalkable;
    public Node[,] gridValue;
    
    public void AssignNewGridData()
    {
        gridValue = new Node[gridSizeX,gridSizeY];
        for(int i = 0;i<gridSizeX;++i)
        {
            for(int j = 0;j<gridSizeY;++j)
            {
                bool check = walkableIndicator.GetTile(new Vector3Int(i,j,0)) == walkable;
                gridValue[i,j] = new Node(check, new Vector3Int(i,j,0));
            }
        }
    }
}
public class Node
{
    public bool walkable;
    public int gCost;
    public int hCost;
    public Vector3Int NodeIndex;
    public Node lastNode;
    public Node(bool _walkable, Vector3Int idx)
    {
        walkable = _walkable;
        NodeIndex = idx;
        gCost = int.MaxValue;
        hCost = int.MaxValue;
        lastNode = null;
    }
    public int fCost { get { return gCost + hCost; } }
}
