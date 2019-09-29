using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeSteeringA : SteeringBehaviour
{
    
    public override Steering getSteering(AgentNPC agent)
    {
        Steering steering = new Steering();

        steering.Lineal = agent.Posicion - target.Posicion;

        steering.Lineal = steering.Lineal.normalized;

        steering.Lineal *= agent.MaxAcceleration;

        steering.Angular = 0;
        return steering;
    }
}
