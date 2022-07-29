using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Enemy
{
    private void OnEnable()
    {
        InitCooltime = 5;
        RemainingCooltime = Random.Range(1, InitCooltime);
        Helth = 3;
        pos = new GridIndex(7, 4);
        MoveDir = new GridIndex[1] { new GridIndex(-1, 0) };
        _moveType = EMovementType.Slide;
        MoveCount = 1;
        Gamemanager.instance.OnTurnEnd += TurnCount;
        //transform.position = Gamemanager.instance.Board.BoardPan[pos.X, pos.Y];
    }

    protected override void CheckAttak(GridIndex playerPos)
    {
        GridIndex checkpos1 = pos + new GridIndex(-1, -1);
        GridIndex checkpos2 = pos + new GridIndex(-1, 1);


        if (checkpos1.X > 0 && playerPos == checkpos1 || checkpos2.Y < 7 && playerPos == checkpos2)
        {
           // if (IsCheckMate)
                Debug.Log("³Ê Á×À½");
            //else
            //    IsCheckMate = true;
        }
       //else
       //{
       //    IsCheckMate = false;
       //}
        //Debug.Log(IsCheckMate);
    }
    public bool isPromotion()
    {
        return true;
    }

    void Promotion()
    {

    }

    protected override void arrivalPosition(GridIndex targetGrid, out Vector3 target)
    {
        GridIndex forward = pos + MoveDir[0];
        pos = forward;
        Debug.Log(forward);
        target = Board.BoardPan[forward.X, forward.Y];
        Debug.Log(target);
    }
}
