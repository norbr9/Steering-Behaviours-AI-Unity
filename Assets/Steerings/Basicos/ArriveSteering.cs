using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArriveSteering : SteeringBehaviour
{

     /**
     * At each step the character tries to reach its target in
     * this duration. This means it moves more slowly when nearby.
     */
    private float timeToTarget = 2f;
    private float radio = 1f;

   

    public override Steering getSteering(AgentNPC agent)
    {
        Steering steering = new Steering();
        steering.Lineal = target.Posicion - agent.Posicion;

        if (steering.Lineal.magnitude < radio)
        {
            steering.Lineal = new Vector3(0, 0, 0);
            steering.Angular = 0;
            return steering;
        }

        steering.Lineal /= timeToTarget;

        if (steering.Lineal.magnitude > agent.MaxSpeed)
        {
            steering.Lineal = steering.Lineal.normalized;
            steering.Lineal *= agent.MaxSpeed;
        }

        agent.Orientacion = getNewOrientation(agent.Orientacion, steering.Lineal);

        steering.Angular = 0;
        return steering;
    }

}
