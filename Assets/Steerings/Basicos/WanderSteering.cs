using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderSteering : SteeringBehaviour
{

    private float ratio = 2;

  

    private Vector3 asVector(float orientacion)
    {
        return new Vector3(Mathf.Sin(orientacion * Mathf.Deg2Rad), 0, Mathf.Cos(orientacion * Mathf.Deg2Rad));
    }

    public override Steering getSteering(AgentNPC agent)
    {
        Steering steering = new Steering();
        
        steering.Lineal = agent.MaxSpeed * asVector(agent.Orientacion);


        steering.Angular = Random.Range(-1f, 1f) * agent.MaxRotation;
        
        
        return steering;
    }
}
