using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CilinderAgent : AgentNPC
{
    public override void applySteering(Steering steering)
    {
        SetVelocidad(steering.Lineal);
        SetRotacion(steering.Angular);
        SetOrientacion();
        SetPosicion();
    }
}
