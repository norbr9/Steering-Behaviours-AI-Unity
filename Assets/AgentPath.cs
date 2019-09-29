using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentPath : Agent
{
    // Start is called before the first frame update
    public new void Start()
    {
        base.Start();
        Posicion = transform.localPosition;
    }

    // Update is called once per frame
    new void Update()
    {
    }
}
