using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoidanceSteering : SteeringBehaviour
{
    [SerializeField]
    private List<Agent> targets;

    private float radius = 4;

    public override Steering getSteering(AgentNPC agent)
    {
        Steering steering = new Steering();

        float shortestTime = Mathf.Infinity;

        Agent firstTarget = null;
        float firstMinSeparation = 0;
        float firstDistance = 0;
        Vector3 firstRelativePos = Vector3.zero;
        Vector3 firstRelativeVel = Vector3.zero;

        foreach (Agent target in targets)
        {
            Vector3 relativePos = target.Posicion - agent.Posicion;
            Vector3 relativeVel = target.Velocidad - agent.Velocidad;
            float relativeSpeed = relativeVel.magnitude;
            float timeToCollision = (new Vector3(relativePos.x + relativeVel.x, 0, relativePos.z + relativeVel.z).magnitude / (relativeSpeed * relativeSpeed));

            float distance = relativePos.magnitude;
            float minSeparation = distance - relativeSpeed * timeToCollision;

            if (minSeparation > 2 * radius)
                continue;

            if (timeToCollision > 0 && timeToCollision < shortestTime)
            {
                shortestTime = timeToCollision;
                firstTarget = target;
                firstMinSeparation = minSeparation;
                firstDistance = distance;
                firstRelativePos = relativePos;
                firstRelativeVel = relativeVel;
            }
        }

        if (firstTarget == null)
            return steering;

        if (firstMinSeparation <= 0 || firstDistance < 2 * radius)
            firstRelativePos = firstTarget.Posicion - agent.Posicion;
        else
            firstRelativePos += firstRelativeVel * shortestTime;

        firstRelativePos.Normalize();
        steering.Lineal = firstRelativePos * agent.MaxAcceleration;

        return steering;
    }
}
