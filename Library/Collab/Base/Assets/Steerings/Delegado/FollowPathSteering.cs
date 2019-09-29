using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPathSteering : SeekSteeringA
{
    private enum FinalStep { Reverse, Init };

    // Camino a seguir
    [SerializeField]
    public Path path;

    private int pathDir = 1;
    // Posicion del path en el que se encuentra
    [SerializeField]
    private int nodoActual = 0;

    private Nodo currentParam;

    [SerializeField]
    FinalStep decisionFinal;

    // Radio de proximidad de un punto
    [SerializeField]
    private int radioProx;

    // Target sobre el que se hará el seek
    private GameObject targetPath;

    private Vector3 originalTarget;

    public override Steering getSteering(AgentNPC agent)
    {
        radioProx = path.Nodos[nodoActual].Radius;
        target = path.Nodos[nodoActual].GetComponentInParent<BasicAtrs>();
        if (distancia(agent.Posicion, target.Posicion) <= radioProx)
            nodoActual += pathDir;

        if (nodoActual >= path.Nodos.Length || nodoActual < 0)
        {
            switch (decisionFinal)
            {
                case FinalStep.Reverse:
                    pathDir = -pathDir;
                    nodoActual += pathDir;
                    break;
                case FinalStep.Init:
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
        path = new Path(objects.Length);
        foreach (GameObject obj in objects)
        {
            path.Nodos[obj.GetComponent<Nodo>().Index] = obj.GetComponent<Nodo>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private float distancia(Vector3 a, Vector3 b)
    {
        return Mathf.Sqrt((a.x - b.x) * (a.x - b.x) + (a.z - b.z) * (a.z - b.z));
    }
}
