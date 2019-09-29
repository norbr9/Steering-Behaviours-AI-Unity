using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlador : MonoBehaviour
{
    [SerializeField]
    private List<AgentNPC> agentes = new List<AgentNPC>();

    [SerializeField]
    private AgentNPC agenteSeleccionado;

    private Kinematic puntoSeleccionado;

    [SerializeField]
    private GameObject terreno;

    public List<AgentNPC> Agentes { get => agentes; set => agentes = value; }

    public AgentNPC AgenteSeleccionado { get => agenteSeleccionado; set => agenteSeleccionado = value; }
    public GameObject Terreno { get => terreno; set => terreno = value; }

    public void SeleccionarAgente ()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                if (Agentes.Count >= 1)
                    AgenteSeleccionado = agentes[0];
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (Agentes.Count >= 2)
                    AgenteSeleccionado = agentes[1];
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (Agentes.Count >= 3)
                    AgenteSeleccionado = agentes[2];
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (Agentes.Count >= 4)
                    AgenteSeleccionado = agentes[3];
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (Agentes.Count >= 5)
                    AgenteSeleccionado = agentes[4];
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (terreno.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity))
            {
                puntoSeleccionado.Posicion = hit.point;

            }

            if (AgenteSeleccionado != null)
            {
                if (AgenteSeleccionado.GetComponent<ArriveSteeringA>() == null)
                {
                    ArriveSteeringA st = AgenteSeleccionado.gameObject.AddComponent<ArriveSteeringA>();
                    st.target = puntoSeleccionado;
                }
                else
                {
                    SteeringBehaviour st = AgenteSeleccionado.GetComponent<ArriveSteeringA>();
                    st.target = puntoSeleccionado;
                    AgenteSeleccionado.ReloadSteerings();
                }
            }
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        puntoSeleccionado = gameObject.AddComponent<Kinematic>();
    }

    // Update is called once per frame
    void Update()
    {
        SeleccionarAgente();
        SeleccionarObjetivo();
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        if (puntoSeleccionado != null)
        {
            Gizmos.DrawSphere(puntoSeleccionado.Posicion, puntoSeleccionado.ExteriorRadius/2);
        }
    }
}
