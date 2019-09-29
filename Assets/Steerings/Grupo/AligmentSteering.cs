using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AligmentSteering : SteeringBehaviour
{
    [SerializeField]
    private List<Agent> targets;

    [SerializeField]
    private float threshold = 15;

    public List<Agent> Targets { get => targets; set => targets = value; }
    public float Threshold { get => threshold; set => threshold = value; }

    public override Steering getSteering(AgentNPC agent)
    {
        Steering steering = new Steering();

        float count = 0;
        float Heading = 0;

        foreach (Agent t in Targets)
        {
            Vector3 direction = agent.Posicion - t.Posicion;
            float distance = direction.magnitude;

            if (distance > Threshold)
                continue;

            Heading += t.Orientacion;
            count++;
        }

        if (count > 0)
        {
            Heading /= count;
            Heading -= agent.Orientacion;
        }

        steering.Angular = Heading;

        return steering;
    }
}
