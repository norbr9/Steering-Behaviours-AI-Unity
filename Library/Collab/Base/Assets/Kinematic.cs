using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Kinematic : BasicAtrs
{
    public float mass;
    public float maxAcceleration;
    public float maxSpeed;
    public float maxRotation;
    public float maxAngularAceleration;
      

    public void SetOrientacion()
    {
        Orientacion += Rotacion * Time.deltaTime;

        if (Rotacion == 0 && Velocidad.magnitude != 0)
            Orientacion = Mathf.Atan2(Velocidad.x, Velocidad.z) * Mathf.Rad2Deg;



        float orientacion2 = Orientacion % 360;

        if (orientacion2 < 0)
        {
            orientacion2 += 360;
        }


        Orientacion = orientacion2;
        

        transform.eulerAngles = new Vector3(0, Orientacion, 0);
        
       
    }

    public void setPosicion()
    {
        Posicion = new Vector3(Posicion.x + Velocidad.x * Time.deltaTime, Posicion.y, Posicion.z + Velocidad.z * Time.deltaTime);
        transform.position = new Vector3(Posicion.x, Posicion.y, Posicion.z);
    }

    public void setVelocidad(Vector3 lineal)
    {
        if (lineal.magnitude != 0)
            Velocidad += lineal * Time.deltaTime;
        else
            Velocidad = new Vector3(0, 0, 0);

        Velocidad = Vector3.ClampMagnitude(Velocidad, maxSpeed);

    }

    public void setRotacion(float angular)
    {
        if (angular == 0)
            Rotacion = 0;
        else
            Rotacion += angular * Time.deltaTime;

        Rotacion = Mathf.Clamp(Rotacion, -maxRotation, maxRotation);
    }


   




    /*
    // No Acelerado
    public void setOrientacion()
    {
        if (velocidad.magnitude != 0)
            orientacion = Mathf.Atan2(velocidad.x, velocidad.z) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, orientacion, 0);
    }

    public void setPosicion()
    {
        transform.localPosition = new Vector3(posicion.x, posicion.y, posicion.z);
    }*/



}
