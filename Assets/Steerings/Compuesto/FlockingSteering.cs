
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingSteering : BlendedSteering
{
    public void Start()
    {
        SeparationSteering sep = new SeparationSteering();
        CohesionSteering coe = new CohesionSteering();
        AligmentSteering ali = new AligmentSteering();
        WanderSteering wan = new WanderSteering();

        GameObject[] objs = GameObject.FindGameObjectsWithTag("Target");
        List<Agent> agents = new List<Agent>();

        foreach (GameObject obj in objs)
        {
            if (!gameObject.GetComponentInParent<CilinderAgent>().Equals(obj.GetComponent<CilinderAgent>()))
                agents.Add(obj.GetComponent<CilinderAgent>());
        }

        sep.Targets = agents;
        sep.Threshold = 5;
        sep.DecayCoefficient = 3;
        sep.target = target;

        coe.Targets = agents;
        coe.Threshold = 5;
        coe.target = target;

        ali.Targets = agents;
        ali.Threshold = 5;
        ali.target = target;

        wan.target = target;

        Behaviours = new List<BlendedSteering.BehaviourAndWeight>();
        Behaviours.Add(new BehaviourAndWeight(sep, 2));
        Behaviours.Add(new BehaviourAndWeight(coe, 1));
        Behaviours.Add(new BehaviourAndWeight(ali, 5));
        Behaviours.Add(new BehaviourAndWeight(wan, 5));
    }
}
