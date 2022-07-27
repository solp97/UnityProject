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
        Debug.DrawLine(transform.position,transform.forward*10f);
    }
}
