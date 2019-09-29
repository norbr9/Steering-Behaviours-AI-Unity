using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pesado : Rol
{
    public Pesado()
    {
        rolType = RolType.Pesado;
        Velocidad = 2;
        Aceleracion = 100;
    }
}
