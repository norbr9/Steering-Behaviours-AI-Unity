using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node
{
    // Kind of map's element a node may belong to
    public enum TerrainType { Water, Grass, Pathway, Stone, HealingA,HealingB, BaseA, BaseB, Undefined }


    [SerializeField]
    public Vector3 location;

    [SerializeField]
    private TerrainType type;

    public float G { get;  set; }

    //Estimated total distance/cost.
    public float F { get { return this.G + this.H; } }

    public float H = -1;

    public NodeState State { get; set; }

    public Node ParentNode;

    public enum NodeState { Untested, Open, Closed }

   
    public RolType Rol {get;set;}
    
    public void setH(float value)
    {
        H = value;
    }

    public float getH() { return H; }

    public Node(Vector3 location, TerrainType type)
    {
        this.location = location;
        this.type = type;
        ParentNode = null;
        State = NodeState.Untested;
        G = 0;
    }


    public bool isWalkable()
    {
        return type != TerrainType.Stone && type != TerrainType.Water;
    }

    
    public TerrainType getType()
    {
        return type;
    }


    public void setType(TerrainType type)
    {
        this.type = type;
    }

    public Color getColor()
    {
        Color color;
        switch (type)
        {
            case TerrainType.Water:
                color = Color.blue;
                break;
            case TerrainType.Grass:
                color = Color.green;
                break;
            case TerrainType.Pathway:
                color = Color.grey;
                break;
            case TerrainType.Stone:
                color = Color.black;
                break;
            case TerrainType.HealingA:
                color = Color.white;
                break;
            case TerrainType.HealingB:
                color = Color.magenta;
                break;
            case TerrainType.BaseA:
                color = Color.red;
                break;

            case TerrainType.BaseB:
                color = Color.yellow;
                break;

            default:
                color = Color.white;
                break;
        }

        return color;
    }


    public void resetState(){
        ParentNode = null;
        State = NodeState.Untested;
        G = 0;
        H = -1;
    }

    // Coste que tiene ir por un camino
    // A menor coste mas se intentara ir por ese camino
    public float getCost()
    {
        if (Rol == RolType.Ligero) return getCostLigero();
        if (Rol == RolType.Pesado) return getCostPesado();
        if (Rol == RolType.Patrullador) return getCostPatrullador();

        return getCostLigero();
    }

    private float getCostLigero(){
        float cost = 10;

        switch (type)
        {
            case TerrainType.Water:
                cost = Mathf.Infinity;
                break;
            case TerrainType.Grass:
                cost = 10;
                break;
            case TerrainType.Pathway:
                cost = 3;
                break;
            case TerrainType.Stone:
                cost = Mathf.Infinity;
                break;
            case TerrainType.HealingA:
                cost = 9;
                break;
            case TerrainType.HealingB:
                cost = 9;
                break;
            case TerrainType.BaseA:
                cost = 9;
                break;

            case TerrainType.BaseB:
                cost = 9;
                break;

            default:
                cost = 10;
                break;
        }

        return cost;
    }

    private float getCostPesado(){
        float cost = 10;

        switch (type)
        {
            case TerrainType.Water:
                cost = Mathf.Infinity;
                break;
            case TerrainType.Grass:
                cost = 10;
                break;
            case TerrainType.Pathway:
                cost = 9;
                break;
            case TerrainType.Stone:
                cost = Mathf.Infinity;
                break;
            case TerrainType.HealingA:
                cost = 1;
                break;
            case TerrainType.HealingB:
                cost = 1;
                break;
            case TerrainType.BaseA:
                cost = 9;
                break;

            case TerrainType.BaseB:
                cost = 9;
                break;

            default:
                cost = 10;
                break;
        }

        return cost;

    }

    private float getCostPatrullador(){
        float cost = 10;

        switch (type)
        {
            case TerrainType.Water:
                cost = Mathf.Infinity;
                break;
            case TerrainType.Grass:
                cost = 10;
                break;
            case TerrainType.Pathway:
                cost = 5;
                break;
            case TerrainType.Stone:
                cost = Mathf.Infinity;
                break;
            case TerrainType.HealingA:
                cost = 3;
                break;
            case TerrainType.HealingB:
                cost = 3;
                break;
            case TerrainType.BaseA:
                cost = 1;
                break;

            case TerrainType.BaseB:
                cost = 1;
                break;

            default:
                cost = 10;
                break;
        }

        return cost;
    }


    // Velocidad maxima del terrono
    public float getMaxSpeed()
    {
        float cost = 2;

        switch (type)
        {
            case TerrainType.Water:
                break;
            case TerrainType.Grass:
                cost = 2;
                break;
            case TerrainType.Pathway:
                cost = 7;
                break;
            case TerrainType.Stone:
                break;
            case TerrainType.HealingA:
                cost = 6;
                break;
            case TerrainType.HealingB:
                cost = 6;
                break;
            case TerrainType.BaseA:
                cost = 5;
                break;

            case TerrainType.BaseB:
                cost = 5;
                break;

            default:
                break;
        }

        return cost;
    }


}
