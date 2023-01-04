using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BreadthFirstSearch
{
    public static NavigationNode Search(NavigationNode startNode, NavigationNode finalNode)
    {
        var visited = new List<NavigationNode>();
        var frontier = new Queue<NavigationNode>();
        NavigationNode current = null;

        frontier.Enqueue(startNode);

        while (frontier.Count > 0)
        {
            current = frontier.Dequeue();
            visited.Add(current);

            if (current == finalNode)
                break;

            var neighbours = current.GetNeighbors();
            foreach (var neighbour in neighbours)
                if (!visited.Contains(neighbour))
                {
                    frontier.Enqueue(neighbour);
                    neighbour.Previous = current;
                }
        }

        return current.Equals(finalNode) ? current : startNode;
    }
}
