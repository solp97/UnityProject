using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Enemy
{
    private void OnEnable()
    {
        InitCooltime = 5;
        Helth = 3;
        RemainingCooltime = Random.Range(1, InitCooltime);
        pos = new GridIndex(7, 4);
        MoveDir = new GridIndex[1] { new GridIndex(-1, 0) };
        _moveType = EMovementType.Slide;
        MoveCount = 1;
        Gamemanager.instance.OnTurnEnd += TurnCount;

    }

    protected override void CheckAttak(GridIndex playerPos)
    {
        if (playerPos == pos + new GridIndex(-1, -1) || playerPos == pos + new GridIndex(1, -1))
            Debug.Log("Á×À½");
    }


    protected override void Findtarget(GridIndex targetGrid, out Vector3 target)

    {
        GridIndex forward = pos + MoveDir[0];
        pos = forward;
        target = Gamemanager.instance.Board.BoardPan[forward.X, forward.Y];
    }
}
