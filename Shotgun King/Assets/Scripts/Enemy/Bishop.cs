using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Enemy
{
    Queue<GridIndex> queue = new Queue<GridIndex>();

    private void OnEnable()
    {
        InitCooltime = 3;
        RemainingCooltime = Random.Range(1, InitCooltime);
        Helth = 4;
        pos = new GridIndex(7, 4);
        MoveDir = new GridIndex[4] { new GridIndex(-1, -1), new GridIndex(1, 1), new GridIndex(1, -1), new GridIndex(1, -1) };
        _moveType = EMovementType.Slide;
        MoveCount = 7;
        Gamemanager.instance.OnTurnEnd += TurnCount;
        //transform.position = Gamemanager.instance.Board.BoardPan[pos.X, pos.Y];
    }

    protected override void CheckAttak(GridIndex playerPos)
    {

    }

    protected override void arrivalPosition(GridIndex targetGrid, out Vector3 target)
    {
        GridIndex bestposition;
        for (int i = 0; i < MoveCount; ++i)
        {
            foreach (GridIndex j in MoveDir)
            {
                if (j.X * i < 0 || j.X * i > 7 || j.Y * i < 0 || j.Y * i > 7)
                    continue;
                    queue.Enqueue(j * i);
            }
        }
        while (queue.Count != 0)
        {
            for (int i = 0; i < MoveCount; ++i)
            {
                foreach (GridIndex j in MoveDir)
                {

                }
            }

        }



    }
