using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignSteering : SteeringBehaviour
{
    private float timeToTarget = 0.1f;

    public override Steering getSteering(AgentNPC agent)
    {
        Steering steering = new Steering();

        float rotation = target.Orientacion - agent.Orientacion;
        
        rotation = MapToRange(rotation);

        float rotationSize = Mathf.Abs(rotation);

        if (rotationSize < target.InteriorRadius)
        {
            steering.Lineal = new Vector3(0, 0, 0);
            steering.Angular = 0;
            return steering;
        }

        float targetRotation;

        if (rotationSize > target.ExteriorRadius)
            targetRotation = agent.MaxRotation;
        else
            targetRotation = agent.MaxRotation * rotationSize / target.ExteriorRadius;

        targetRotation *= rotation / rotationSize;

        steering.Angular = targetRotation - agent.Rotacion;
        steering.Angular /= timeToTarget;

        float angularAceleration = Mathf.Abs(steering.Angular);


        if (angularAceleration > agent.MaxAngularAceleration)
        {
            steering.Angular /= angularAceleration;
            steering.Angular *= agent.MaxAngularAceleration;
        }
        steering.Lineal = new Vector3(0,0,0);
        
        return steering;
    }

    private float MapToRange(float rotation)
    {
        if (rotation > 180)
        {
            return MapToRange(rotation - 360);
        }
        if (rotation < -180)
        {
            return MapToRange(rotation + 360);
        }

        return rotation;
    }
}