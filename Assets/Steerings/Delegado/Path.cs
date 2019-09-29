using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class Path
{
    [SerializeField]
    private Nodo[] nodos;

    public Path(int size)
    {
        nodos = new Nodo[size];
    }

    public Nodo[] Nodos { get => nodos; set => nodos = value; }
}