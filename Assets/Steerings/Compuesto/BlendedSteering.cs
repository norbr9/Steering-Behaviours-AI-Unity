
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendedSteering : SteeringBehaviour
{
     public class BehaviourAndWeight
    {
        public SteeringBehaviour behaviour;
        public float weight;

        public BehaviourAndWeight(SteeringBehaviour steering, float peso)
        {
            behaviour = steering;
            weight = peso;
        }
    }

    [SerializeField]
    public List<BehaviourAndWeight> Behaviours { get; set; }

    public override Steering getSteering(AgentNPC agent)
    {
        Steering steering = new Steering();

        foreach (BehaviourAndWeight behaviour in Behaviours)
        {
            Steering steer = behaviour.behaviour.getSteering(agent);
            steering.Lineal += steer.Lineal * behaviour.weight;
            steering.Angular += steer.Angular * behaviour.weight;
        }

         steering.Lineal = Vector3.ClampMagnitude(steering.Lineal, agent.MaxAcceleration);

        return steering;
    }

    private void Awake()
    {
        Behaviours = new List<BehaviourAndWeight>();
    }
}
