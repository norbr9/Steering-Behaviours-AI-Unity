using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kinematic : MonoBehaviour
{ 
    [Header("Atributos Principales")]
    [SerializeField]
    private Vector3 posicion;
    [SerializeField]
    private float orientacion;
    [SerializeField]
    Vector3 velocidad;
    [SerializeField]
    private float rotacion;


    [Header("Límites")]
    [SerializeField]
    private float maxAcceleration;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float maxRotation;
    [SerializeField]
    private float maxAngularAceleration;

    [Header("Radios")]
    [SerializeField]
    private float interiorRadius;
    [SerializeField]
    private float exteriorRadius;
    [SerializeField]
    private float targetRadius;
    [SerializeField]
    private float slowRadius;


    public Vector3 Posicion { get => posicion; set => posicion = value; }
    public float Orientacion { get => orientacion; set => orientacion = value; }
    public Vector3 Velocidad { get => velocidad; set => velocidad = value; }
    public float Rotacion { get => rotacion; set => rotacion = value; }

    public float MaxAcceleration { get => maxAcceleration; set => maxAcceleration = value; }
    public float MaxSpeed { get => maxSpeed; set => maxSpeed = value; }
    public float MaxRotation { get => maxRotation; set => maxRotation = value; }
    public float MaxAngularAceleration { get => maxAngularAceleration; set => maxAngularAceleration = value; }

    public float InteriorRadius { get => interiorRadius; set => interiorRadius = value; }
    public float ExteriorRadius { get => exteriorRadius; set => exteriorRadius = value; }
    public float TargetRadius { get => targetRadius; set => targetRadius = value; }
    public float SlowRadius { get => slowRadius; set => slowRadius = value; }

    public void Update()
    {
        
    }

    public void Start()
    {
        interiorRadius = 1;
        exteriorRadius = 1;

        Posicion = transform.localPosition;
        Orientacion = transform.eulerAngles.y;
    }

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

    public void SetPosicion()
    {
        Posicion = new Vector3(Posicion.x + Velocidad.x * Time.deltaTime, Posicion.y, Posicion.z + Velocidad.z * Time.deltaTime);
        transform.position = new Vector3(Posicion.x, Posicion.y, Posicion.z);
    }

    public void SetVelocidad(Vector3 lineal)
    {
        if (lineal.magnitude != 0)
            Velocidad += lineal * Time.deltaTime;
        else
            Velocidad = new Vector3(0, 0, 0);

        Velocidad = Vector3.ClampMagnitude(Velocidad, MaxSpeed);

    }

    public void SetRotacion(float angular)
    {
        if (angular == 0)
            Rotacion = 0;
        else
            Rotacion += angular * Time.deltaTime;

        Rotacion = Mathf.Clamp(Rotacion, -MaxRotation, MaxRotation);
    }
}
