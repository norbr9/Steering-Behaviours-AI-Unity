using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CohesionSteering : SeekSteeringA
{
    [SerializeField]
    private List<Agent> targets;

    [SerializeField]
    private float threshold = 30;

    public List<Agent> Targets { get => targets; set => targets = value; }
    public float Threshold { get => threshold; set => threshold = value; }

    public override Steering getSteering(AgentNPC agent)
    {
        Steering steering = new Steering();

        float count = 0;
        Vector3 centerOfMass = Vector3.zero;

        foreach (Agent t in Targets)
        {
            Vector3 direction = t.Posicion - agent.Posicion;
            float distance = Mathf.Abs(direction.magnitude);

            if (distance > Threshold)
                continue;

            centerOfMass += t.Posicion;

            count++;
        }

        if (count == 0)
            return steering;


        centerOfMass = new Vector3(centerOfMass.x/count, centerOfMass.y, centerOfMass.z/count);

        if (target == null)
            target = new Kinematic();

        target.Posicion = centerOfMass;

        return base.getSteering(agent);
    }
}
