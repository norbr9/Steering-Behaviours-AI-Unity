using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PathFinder : MonoBehaviour 
{
    public enum TiposHeuristica { Manhattan, Chebychev, Euclidea };

    [SerializeField]
    private TiposHeuristica tipoHeuristica;

    public Grid grid;

    [SerializeField]
    public Node startNode { get; set; }

    [SerializeField]
    public Node endNode { get; set; }
  

    public List<Vector3> path = new List<Vector3>();
    public Heuristic heuristic;

    // Calcular el path con una corutina
    public abstract IEnumerator FindPath();

    private List<GameObject> objetos = new List<GameObject>();

    // Acceso al path durante la corutina
    protected int index = 0;
    protected int availableIndex = -1;

    public bool fin = false;

    protected bool diagonal(Node child, Node parent)
    {
        bool diag = false;
        if (parent.location.x + 1 == child.location.x
        && parent.location.z + 1 == child.location.z)
            diag = true;
        else if (parent.location.x - 1 == child.location.x
        && parent.location.z + 1 == child.location.z)
            diag = true;
        else if (parent.location.x + 1 == child.location.x
        && parent.location.z - 1 == child.location.z)
            diag = true;
        else if (parent.location.x - 1 == child.location.x
        && parent.location.z - 1 == child.location.z)
            diag = true;

        return diag;
    }


    public void initPathFinder(TiposHeuristica h, Grid grid, Vector3 start, Vector3 end)
    {
        this.grid = grid;
        startNode = grid.locationToNode(start);
        endNode = grid.locationToNode(end);

        tipoHeuristica = h;
        switch (tipoHeuristica)
        {
            case TiposHeuristica.Manhattan:
                heuristic = new ManhattanDistance();
                break;
            case TiposHeuristica.Chebychev:
                heuristic = new DiagonalDistance();
                break;
            case TiposHeuristica.Euclidea:
                heuristic = new EuclideanDistance();
                break;
            
            default:
                break;
        }
    }

    public void Start()
    {

    }

    public void setStartNode(Node nodo)
    {
        startNode = nodo;
    }
    public void setFinNode(Node nodo)
    {
        endNode = nodo;
    }


    public void Update()
    {
        if (index <= availableIndex)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Vector3 ad = new Vector3(path[index].x - Grid.offset, 0, path[index].z - Grid.offset);
            sphere.transform.localScale *= 0.5f;
            sphere.transform.position = ad;
            objetos.Add(sphere);
            index++;
        }
    }

    public void Resetear()
    {
        StopCoroutine("FindPath");
        grid.resetStateGrid();
        path.Clear();
        index = 0;
        fin = false;
        availableIndex = -1;
        foreach (GameObject obj in objetos)
        {
            Destroy(obj);
        }
        objetos.Clear();

    }

    public void startFinding()
    {
        StartCoroutine("FindPath");
    }
}
