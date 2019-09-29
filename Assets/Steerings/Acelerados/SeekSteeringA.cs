using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekSteeringA : SteeringBehaviour
{
    public override Steering getSteering(AgentNPC agent)
    {
        Steering steering = new Steering();

        steering.Lineal = target.Posicion - agent.Posicion;

        steering.Lineal = steering.Lineal.normalized;

        steering.Lineal *= agent.MaxAcceleration;

        steering.Angular = 0;
        return steering;
    }
}
