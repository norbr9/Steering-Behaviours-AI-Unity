using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentPlayer : Agent
{
    public float velocity = 8f;

    private Vector3 lastPosition;

    // Start is called before the first frame update
    public new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        lastPosition = transform.position;

        // Leer el teclado
        Vector3 newDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // Mirar en la dirección del vector leído.
        transform.LookAt(transform.position + newDirection);

        // Avanzar de acuerdo a la velocidad establecida
        transform.position += newDirection * velocity * Time.deltaTime;

        Posicion = transform.position;

        Velocidad = (Posicion - lastPosition) / Time.deltaTime;

        transform.GetComponentInParent<Kinematic>().Orientacion = transform.eulerAngles.y;
    }
}
