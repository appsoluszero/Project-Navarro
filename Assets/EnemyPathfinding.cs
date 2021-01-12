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
    [SerializeField] private Tile walkable;

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
        gridData.gridval = new Dictionary<Vector3Int, Node>();
        Vector3Int StartIdx = walkableIndicator.WorldToCell(StartPos);
        Vector3Int TargetIdx = walkableIndicator.WorldToCell(TargetPos);
        bool check = walkableIndicator.GetTile(StartIdx) == walkable;
        gridData.gridval.Add(StartIdx, new Node(check, StartIdx, 0, CalculateDistance(StartIdx, TargetIdx)));
        gridData.gridval.Add(TargetIdx, new Node(check, TargetIdx, int.MaxValue, int.MaxValue));
        Node StartNode = gridData.gridval[StartIdx];
        Node TargetNode = gridData.gridval[TargetIdx];
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

                int MovementCost = currNode.gCost + CalculateDistance(currNode.NodeIndex, neighbor.NodeIndex);
                if(MovementCost < neighbor.gCost)
                {
                    neighbor.lastNode = currNode;
                    neighbor.gCost = MovementCost;
                    neighbor.hCost = CalculateDistance(neighbor.NodeIndex, TargetNode.NodeIndex);
                    if(!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }
        return null;
    }

    private int CalculateDistance(Vector3Int n1, Vector3Int n2)
    {
        int xDis = Mathf.Abs(n1.x - n2.x);
        int yDis = Mathf.Abs(n1.y - n2.y);
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
                Vector3Int nowPos = new Vector3Int(currNode.NodeIndex.x + i,currNode.NodeIndex.y + j, 0);
                if(!gridData.gridval.ContainsKey(nowPos))
                {
                    bool check = walkableIndicator.GetTile(nowPos) == walkable;
                    gridData.gridval.Add(nowPos, new Node(check, nowPos, int.MaxValue, int.MaxValue));
                }
                n.Add(gridData.gridval[nowPos]);
            }
        }
        return n;
    }
}
