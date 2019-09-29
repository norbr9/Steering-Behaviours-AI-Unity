using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvadeSteering : FleeSteeringA
{
    public float maxPrediction = 1f;

    private Kinematic pursueTarget; //target sobre el que hare seek

    [SerializeField]
    private Kinematic originalTarget;

    public Kinematic OriginalTarget { get => originalTarget; set => originalTarget = value; }

    public override Steering getSteering(AgentNPC agent)
    {
        pursueTarget = new Kinematic();

        Vector3 direction = OriginalTarget.Posicion - agent.Posicion;
        float distance = direction.magnitude;

        float speed = agent.Velocidad.magnitude;

        float prediction;

        if (speed <= distance / maxPrediction)
            prediction = maxPrediction;
        else
            prediction = distance / speed;

        pursueTarget.Posicion = OriginalTarget.Posicion;
        pursueTarget.Rotacion = OriginalTarget.Rotacion;
        pursueTarget.Velocidad = OriginalTarget.Velocidad;
        pursueTarget.Orientacion = OriginalTarget.Orientacion;

        pursueTarget.Posicion += pursueTarget.Velocidad * prediction;

        base.target = pursueTarget;

        return base.getSteering(agent);
    }

}
