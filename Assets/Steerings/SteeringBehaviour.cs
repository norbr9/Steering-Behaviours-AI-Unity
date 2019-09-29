using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SteeringBehaviour: MonoBehaviour
{
    public Kinematic target;
    protected float getNewOrientation(float orientation, Vector3 velocity)
    {

        if (velocity.magnitude > 0)
            return Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;
        else
            return orientation;

    }
    public abstract Steering getSteering(AgentNPC agent);

    private void Update()
    {
        
    }
}
