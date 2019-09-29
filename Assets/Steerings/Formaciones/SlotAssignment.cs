using UnityEngine;
using System.Collections;

public class SlotAssignment
{
    private Agent character;
    private int slotNumber;
    private Kinematic location;

    public SlotAssignment(Agent character, int slotNumber)
    {
        this.character = character;
        this.slotNumber = slotNumber;
    }

    public int SlotNumber { get => slotNumber; set => slotNumber = value; }
    public Agent Character { get => character; set => character = value; }
    public Kinematic Location { get => location; set => location = value; }
}
