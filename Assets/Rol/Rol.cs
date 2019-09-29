using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum RolType{ Ligero, Pesado, Patrullador}

public abstract class Rol
{
    public RolType rolType { get; set; }

    public float Velocidad { get; set; }
    public float Aceleracion { get; set; }
}
