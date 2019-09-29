using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityMatchingSteering : SteeringBehaviour
{
    public float timeToTarget = 0.1f;

  

    public override Steering getSteering(AgentNPC agent)
    {
        Steering steering = new Steering();

        steering.Lineal = target.Velocidad - agent.Velocidad;
        steering.Lineal /= timeToTarget;

        if (steering.Lineal.magnitude > agent.MaxAcceleration)
        {

            steering.Lineal = steering.Lineal.normalized;
            steering.Lineal *= agent.MaxAcceleration;
        }

        steering.Angular = 0;
        return steering;
    }
}
