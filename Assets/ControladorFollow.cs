using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorFollow : MonoBehaviour
{
    [SerializeField]
    private List<AgentNPC> agentes = new List<AgentNPC>();

    [SerializeField]
    private AgentNPC agenteSeleccionado;

    private Dictionary<AgentNPC, Kinematic> puntosSeleccionados = new Dictionary<AgentNPC, Kinematic>();
    private Dictionary<AgentNPC, PathFinder> finders = new Dictionary<AgentNPC, PathFinder>();
    private Dictionary<AgentNPC, bool> buscando = new Dictionary<AgentNPC, bool>();

    [SerializeField]
    private GameObject terreno;

    public List<AgentNPC> Agentes { get => agentes; set => agentes = value; }

    public AgentNPC AgenteSeleccionado { get => agenteSeleccionado; set => agenteSeleccionado = value; }
    public GameObject Terreno { get => terreno; set => terreno = value; }
    public Dictionary<AgentNPC, Kinematic> PuntosSeleccionados { get => puntosSeleccionados; set => puntosSeleccionados = value; }
    public Dictionary<AgentNPC, PathFinder> Finders { get => finders; set => finders = value; }
    public Dictionary<AgentNPC, bool> Buscando { get => buscando; set => buscando = value; }

    private enum PathFinderType  { LRTA, ASTAR };

    [SerializeField]
    private PathFinderType pathFinder;

    [SerializeField]
    private PathFinder.TiposHeuristica heuristic;

    FollowPathFinderSteering fps = null;

    public void SeleccionarAgente ()
    {
        if (Input.anyKeyDown)
        {
            if (AgenteSeleccionado != null)
            {
                SteeringBehaviour st = AgenteSeleccionado.GetComponent<SteeringBehaviour>();
                if (st != null)
                    AgenteSeleccionado.GetComponent<SteeringBehaviour>().target = null;
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (Agentes.Count >= 1)
                    AgenteSeleccionado = agentes[0];
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (Agentes.Count >= 2)
                    AgenteSeleccionado = agentes[1];
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (Agentes.Count >= 3)
                    AgenteSeleccionado = agentes[2];
            }
            else if (Input.GetKeyDown(KeyCode.Space))
                AgenteSeleccionado = null;
        }
    }

    public void SeleccionarObjetivo ()
    {
        
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            Kinematic ps = new Kinematic();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Terreno.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity))
            {
                ps.Posicion = hit.point;

            }

            if (AgenteSeleccionado != null)
            {
                if (!PuntosSeleccionados.ContainsKey(AgenteSeleccionado))
                    PuntosSeleccionados.Add(AgenteSeleccionado, ps);
                if (!Buscando.ContainsKey(AgenteSeleccionado))
                    Buscando.Add(AgenteSeleccionado, false);

                if (!Finders.ContainsKey(AgenteSeleccionado))
                {
                    if (pathFinder == PathFinderType.LRTA)
                        Finders.Add(AgenteSeleccionado,gameObject.AddComponent<LRTAstar>());
                    else
                        Finders.Add(AgenteSeleccionado, gameObject.AddComponent<Astar>());
                }

                PuntosSeleccionados[AgenteSeleccionado] = ps;
                Finders[AgenteSeleccionado].initPathFinder(heuristic, AgenteSeleccionado.Grid, AgenteSeleccionado.Posicion, PuntosSeleccionados[AgenteSeleccionado].Posicion);

                if (Finders[AgenteSeleccionado].endNode.isWalkable())
                {
                    if (Buscando[AgenteSeleccionado])
                    {
                        Destroy(fps);
                        Finders[AgenteSeleccionado].Resetear();
                      
                    }
                    else
                    {
                        Buscando[AgenteSeleccionado] = true;
                    }

                    Finders[AgenteSeleccionado].startFinding();
                }
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        SeleccionarAgente();
        SeleccionarObjetivo();

        foreach (AgentNPC agent in Finders.Keys)
        {
            if (Buscando[agent])
            {
                if (agent.GetComponent<FollowPathFinderSteering>() == null)
                {
                    fps = agent.gameObject.AddComponent<FollowPathFinderSteering>();
                    fps.Path = Finders[agent].path;

                    agent.ReloadSteerings();
                }
                else
                {
                    fps = agent.gameObject.GetComponent<FollowPathFinderSteering>();

                    if (Finders[agent].fin && (fps.NodoActual == fps.Path.Count))
                    {
                        Finders[agent].Resetear();

                        Destroy(fps);
                        Buscando[agent] = false;
                    }

                }
            }
        }
    }
}
