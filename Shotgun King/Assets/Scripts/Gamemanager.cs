using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : SingletonBehaviour<Gamemanager>
{
    public bool IsGameOver { get; private set; }
    public int Turn;


    private void Update()
    {
     
    }
}
