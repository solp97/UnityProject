using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool Mouse { get; private set; }
    void Update()
    {
        if (Gamemanager.instance.IsGameOver)
        {
            Mouse = false;
            return;
        }
        Mouse = Input.GetMouseButtonDown(0);
        Debug.DrawLine(transform.position, transform.forward * 10f, Color.black);
        Vector3 referenceVector = Quaternion.AngleAxis(22.5f, Vector3.up) * transform.forward;
        Debug.DrawLine(transform.position, referenceVector * 10f, Color.red);


    }
}
