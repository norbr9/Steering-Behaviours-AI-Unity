using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceSteering : AlignSteering
{
    public override Steering getSteering(AgentNPC agent)
    {
        Vector3 direction = target.Posicion - agent.Posicion;

        if (direction.magnitude == 0)
        {
            Steering steering = new Steering();
            steering.Lineal = new Vector3(0, 0, 0);
            steering.Angular = 0;
            return steering;
        }

        target.Orientacion = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Steering st = base.getSteering(agent);

        return st;
    }
}
