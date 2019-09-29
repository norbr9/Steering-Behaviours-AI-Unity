using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodo : MonoBehaviour
{
    [SerializeField]
    private bool drawRadius = true;
    [SerializeField]
    private int radius;
    [SerializeField]
    private int index;
    [SerializeField]
    private Vector3 posicion;

    public bool DrawRadius { get => drawRadius; set => drawRadius = value; }
    public int Radius { get => radius; set => radius = value; }
    public int Index { get => index; set => index = value; }
    public Vector3 Posicion { get => posicion; set => posicion = value; }
    
    public void OnDrawGizmos()
    {
        if (DrawRadius)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, Radius);
        }
    }

}
