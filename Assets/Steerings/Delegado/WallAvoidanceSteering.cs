using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAvoidanceSteering : SeekSteeringA
{
    class Sensor
    {
        public float angulo;
        public float lookAhead;

        public Vector3 rayo;

        public Sensor(float ang, float lookA)
        {
            angulo = ang;
            lookAhead = lookA;
        }
    }

    private List<Sensor> sensores;

    private GameObject t;

    [SerializeField]
    private bool drawSensorsGizmo = true;
    
    // Detector de colisión
    private CollisionDetector collisionDetector;

    // Mínima distancia al muro.
    [SerializeField]
    private float avoidDistance = 5;

    private Collision collision;

    private Vector3 AsVector(float orientacion)
    {
        return new Vector3(Mathf.Sin(orientacion * Mathf.Deg2Rad), 0, Mathf.Cos(orientacion * Mathf.Deg2Rad));
    }

    public override Steering getSteering(AgentNPC agent)
    {
        Steering steering = new Steering();
        
        foreach (Sensor sensor in sensores)
        {
            float angulo = sensor.angulo + Mathf.Atan2(agent.Velocidad.x, agent.Velocidad.z) * Mathf.Rad2Deg;
            Vector3 rayVector = AsVector(angulo);
            rayVector = rayVector.normalized;
            sensor.rayo = rayVector * sensor.lookAhead;

            collision = collisionDetector.GetCollision(agent.Posicion, sensor.rayo, sensor.rayo.magnitude);

            if (collision != null)
            {
                Vector3 newDir = collision.Posicion + collision.Normal * avoidDistance;
                target.Posicion = new Vector3(newDir.x, newDir.y, newDir.z);

                Destroy(t);
                return base.getSteering(agent);
            }
        }

        steering.Lineal = new Vector3(0, 0, 0);
        steering.Angular = 0;

        Destroy(t);
        return steering;

    }

    public void Awake()
    {
        collisionDetector = gameObject.AddComponent<CollisionDetector>();

        t = new GameObject();
        t.AddComponent<Kinematic>();

        target = t.GetComponent<Kinematic>();
        
        sensores = new List<Sensor>();
        Sensor s1 = new Sensor(-30, 5);
        Sensor s2 = new Sensor(0, 7);
        Sensor s3 = new Sensor(30, 5);
        sensores.Add(s1);
        sensores.Add(s2);
        sensores.Add(s3);
    }
    
    public void OnDrawGizmos()
    {
        if (drawSensorsGizmo)
        { //dibujo del raycast que detecta colisiones
            Gizmos.color = Color.green; //el raycast frontal tiene color verde
            int i = 0;
            if (sensores != null)
            {
                foreach (Sensor s in sensores)
                {
                    if (i != 1) Gizmos.color = Color.gray; //los laterales tienen color gris    
                    else Gizmos.color = Color.magenta;
                    Gizmos.DrawLine(gameObject.transform.position, gameObject.transform.localPosition + s.rayo);
                    i++;
                }
            }

            if (collision != null)
            { //dibujo de la normal cuando se detecta colision            
                Gizmos.color = Color.red;
                Gizmos.DrawLine(collision.Posicion, collision.Posicion + collision.Normal * avoidDistance);
            }
        }        
    }
}