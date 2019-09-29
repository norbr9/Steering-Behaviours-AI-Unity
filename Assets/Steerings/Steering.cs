using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering
{
    private Vector3 lineal;
    private float angular;

    public Steering()
    {
        lineal = Vector3.zero;
        angular = 0;
    }

    public Vector3 Lineal { get => lineal; set => lineal = value; }
    public float Angular { get => angular; set => angular = value; }

    public void clear(){
        Lineal = Vector3.zero;
        Angular = 0;
    }
}
