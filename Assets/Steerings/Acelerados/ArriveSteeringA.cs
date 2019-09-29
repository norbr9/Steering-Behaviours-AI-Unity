using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArriveSteeringA : SteeringBehaviour
{
    private float timeToTarget = 0.1f;
    private float targetSpeed;
    private Vector3 targetVelocity;

  

    public override Steering getSteering(AgentNPC agent)
    {
        Steering steering = new Steering();

        Vector3 direction = target.Posicion - agent.Posicion;
        float distance = direction.magnitude;

        if (distance < target.InteriorRadius)
        {
            steering.Lineal = new Vector3(0, 0, 0);
            steering.Angular = 0;
            return steering;
        }

        if (distance > target.ExteriorRadius)
            targetSpeed = agent.MaxSpeed;
        else
            targetSpeed = agent.MaxSpeed * distance / target.ExteriorRadius;

        targetVelocity = direction;
        targetVelocity = targetVelocity.normalized;
        targetVelocity *= targetSpeed;

        steering.Lineal = targetVelocity - agent.Velocidad;
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
