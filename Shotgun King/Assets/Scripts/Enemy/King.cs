using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Enemy
{

    private void OnEnable()
    {
        InitCooltime = 5;
        RemainingCooltime = Random.Range(1, InitCooltime);
        Helth = 8;
        pos = new GridIndex(7, 4);
        MoveDir = new GridIndex[1] { new GridIndex(-1, 0) };
        _moveType = EMovementType.Slide;
        MoveCount = 1;
        Gamemanager.instance.OnTurnEnd += TurnCount;
        //transform.position = Gamemanager.instance.Board.BoardPan[pos.X, pos.Y];
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
