using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManhattanDistance : Heuristic
{
    public override float calculateH(Node actual, Node end)
    {
        float dx = Mathf.Abs(actual.location.x - end.location.x);
        float dy = Mathf.Abs(actual.location.z - end.location.z);

        return STRAIGHT_COST * (dx + dy);
    }
}
