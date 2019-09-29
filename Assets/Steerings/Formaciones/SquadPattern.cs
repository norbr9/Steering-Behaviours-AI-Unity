using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadPattern : FormationPattern
{
    public SquadPattern(Agent lider) : base(lider)
    {
        Posiciones = new Kinematic[3];

        MaxSlotNumber = 3;

        Posiciones[0] = new Kinematic();
        Posiciones[0].Posicion = new Vector3(1f, 0f, 0f);
        Posiciones[0].Orientacion = 0;
        Posiciones[0].ExteriorRadius = 0.01f;

        Posiciones[1] = new Kinematic();
        Posiciones[1].Posicion = new Vector3(0f, 0f, -1f);
        Posiciones[1].Orientacion = 135f;
        Posiciones[1].ExteriorRadius = 0.01f;

        Posiciones[2] = new Kinematic();
        Posiciones[2].Posicion = new Vector3(1f, 0f, -1f);
        Posiciones[2].Orientacion = -135f;
        Posiciones[2].ExteriorRadius = 0.01f;
    }
}
