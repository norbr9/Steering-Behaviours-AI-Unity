using System.Collections.Generic;
using UnityEngine;

public abstract class FormationPattern
{
    private Agent lider;
    private Kinematic[] posiciones;

    private int numberOfSlots;

    private int maxSlotNumber;

    private float distanciaLider = 2f;


    public FormationPattern(Agent lider)
    {
        Lider = lider;
    }
    
    public int MaxSlotNumber { get => maxSlotNumber; set => maxSlotNumber = value; }
    public Agent Lider { get => lider; set => lider = value; }

    public int NumberOfSlots { get => numberOfSlots; set => numberOfSlots = value; }
    public Kinematic[] Posiciones { get => posiciones; set => posiciones = value; }
    public float DistanciaLider { get => distanciaLider; set => distanciaLider = value; }

    public Kinematic GetSlotLocation(int slotNumber)
    {
        float oriLider = Lider.Orientacion * Mathf.Deg2Rad;

        Kinematic location = new Kinematic();
        float r1 = Mathf.Cos(oriLider);
        float r2 = Mathf.Sin(oriLider);
        float r3 = -Mathf.Sin(oriLider);

        Vector3 newPosition = new Vector3(Posiciones[slotNumber].Posicion.x * r1 + Posiciones[slotNumber].Posicion.z * r2,
                                            Lider.Posicion.y,
                                            Posiciones[slotNumber].Posicion.x * r3 + Posiciones[slotNumber].Posicion.z * r1) * DistanciaLider;

        location.Posicion = new Vector3(Lider.Posicion.x + newPosition.x, Lider.Posicion.y, Lider.Posicion.z + newPosition.z);
        location.Orientacion = Lider.Orientacion + Posiciones[slotNumber].Orientacion;

        return location;
    }

}