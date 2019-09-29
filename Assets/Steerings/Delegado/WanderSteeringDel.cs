using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderSteeringDel : FaceSteering
{
    [SerializeField]
    private float wanderOffset = 5f;
    [SerializeField]
    private float wanderRadius = 3f;

    [SerializeField]
    private float wanderRate = 20f;

    private float wanderOrientation;

    private float maxAcceleration;

    GameObject t;


    private Vector3 asVector(float orientacion)
    {
        return new Vector3(Mathf.Sin(orientacion * Mathf.Deg2Rad), 0, Mathf.Cos(orientacion * Mathf.Deg2Rad));
    }

    


    public override Steering getSteering(AgentNPC agent)
    {
        Steering steering = new Steering();

        t = new GameObject();
        t.AddComponent<Kinematic>();

        target = t.GetComponent<Kinematic>();

        wanderOrientation = Random.Range(-1f, 1f) * wanderRate;   
       

        target.Orientacion = wanderOrientation + agent.Orientacion;

        target.Posicion = agent.Posicion + wanderOffset * asVector(agent.Orientacion);
        target.Posicion += (wanderRadius * asVector(target.Orientacion));

        base.target = target;

        steering = base.getSteering(agent);
        steering.Lineal = agent.MaxAcceleration * asVector(agent.Orientacion);

        Destroy(t);

        return steering;
    }
}
