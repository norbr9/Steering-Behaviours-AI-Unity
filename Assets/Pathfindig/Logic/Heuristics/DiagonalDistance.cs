using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//REF http://theory.stanford.edu/~amitp/GameProgramming/Heuristics.html

public class DiagonalDistance : Heuristic
{
    public override float calculateH(Node actual, Node end)
    {
        float dx = Mathf.Pow(Mathf.Abs(actual.location.x - end.location.x), 2);
        float dy = Mathf.Pow(Mathf.Abs(actual.location.z - end.location.z), 2);

        return STRAIGHT_COST * (dx + dy) + (STRAIGHT_COST - 2 * STRAIGHT_COST) * Mathf.Min(dx, dy);
    }
}
