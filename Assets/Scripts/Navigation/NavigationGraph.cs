using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationGraph : MonoBehaviour
{
    private static Grid grid;
    private static NavigationNode[,] navigationMap;
    [SerializeField]
    private GameObject topRBorder, downLBorder;
    private Vector3Int topRight, downLeft;

    private void Start()
    {
        grid = GetComponentInParent<Grid>();
        downLeft = grid.WorldToCell(downLBorder.transform.position);
        topRight = grid.WorldToCell(topRBorder.transform.position);
        navigationMap = new NavigationNode[topRight.x - downLeft.x, topRight.y - downLeft.y];   
        GetNavigationMap();
        CreateGraphs();
    }
    private void GetNavigationMap()
    {
        RaycastHit2D hitPlatforms, hitStairs;
        Vector3Int cellOrigin = new Vector3Int();
        Vector2 origin = new Vector2();

        for (int i = downLeft.x; i < topRight.x; i++)
            for (int j = downLeft.y; j < topRight.y; j++)
            {
                cellOrigin.Set(i, j, 0);
                origin.Set(grid.CellToWorld(cellOrigin).x, grid.CellToWorld(cellOrigin).y);

                hitPlatforms = Physics2D.Raycast(origin, Vector2.down, 0.6f, LayerMask.GetMask("Default"));
                hitStairs = Physics2D.Raycast(origin, Vector2.down, 0.5f, LayerMask.GetMask("Ignore Raycast"));

                if (hitPlatforms.distance > 0 | hitStairs.collider?.transform.position.x == origin.x)
                    navigationMap[i, j] = new NavigationNode(cellOrigin);
            }
    }
    private void CreateGraphs()
    {
        NavigationNode currNode;

        for (int i = downLeft.x + 1; i < topRight.x -1; i++)
            for (int j = downLeft.y + 1; j < topRight.y - 1; j++)
            {
                currNode = navigationMap[i, j];

                if (currNode != null)
                {
                    GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    sphere.transform.position = grid.CellToWorld(currNode.point);
                }

                currNode?.AddNeighbor(navigationMap[i - 1, j]);
                currNode?.AddNeighbor(navigationMap[i + 1, j]);
                currNode?.AddNeighbor(navigationMap[i, j - 1]);
                currNode?.AddNeighbor(navigationMap[i, j + 1]);
            }
    }
    private static NavigationNode NodeByWorld(Vector2 position)
    {
        var cellPosition = grid.WorldToCell(position);
        var node = navigationMap[cellPosition.x, cellPosition.y];

        return node;
    }
    public static Stack<Vector2> GetPath(Vector2 startingPos, Vector2 finalPos)
    {
        var startNode = NodeByWorld(startingPos);
        var finalNode = NodeByWorld(finalPos);
        var lastNode = BreadthFirstSearch.Search(startNode, finalNode);
        var path = new Stack<Vector2>();
        
        while (lastNode != startNode)
        {
            path.Push(grid.CellToWorld(lastNode.point));
            lastNode = lastNode.Previous;
        }

        return path;
    }
}
