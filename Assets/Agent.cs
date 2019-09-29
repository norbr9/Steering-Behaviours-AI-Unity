using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : Kinematic
{

    // Rol del agente
    private Rol rol;

    [Header("Rol")]
    [SerializeField]
    private RolType tipoR;

    // Grid del Terreno, para calcular las velocidades en cada tipo de terreno
    private Grid grid;

    // Terreno donde está el agente en cada momento
    Node.TerrainType actual;

    public RolType TipoR { get => tipoR; set => tipoR = value; }
    public Grid Grid { get => grid; set => grid = value; }
    public Node.TerrainType TipoTerrenoActual { get => actual; set => actual = value; }

    [Header("Agent Gizmos")]
    [SerializeField]
    [Tooltip("Set true to draw interior radious.")]
    private bool drawInteriorRadius = false;

    [SerializeField]
    [Tooltip("Set true to draw exterior radious.")]
    private bool drawExteriorRadius = false;

    public new void Start()
    {
        base.Start();
        Grid = gameObject.AddComponent<Grid>();

        switch (TipoR)
        {
            case RolType.Ligero:
                rol = new Ligero();
                break;
            case RolType.Pesado:
                rol = new Pesado();
                break;
            case RolType.Patrullador:
                rol = new Patrullador();
                break;
            default:
                break;
        }

        TipoTerrenoActual = Node.TerrainType.Undefined;
    }

    public new void Update()
    {
        base.Update();

        updateSpeedTerrain();
    }

    public void OnDrawGizmos()
    {
        if (drawInteriorRadius)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, InteriorRadius);
        }
        if (drawExteriorRadius)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, ExteriorRadius);
        }
    }



    private void updateSpeedTerrain()
    {
        Vector3 position = Grid.GetNearestPointOnGrid(transform.position);
        Node actualNode = Grid.grid[(int)position.x][(int)position.z];

        // Actualizar la velocidad y en que terreno estoy
        if (TipoTerrenoActual != actualNode.getType()) {
            MaxSpeed = rol.Velocidad;
            MaxSpeed += actualNode.getMaxSpeed();
            TipoTerrenoActual = actualNode.getType();
        }

    }

}
