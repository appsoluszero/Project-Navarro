using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridInitialize : MonoBehaviour
{
    [Header("Walkable")]
    [SerializeField] private Tilemap walkableIndicator;
    [SerializeField] private Tile walkable;
    [SerializeField] private Tile unwalkable;
    public Dictionary<Vector3Int, Node> gridval;
}

public class Node
{
    public bool walkable;
    public int gCost;
    public int hCost;
    public Vector3Int NodeIndex;
    public Node lastNode;
    public Node(bool _walkable, Vector3Int idx, int g, int h)
    {
        walkable = _walkable;
        NodeIndex = idx;
        gCost = g;
        hCost = h;
        lastNode = null;
    }
    public int fCost { get { return gCost + hCost; } }
}
