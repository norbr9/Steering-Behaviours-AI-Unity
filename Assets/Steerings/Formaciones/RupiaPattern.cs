using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RupiaPattern : FormationPattern
{
    public RupiaPattern(Agent lider) : base(lider)
    {
        MaxSlotNumber = 5;

        Posiciones = new Kinematic[MaxSlotNumber];

        Posiciones[0] = new Kinematic();
        Posiciones[0].Posicion = new Vector3(-1f, 0f, -1f);
        Posiciones[0].Orientacion = 0;

        Posiciones[1] = new Kinematic();
        Posiciones[1].Posicion = new Vector3(1f, 0f, -1f);
        Posiciones[1].Orientacion = 0;

        Posiciones[2] = new Kinematic();
        Posiciones[2].Posicion = new Vector3(-1f, 0f, -2f);
        Posiciones[2].Orientacion = 0;

        Posiciones[3] = new Kinematic();
        Posiciones[3].Posicion = new Vector3(1f, 0f, -2f);
        Posiciones[3].Orientacion = 0;

        Posiciones[4] = new Kinematic();
        Posiciones[4].Posicion = new Vector3(0f, 0f, -3f);
        Posiciones[4].Orientacion = 180;
    }
}
