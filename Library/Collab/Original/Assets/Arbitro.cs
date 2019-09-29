using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arbitro : MonoBehaviour
{
    // Steering a usar
    private BlendedSteering blendedSteering;

    // Obtener el input
    private InputHandler inputHandler;

   


    // Start is called before the first frame update
    void Start()
    {
        inputHandler = gameObject.AddComponent<InputHandler>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (inputHandler.reload){
            destroySteerings();

            gameObject.AddComponent<BlendedSteering>();
            blendedSteering = gameObject.GetComponent<BlendedSteering>();
            
            gameObject.GetComponent<AgentNPC>().ReloadSteerings();

            updateSteering();
            inputHandler.reload = false;

            
        }
        
        
    }

    private void updateSteering()
    {
        // Obtener lista de BehaviourAndWeight del input
        List<BlendedSteering.BehaviourAndWeight> behaviourAndWeights = new List<BlendedSteering.BehaviourAndWeight>();
      

        foreach (var pair in inputHandler.getInput())
        {
            SteeringBehaviour steering = pair.Key;
            float weigth = pair.Value;


            behaviourAndWeights.Add(new BlendedSteering.BehaviourAndWeight(steering, weigth));

        }

        blendedSteering.Behaviours = behaviourAndWeights;

    }


    private void destroySteerings()
    {
        foreach (CollisionDetector c in gameObject.GetComponents<CollisionDetector>())
            Destroy(c);

    /*    foreach (Collision c in gameObject.GetComponents<Collision>())
            Destroy(c);*/

        foreach (SteeringBehaviour st in gameObject.GetComponents<SteeringBehaviour>())
            Destroy(st);
    }
  

}
