using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrioritySteering : SteeringBehaviour
{

    private List<BlendedSteering> groups;

    private float epsilon = 0.1f;

    public override Steering getSteering(AgentNPC agent)
    {
        Steering steering = null;
        foreach (BlendedSteering group in groups)
        {
            steering = group.getSteering(agent);
            if (steering.Lineal.magnitude > epsilon || Mathf.Abs(steering.Angular) > epsilon)
                return steering;
        }
        return steering;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
