using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationNode
{
    public readonly Vector3Int point;
    private List<NavigationNode> neighbors;
    public NavigationNode Previous
    { get; set; }
    public NavigationNode(Vector3Int point)
    {
        this.point = point;
        neighbors = new List<NavigationNode>();
    }
    public void AddNeighbor(NavigationNode neighbor)
    {
        if (neighbor != null)
            neighbors.Add(neighbor);
    }
    public List<NavigationNode> GetNeighbors()
    {
        return neighbors;
    }
}
