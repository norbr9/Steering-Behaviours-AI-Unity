using UnityEngine;
using System.Collections;

public class CollisionDetector : MonoBehaviour
{
    public Collision GetCollision(Vector3 origen, Vector3 direccion, float maxDistancia)
    {
        Collision colision = new Collision();
        RaycastHit[] hit = Physics.RaycastAll(origen, direccion, maxDistancia, LayerMask.GetMask("Muro"));

        colision.Normal = Vector3.zero;
        colision.Posicion = Vector3.zero;

        bool hayColision = false;
        if (hit.Length > 0)
        {
            colision.Posicion = hit[0].point;
            colision.Normal += hit[0].normal;
            hayColision = true;
        }
        if (hayColision)
            return colision;
        return null;
    }
}

