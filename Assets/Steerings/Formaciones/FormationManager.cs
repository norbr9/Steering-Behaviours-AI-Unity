using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationManager : MonoBehaviour
{
    private enum TipoFormacion { Rupia, Cuadrado };

    [SerializeField]
    private TipoFormacion tipoFormacion;

    private List<SlotAssignment> slotAssignments = new List<SlotAssignment>();

    [SerializeField]
    private List<Agent> personajes;

    [SerializeField]
    private Agent lider;

    public List<SlotAssignment> SlotAssignments { get => slotAssignments; set => slotAssignments = value; }
    public List<Agent> Personajes { get => personajes; set => personajes = value; }
    public Agent Lider { get => lider; set => lider = value; }
    
    FormationPattern formacion;

    void UpdateSlotAssignments()
    {
        for (int i = 0; i < Personajes.Count && i < formacion.MaxSlotNumber ; i++)
        {
            SlotAssignment slot = new SlotAssignment(Personajes[i], i);

            SlotAssignments.Add(slot);
        }
    }

    public Kinematic GetSlotByCharacter(Agent character)
    {
        for (int i = 0; i < SlotAssignments.Count; i++)
        {
            if (SlotAssignments[i].Character.Equals(character))
                return SlotAssignments[i].Location;
        }
        return null;
    }

    void UpdateSlots()
    {
        if (Personajes.Count == 0 || Lider == null) return;

        for(int i = 0; i < slotAssignments.Count; i++)
        {
            SlotAssignments[i].Location = formacion.GetSlotLocation(i);
        }
    }

    private void Start()
    {
        switch (tipoFormacion)
        {
            case TipoFormacion.Rupia:
                formacion = new RupiaPattern(Lider);
                break;
            case TipoFormacion.Cuadrado:
                formacion = new SquadPattern(Lider);
                break;
            default:
                break;
        }
        UpdateSlotAssignments();
    }

    private void Update()
    {
        UpdateSlots();
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        if (SlotAssignments.Count > 0)
        {
            foreach (SlotAssignment slot in SlotAssignments)
            {
                Gizmos.DrawSphere(slot.Location.Posicion, slot.Location.ExteriorRadius);
            }
        }
    }
}
