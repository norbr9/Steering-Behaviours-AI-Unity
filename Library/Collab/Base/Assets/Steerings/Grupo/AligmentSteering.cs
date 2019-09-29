using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AligmentSteering : BasicSteering
{
    [SerializeField]
    private List<Agent> targets;

    [SerializeField]
    private float threshold = 15;

    [SerializeField]
    private float decayCoefficient = 3;

    public List<Agent> Targets { get => targets; set => targets = value; }
    public float Threshold { get => threshold; set => threshold = value; }
    public float DecayCoefficient { get => decayCoefficient; set => decayCoefficient = value; }

    public override Steering getSteering(AgentNPC agent)
    {
        Steering steering = new Steering();

        float count = 0;
        Vector3 Heading = Vector3.zero;

        foreach (Agent t in Targets)
        {
            Vector3 direction = agent.Posicion - t.Posicion;
            float distance = direction.magnitude;

            if (distance > Threshold)
                continue;

            Heading += t.Velocidad;
            count++;
        }

        if (count > 0)
        {
            Heading /= count;
            Heading -= agent.Velocidad;
        }

        steering.Lineal = Heading;

        return steering;
    }

    /*public void Start()
    {
        Targets = new List<Agent>();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Target"))
        {
            Targets.Add(obj.GetComponent<Agent>());
        }
    }*/
}
