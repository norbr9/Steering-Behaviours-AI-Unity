using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensiveCirclePattern : FormationPattern
{
    [SerializeField]
    private float characterRadius = 6;

    public DefensiveCirclePattern(Agent lider) : base(lider)
    {
    }

    public float CharacterRadius { get => characterRadius; set => characterRadius = value; }

    public int CalculateNumberOfSlots(List<SlotAssignment> assignments)
    {

        //Find the number of filled slots: it will be the highest slot number in the assignment
        int filledSlots = 0;
        foreach (SlotAssignment assignment in assignments)
        {
            if (assignment.SlotNumber >= MaxSlotNumber)
                filledSlots = assignment.SlotNumber;
        }

        //Add one to go from the index of the highest slot to the number of slots needed
        NumberOfSlots = filledSlots + 1;

        return NumberOfSlots;
    }

    public  Kinematic GetDriftOffset(List<SlotAssignment> assignments)
    {
        NumberOfSlots = assignments.Count;
        Kinematic center = new Kinematic
        {
            Posicion = Vector3.zero,
            Orientacion = 0
        };

        foreach (SlotAssignment slot in assignments)
        {
            Kinematic location = GetSlotLocation(slot.SlotNumber);
            center.Posicion = new Vector3(center.Posicion.x + location.Posicion.x,
                location.Posicion.y,
                center.Posicion.z + location.Posicion.z);
            center.Orientacion += location.Orientacion;
        }


        /*
        for (int i = 0; i < assignments.Count; i++)
        {
            center.Posicion = new Vector3(center.Posicion.x + assignments[i].Character.transform.localPosition.x,
                center.Posicion.y + assignments[i].Character.transform.localPosition.y,
                center.Posicion.z + assignments[i].Character.transform.localPosition.z);
            center.Orientacion += assignments[i].Character.GetComponent<Agent>().orientacion;
        }*/

        int numberOffAssignments = assignments.Count;
        center.Posicion = new Vector3(center.Posicion.x / numberOffAssignments,
                                                        center.Posicion.y / numberOffAssignments,
                                                        center.Posicion.z / numberOffAssignments);
        center.Orientacion /= numberOffAssignments;

        return center;

    }

    /*public override Kinematic GetSlotLocation(int slotNumber)
    {
        float angleAroundCircle = slotNumber / (float)NumberOfSlots;

        angleAroundCircle *= (Mathf.PI * 2);
        //angleAraoundCircle son raidanes

        //The radius depends on the radius of the character, and the number f character in the circle
        // We want there to be no gap between character's shoulders
        float radius = CharacterRadius / Mathf.Sin(Mathf.PI / NumberOfSlots);
        //Create a location, and fill its components based on the angle aorund circle
        Kinematic location = new Kinematic
        {
            Posicion = new Vector3(radius * Mathf.Cos(angleAroundCircle),
            0, radius * Mathf.Sin(angleAroundCircle)),

            Orientacion = angleAroundCircle
        };

        return location;
/*
        v += center.transform.localPosition;
        assignment.Character.posicion = new Vector3(v.x, v.y, v.z);
        //The characters should be facing out
        assignment.Character.orientacion = angleAroundCircle;

        //return the slot location
        return assignment.Character;*/
 //   }

    public  bool SupportsSlots(int slotCount)
    {
        if (MaxSlotNumber >= slotCount)
            return true;
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
