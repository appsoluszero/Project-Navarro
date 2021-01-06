using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyPathfinding : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private GridInitialize gridData;
    [SerializeField] private Transform playerPos;
    [SerializeField] private Grid grid;
    [Header("Walkable")]
    [SerializeField] private Tilemap walkableIndicator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        List<Node> path = FindPath(transform.position, playerPos.position);
        if(path != null)
        {
            for(int i = 0;i<path.Count - 1;++i)
            {
                Vector3 n1 = grid.CellToWorld(path[i].NodeIndex);
                n1.x += grid.cellSize.x/2f;
                n1.y += grid.cellSize.y/2f;
                Vector3 n2 = grid.CellToWorld(path[i+1].NodeIndex);
                n2.x += grid.cellSize.x/2f;
                n2.y += grid.cellSize.y/2f;
                Debug.DrawLine(n1, n2, Color.green,1f);
            }
        }
    }

    List<Node> FindPath(Vector2 StartPos, Vector2 TargetPos)
    {
        gridData.AssignNewGridData();  
        Vector3Int StartIdx = walkableIndicator.WorldToCell(StartPos);
        Vector3Int TargetIdx = walkableIndicator.WorldToCell(TargetPos);
        Node StartNode = gridData.gridValue[StartIdx.x, StartIdx.y];
        Node TargetNode = gridData.gridValue[TargetIdx.x, TargetIdx.y];
        StartNode.gCost = 0;
        StartNode.hCost = CalculateDistance(StartNode, TargetNode);
        List<Node> openSet = new List<Node> {StartNode};
        HashSet<Node> closedSet = new HashSet<Node>();
        while(openSet.Count > 0)
        {
            Node currNode = openSet[0];
            for(int i = 1; i<openSet.Count;++i)
            {
                if(openSet[i].fCost < currNode.fCost || (openSet[i].fCost == currNode.fCost && openSet[i].hCost < currNode.hCost))
                {
                    currNode = openSet[i];
                }   
            }
            if(currNode == TargetNode)
            {
                List<Node> pathRetract = new List<Node>();
                Node cNode = TargetNode;
                pathRetract.Add(cNode);
                while(cNode.lastNode != null)
                {
                    pathRetract.Add(cNode.lastNode);
                    cNode = cNode.lastNode;
                }
                pathRetract.Reverse();
                return pathRetract;
            }
            openSet.Remove(currNode);
            closedSet.Add(currNode);
            foreach(Node neighbor in neighborList(currNode))
            {
                if(!neighbor.walkable || closedSet.Contains(neighbor)) continue;

                int MovementCost = currNode.gCost + CalculateDistance(currNode, neighbor);
                if(MovementCost < neighbor.gCost)
                {
                    neighbor.lastNode = currNode;
                    neighbor.gCost = MovementCost;
                    neighbor.hCost = CalculateDistance(neighbor, TargetNode);
                    if(!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }
        return null;
    }

    private int CalculateDistance(Node n1, Node n2)
    {
        int xDis = Mathf.Abs(n1.NodeIndex.x - n2.NodeIndex.x);
        int yDis = Mathf.Abs(n1.NodeIndex.y - n2.NodeIndex.y);
        int remain = Mathf.Abs(xDis - yDis);
        return 14 * Mathf.Min(xDis, yDis) + 10 * remain;
    }

    private List<Node> neighborList(Node currNode)
    {
        List<Node> n = new List<Node>();
        for(int i = -1;i<=1;++i)
        {
            for(int j = -1;j<=1;++j)
            {
                n.Add(gridData.gridValue[currNode.NodeIndex.x + i,currNode.NodeIndex.y + j]);
            }
        }
        return n;
    }
}
