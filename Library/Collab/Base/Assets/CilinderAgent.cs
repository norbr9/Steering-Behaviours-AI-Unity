using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CilinderAgent : AgentNPC
{
    public override bool applySteering(Steering steering)
    {
        /*if (steering == null)
        {
            Steering a = new Steering();
            a.lineal = new Vector3(0, 0, 0);
            a.angular = 0;

            setOrientacion();
            setPosicion();
            setVelocidad(a.lineal);
            setRotacion(a.angular);
            return false;
        }*/

        setVelocidad(steering.Lineal);
        setRotacion(steering.Angular);
        SetOrientacion();
        setPosicion();
        return true;
    }
}
