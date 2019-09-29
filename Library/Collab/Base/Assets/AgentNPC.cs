using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AgentNPC : Agent, SteeringApplier
{
    
       
    List<SteeringBehaviour> listSteerings;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        listSteerings = new List<SteeringBehaviour>();

        ReloadSteerings();

    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        if (listSteerings.Count == 0) return;



        foreach (SteeringBehaviour sB in listSteerings)
        {
            Steering st = sB.getSteering(this);

            applySteering(st);

        }
    }

    public void ReloadSteerings()
    {



        listSteerings.Clear();


        // Si estoy controlado con un arbitro solo exsite blendedSteering
        Arbitro arbitro = gameObject.GetComponent<Arbitro>();
        if (arbitro != null && arbitro.isActiveAndEnabled)
        {
            listSteerings.Add(gameObject.GetComponent<BlendedSteering>());
           
        }
        else
        {
            foreach (BasicSteering steering in GetComponents<BasicSteering>())
            {
                if (steering.isActiveAndEnabled)
                    listSteerings.Add(steering);
            }
        }


       

    }


    public abstract bool applySteering(Steering steering);


}
