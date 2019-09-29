
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingLeaderSteering : BlendedSteering
{
    [SerializeField]
    private int distanceLeader;

    private Vector3 behind;
    private Kinematic newTarget;

    public void Start()
    {
        Vector3 tv = target.Velocidad * -1;
        tv = tv.normalized * distanceLeader;
        behind = target.Posicion + tv;

        newTarget = new Kinematic();
        newTarget.Posicion = behind;

        SeparationSteering sep = new SeparationSteering();
        ArriveSteeringA arr = new ArriveSteeringA();
        EvadeSteering ev = new EvadeSteering();

        GameObject[] objs = GameObject.FindGameObjectsWithTag("Target");
        List<Agent> agents = new List<Agent>();

        foreach (GameObject obj in objs)
        {
            if (!gameObject.GetComponentInParent<CilinderAgent>().Equals(obj.GetComponent<CilinderAgent>()))
                agents.Add(obj.GetComponent<CilinderAgent>());
        }
        
        sep.Targets = agents;
        sep.Threshold = 6;
        sep.DecayCoefficient = 3;
        sep.target = target;

        arr.target = newTarget;
        ev.OriginalTarget = target;

        Behaviours = new List<BlendedSteering.BehaviourAndWeight>
        {
            new BehaviourAndWeight(arr, 3),
            new BehaviourAndWeight(sep, 4),
            new BehaviourAndWeight(ev, 1)
        };
    }

    private void Update()
    {
        if (target.Velocidad.magnitude != 0)
        {
            Vector3 tv = target.Velocidad * -1;
            tv = tv.normalized * distanceLeader;
            behind = target.Posicion + tv;
            newTarget.Posicion = behind;
        }

    }
    public void OnDrawGizmos()
    {
        if (behind != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(behind, 1);
        }
    }
}
