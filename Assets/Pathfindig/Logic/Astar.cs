using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// REF: https://web.archive.org/web/20170509000025/http://www.policyalmanac.org/games/aStarTutorial.htm
public class Astar : PathFinder
{

    public override IEnumerator FindPath()
    {
        path.Clear();
        bool success = Search(startNode);
        if (success)
        {
            Node node = this.endNode;
            while (node.ParentNode != null)
            {
                path.Add(node.location);
                node = node.ParentNode;
            }
            path.Reverse();
            availableIndex = path.Count - 1;
        }

        fin = true;
        yield return null;
    }


    private bool Search(Node currentNode)
    {
        currentNode.State = Node.NodeState.Closed;
        List<Node> nextNodes = getAdjacentWalkableNodes(currentNode);
        nextNodes.Sort((node1, node2) => node1.F.CompareTo(node2.F));
        foreach (var nextNode in nextNodes)
        {
            if (nextNode.location == this.endNode.location)
            {
                return true;
            }
            else
            {
                if (Search(nextNode)) 
                    return true;
            }
        }
        return false;
    }



    private float getTraversalCost(Node child, Node parent)
    {
        return diagonal(child,parent) ? heuristic.DIAGONAL_COST + child.getCost() : heuristic.STRAIGHT_COST + child.getCost();
    }

    private void updateCost(Node child, Node parent)
    {
        child.G = parent.G + getTraversalCost(child, parent);
        
        child.setH(heuristic.calculateH(child,endNode));
    }


    public List<Node> getAdjacentWalkableNodes(Node fromNode)
    {
        List<Node> walkableNodes = new List<Node>();
        IEnumerable<Vector3> nextLocations = grid.getAdjacentLocations(fromNode);

        foreach (Vector3 location in nextLocations)
        {
            int x = (int) location.x;
            int z = (int) location.z;

            
            if (x < 0 || x >= Grid.X_DIMENSION || z < 0 || z >= Grid.Z_DIMENSION)
                continue;

            Node node = grid.grid[x][z] ;
            
            if (!node.isWalkable())
            {
                continue;
            }
                

            
            if (node.State == Node.NodeState.Closed)
                continue;

           
            if (node.State == Node.NodeState.Open)
            {
                float traversalCost = getTraversalCost(node, node.ParentNode);
                
                if (traversalCost < fromNode.G)
                {
                    
                    walkableNodes.Add(node);
                }
            }
            else
            {
               
                node.ParentNode = fromNode;
                node.State = Node.NodeState.Open;
                updateCost(node, fromNode);
                walkableNodes.Add(node);
            }
        }

        return walkableNodes;
    }


}
