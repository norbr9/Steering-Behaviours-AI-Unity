using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EuclideanDistance : Heuristic
{
    public override float calculateH(Node actual, Node end)
    {
        float dx = Mathf.Abs(actual.location.x - end.location.x);
        float dz = Mathf.Abs(actual.location.z - end.location.z);

        return STRAIGHT_COST * Mathf.Sqrt(dx * dx + dz * dz);
    }

    
}
