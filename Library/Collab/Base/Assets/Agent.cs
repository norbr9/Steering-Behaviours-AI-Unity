using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : Kinematic
{

    // Rol del agente
    [SerializeField]
    public Rol rol;

    [Header("Agent Gizmos")]
    [SerializeField]
    [Tooltip("Set true to draw interior radious.")]
    private bool drawInteriorRadius = false;

    [SerializeField]
    [Tooltip("Set true to draw exterior radious.")]
    private bool drawExteriorRadius = false;

    [SerializeField]
    private RolType tipoR;

    // Grid del Terreno, para calcular las velocidades en cada tipo de terreno
    public Grid grid;

    // Terreno donde esta el agente en cada momento
    Node.TerrainType actual;


    public RolType TipoR { get => tipoR; set => tipoR = value; }

    public new void Start()
    {
        base.Start();
        grid = gameObject.AddComponent<Grid>();

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

        // maxSpeed = rol.Velocidad;
        //maxAcceleration = rol.Aceleracion;

        actual = Node.TerrainType.Undefined;

    }

    public new void Update()
    {
        base.Update();

        //Posicion = transform.localPosition;
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

     //   Gizmos.color = Color.cyan;
     //   Gizmos.DrawLine(transform.position, transform.position + Velocidad);


       // Gizmos.color = Color.black;
       // Gizmos.DrawLine(transform.position, transform.position + aceleration);

      /*  if (GetComponent<LandAgentNPC>() != null)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.localPosition, GetComponent<LandAgentNPC>().Range);
        }*/

    }



    private void updateSpeedTerrain()
    {
        Vector3 position = grid.GetNearestPointOnGrid(transform.position);
        Node actualNode = grid.grid[(int)position.x][(int)position.z];

        // Actualizar la velocidad y en que terreno estoy
        if (actual != actualNode.getType()) {
            maxSpeed = rol.Velocidad;
            maxSpeed += actualNode.getMaxSpeed();
            actual = actualNode.getType();
        }

    }

}
