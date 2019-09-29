using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeparationSteering : SteeringBehaviour
{
    [SerializeField]
    private List<Agent> targets;

    [SerializeField]
    private float threshold = 5;

    [SerializeField]
    private float decayCoefficient = 3;

    public List<Agent> Targets { get => targets; set => targets = value; }
    public float Threshold { get => threshold; set => threshold = value; }
    public float DecayCoefficient { get => decayCoefficient; set => decayCoefficient = value; }

    public override Steering getSteering(AgentNPC agent)
    {
        Steering steering = new Steering();

        foreach (Agent t in Targets)
        {
            Vector3 direction = agent.Posicion - t.Posicion;
            float distance = direction.magnitude;

            if (distance < Threshold){
                float strength = Mathf.Min(DecayCoefficient / (distance * distance), agent.MaxAcceleration);

                direction.Normalize();
                steering.Lineal += strength * direction;
            }
        }

        return steering;
    }
}
