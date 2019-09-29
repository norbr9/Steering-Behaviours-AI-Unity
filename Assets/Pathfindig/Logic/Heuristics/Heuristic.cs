using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Heuristic
{
    public readonly float DIAGONAL_COST = Mathf.Sqrt(2);

    public readonly float STRAIGHT_COST = 1;

    public abstract float calculateH(Node actual, Node end);

}
