using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPathSteering : SeekSteeringA
{
    private enum FinalStep { Reverse, Init };

    // Camino a seguir
    [SerializeField]
    private Path path;

    private int pathDir = 1;
    // Posicion del path en el que se encuentra
    [SerializeField]
    private int nodoActual = 0;

    [SerializeField]
    private FinalStep decisionFinal;

    // Radio de proximidad de un punto
    [SerializeField]
    private int radioProx;

    private Vector3 originalTarget;

    private FinalStep DecisionFinal { get => decisionFinal; set => decisionFinal = value; }
    public Path Path { get => path; set => path = value; }

    public override Steering getSteering(AgentNPC agent)
    {
        radioProx = Path.Nodos[nodoActual].Radius;
        target = Path.Nodos[nodoActual].GetComponentInParent<Kinematic>();

        if (Vector3.Distance(agent.Posicion, target.Posicion) <= radioProx)
            nodoActual += pathDir;

        if (nodoActual >= Path.Nodos.Length || nodoActual < 0)
        {
            switch (DecisionFinal)
            {
                case FinalStep.Reverse:
                    pathDir = -pathDir;
                    nodoActual += pathDir;
                    break;
                case FinalStep.Init:
                    pathDir = 1;
                    nodoActual = 0;
                    break;
                default:
                    break;
            }
        }
        Steering steering = new Steering();
        if (target == null)
        {
            steering.Lineal = Vector3.zero;
            steering.Angular = 0;
            return steering;
        }
        return base.getSteering(agent);
    }

    // Start is called before the first frame update
    void Awake()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Path");
        Path = new Path(objects.Length);
        foreach (GameObject obj in objects)
        {
            Path.Nodos[obj.GetComponent<Nodo>().Index] = obj.GetComponent<Nodo>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
