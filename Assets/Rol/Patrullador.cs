using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrullador : Rol
{
    public Patrullador()
    {
        rolType = RolType.Patrullador;
        Velocidad = 5;
        Aceleracion = 100;
    }
}
