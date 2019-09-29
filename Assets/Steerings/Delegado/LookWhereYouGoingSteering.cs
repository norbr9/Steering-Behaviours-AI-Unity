using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookWhereYouGoingSteering : AlignSteering
{
    public override Steering getSteering(AgentNPC agent)
    {
        if (agent.Velocidad.magnitude == 0)
        {
            Steering steering = new Steering();
            steering.Lineal = new Vector3(0, 0, 0);
            steering.Angular = 0;
            return steering;
        }

        target.Orientacion = Mathf.Atan2(agent.Velocidad.x, agent.Velocidad.z) * Mathf.Rad2Deg;

        return base.getSteering(agent);
    }
}
