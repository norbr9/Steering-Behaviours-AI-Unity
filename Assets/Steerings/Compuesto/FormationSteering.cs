
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationSteering : BlendedSteering
{
    [SerializeField]
    private FormationManager formationManager;

    public FormationManager FormationManager { get => formationManager; set => formationManager = value; }

    public void Start()
    {
        ArriveSteeringA arr = gameObject.AddComponent<ArriveSteeringA>();
        SeparationSteering sep = gameObject.AddComponent<SeparationSteering>();
        WallAvoidanceSteering wall = gameObject.AddComponent<WallAvoidanceSteering>();

        Behaviours = new List<BlendedSteering.BehaviourAndWeight>
        {
            new BehaviourAndWeight(arr, 2),
            new BehaviourAndWeight(wall, 3)
        };

        sep.Targets = FormationManager.Personajes;
        sep.Threshold = 5;
        sep.DecayCoefficient = 3;
        sep.target = target;


        Behaviours.Add(new BehaviourAndWeight(sep, 1));
    }

    public override Steering getSteering(AgentNPC agent)
    {
        Kinematic targ = FormationManager.GetSlotByCharacter(agent);
        if (targ != null)
        {
            targ.InteriorRadius = 0;
            targ.ExteriorRadius = 1;
        }
        foreach (BehaviourAndWeight item in Behaviours)
        {
            item.behaviour.target = targ;
        }

        return base.getSteering(agent);
    }


}
