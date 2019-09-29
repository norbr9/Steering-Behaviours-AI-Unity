using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPathFinderSteering : SeekSteeringA
{


    private List<Vector3> path;

    private int pathDir = 1;
    // Posicion del path en el que se encuentra
    [SerializeField]
    private int nodoActual = 0;

    // Radio de proximidad de un punto
    [SerializeField]
    private int radioProx = 1;

    public List<Vector3> Path { get => path; set => path = value; }
    public int RadioProx { get => radioProx; set => radioProx = value; }
    public int PathDir { get => pathDir; set => pathDir = value; }
    public int NodoActual { get => nodoActual; set => nodoActual = value; }

    public override Steering getSteering(AgentNPC agent)
    {
        Vector3 posNodo;
        if (NodoActual < Path.Count)
        {
            posNodo = Path[NodoActual];
            if (distancia(agent.Posicion, posNodo) <= RadioProx)
                NodoActual += PathDir;

            target = new Kinematic();


            target.Posicion = posNodo;
            return base.getSteering(agent);
        }

        Steering steering = new Steering()
        {
            Lineal = Vector3.zero,
            Angular = 0
        };

        return steering;
    }

    // Start is called before the first frame update
    void Awake()
    {
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
