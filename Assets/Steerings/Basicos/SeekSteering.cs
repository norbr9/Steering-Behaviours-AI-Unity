using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekSteering : SteeringBehaviour
{
    public override Steering getSteering(AgentNPC agent)
    {
        Steering steering = new Steering();
        steering.Lineal = target.Posicion - agent.Posicion;
        steering.Lineal = steering.Lineal.normalized;

        steering.Lineal *= agent.MaxSpeed;

        agent.Orientacion = getNewOrientation(agent.Orientacion, steering.Lineal);

        steering.Angular = 0;
        return steering;
    }
}
