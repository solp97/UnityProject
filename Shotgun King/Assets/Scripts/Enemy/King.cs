using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Enemy
{
    private void OnEnable()
    {
        InitCooltime = 4;
        RemainingCooltime = Random.Range(1, InitCooltime);
        Helth = 8;
        pos = new GridIndex(7, 4);
        MoveDir = new GridIndex[8] { new GridIndex(1, 0), new GridIndex(1, 1), new GridIndex(0, 1), new GridIndex(-1, 1), new GridIndex(-1, 0), new GridIndex(-1, -1), new GridIndex(0, -1), new GridIndex(1, -1) };
        _moveType = EMovementType.Slide;
        MoveCount = 1;
        Gamemanager.instance.OnTurnEnd += TurnCount;
        //transform.position = Gamemanager.instance.Board.BoardPan[pos.X, pos.Y];
    }


    protected override void CheckAttak(GridIndex playerPos)
    {
        base.CheckAttak(playerPos);
    }

    protected override void arrivalPosition(GridIndex targetGrid, out Vector3 target)
    {
        base.arrivalPosition(targetGrid, out target);
    }
}
