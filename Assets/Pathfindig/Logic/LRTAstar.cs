using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRTAstar : PathFinder
{

    public override IEnumerator FindPath()
    {
        path.Clear();

        Node node = startNode;
        node.setH(heuristic.calculateH(node, endNode));
        while(node.location != endNode.location)
        {
            Node next = getNextNode(node);
          
            node.H = node.H > next.F ? node.H : next.F;

            node = next;
            node.State = Node.NodeState.Open;

            path.Add(node.location);
            availableIndex++;
            yield return null;

        }
        fin = true;
    }


    private Node getNextNode(Node actual)
    {
        List<Vector3> adjLocation = grid.getAdjacentLocations(actual);
        List<Node> adjNodes = new List<Node>(adjLocation.Capacity);
        
        foreach (Vector3 n in adjLocation){
            if (grid.locationToNode(n) != null && grid.locationToNode(n).isWalkable())
                adjNodes.Add(grid.locationToNode(n));
        }

        List<float> cost = new List<float>();
        adjNodes.ForEach(n =>  updateCost(n,actual));



        // Min (G + H)
        float minCost = Mathf.Infinity;
        Node minNode = null;
        foreach (Node n in adjNodes)
        {
            if (n.F < minCost){
                minNode = n;
                minCost = n.G + n.H;
            }
        }

        return minNode;


    }

     private void updateCost(Node child, Node parent)
    {
        child.G = diagonal(child,parent) ? heuristic.DIAGONAL_COST + child.getCost() : heuristic.STRAIGHT_COST + child.getCost();
        if (child.State == Node.NodeState.Open) child.G += 10 * heuristic.DIAGONAL_COST;
        if (child.H == -1) child.setH(heuristic.calculateH(child,endNode));
    }

}
