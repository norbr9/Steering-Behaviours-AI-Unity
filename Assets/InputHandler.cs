using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputHandler : MonoBehaviour
{

    private RolType rol;

    [SerializeField]
    public bool rolSteering = true;

    [SerializeField]
    public bool reload = true;

    public enum TipoSteering { Seek, Arrive, WallAvoidance, Wander };
    [SerializeField]
    private List<TipoSteering> steeringsSeleccionados;

    private List<TipoSteering> SteeringsSeleccionados { get => steeringsSeleccionados; set => steeringsSeleccionados = value; }

    [SerializeField]
    private List<float> pesoSelecionado;

    private Agent target;

    [SerializeField]
    private List<float> PesoSelecionado { get => pesoSelecionado; set => pesoSelecionado = value; }
    public Agent Target { get => target; set => target = value; }

    public void Start()
    {
        Agent myself = gameObject.GetComponent<Agent>();
        rol = myself.TipoR;
    }


    public Dictionary<SteeringBehaviour, float> getCustomInput()
    {
        Dictionary<SteeringBehaviour, float> map = new Dictionary<SteeringBehaviour, float>();
        int i = 0;
        foreach (var t in SteeringsSeleccionados)
        {
            switch (t)
            {
                case TipoSteering.Seek:
                      if (!map.ContainsKey(new SeekSteeringA()))
                        map.Add(new SeekSteeringA(), pesoSelecionado[i]);

                    break;
                case TipoSteering.Arrive:

                    if (!map.ContainsKey(new ArriveSteeringA()))
                        map.Add(new ArriveSteeringA(), pesoSelecionado[i]);

                    break;
                case TipoSteering.WallAvoidance:
                    map.Add(gameObject.AddComponent<WallAvoidanceSteering>(), pesoSelecionado[i]);
                   
                    break;
                case TipoSteering.Wander:
                    map.Add(gameObject.AddComponent<WanderSteeringDel>(), pesoSelecionado[i]);
                    break;


                default:
                    break;
            }
            i++;

            
        }
        return map;
    }

    public Dictionary<SteeringBehaviour, float> getInput()
    {
        return rolSteering ? getRolInput() : getCustomInput();
    }

    // Devuelve un mapa con steering, peso
    public Dictionary<SteeringBehaviour, float> getRolInput()
    {
        Dictionary<SteeringBehaviour, float> map = null;
        switch (rol)
        {
            case (RolType.Ligero):
                map = getInputLigero();
                break;

            case (RolType.Patrullador):
                map = getInputPatrullador();
                break;

            case (RolType.Pesado):
                map = getInputPesado();
                break;

            default:
                break;

        }

        return map;

    }



    private Dictionary<SteeringBehaviour, float> getInputLigero()
    {
        Dictionary<SteeringBehaviour, float> map = new Dictionary<SteeringBehaviour, float>();
        WanderSteeringDel wander = gameObject.AddComponent<WanderSteeringDel>();
        float wanderWeigth = 1;

        map.Add(wander, wanderWeigth);

        WallAvoidanceSteering wallAvoid = gameObject.AddComponent<WallAvoidanceSteering>();
        map.Add(wallAvoid, 5);

        return map;
    }

    private Dictionary<SteeringBehaviour, float> getInputPesado()
    {
        Dictionary<SteeringBehaviour, float> map = new Dictionary<SteeringBehaviour, float>();
        WanderSteeringDel wander = gameObject.AddComponent<WanderSteeringDel>();
        float wanderWeigth = 1;

        map.Add(wander, wanderWeigth);

        WallAvoidanceSteering wallAvoid = gameObject.AddComponent<WallAvoidanceSteering>();
        map.Add(wallAvoid, 5);

        return map;
    }

    private Dictionary<SteeringBehaviour, float> getInputPatrullador()
    {
        Dictionary<SteeringBehaviour, float> map = new Dictionary<SteeringBehaviour, float>();
        WanderSteeringDel wander = gameObject.AddComponent<WanderSteeringDel>();
        float wanderWeigth = 1;

        map.Add(wander, wanderWeigth);

        WallAvoidanceSteering wallAvoid = gameObject.AddComponent<WallAvoidanceSteering>();
        map.Add(wallAvoid, 5);

        return map;
    }
}
