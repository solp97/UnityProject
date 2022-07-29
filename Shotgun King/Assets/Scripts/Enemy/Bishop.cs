using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Enemy
{
    
    private void OnEnable()
    {
        InitCooltime = 3;
        RemainingCooltime = Random.Range(1, InitCooltime);
        Helth = 4;
        pos = new GridIndex(7, 4);
        MoveDir = new GridIndex[4] { new GridIndex(-1, -1), new GridIndex(1, 1), new GridIndex(1, -1), new GridIndex(-1, 1) };
        _moveType = EMovementType.Slide;
        MoveCount = 7;
        Gamemanager.instance.OnTurnEnd += TurnCount;
        //transform.position = Gamemanager.instance.Board.BoardPan[pos.X, pos.Y];
    }

    //protected override void CheckAttak(GridIndex playerPos)
    //{
    //    foreach (GridIndex index in MoveDir)
    //    {
    //        for (int i = 1; i < MoveCount; ++i)
    //        {
    //            GridIndex newIndex = pos + (index * i);
    //            if (newIndex.X < 0 || newIndex.X > 7 || newIndex.Y < 0 || newIndex.Y > 7)
    //                break;
    //            if (Board.state[newIndex.X, newIndex.Y] != Board.State.empty)
    //                break;
    //            if (newIndex == playerPos)
    //                Debug.Log("Á×À½");
    //        }
    //    }
    //}
    //
    //protected override void arrivalPosition(GridIndex targetGrid, out Vector3 target)
    //{
    //    GridIndex bestposition = new GridIndex();
    //    CalIncrease(targetGrid);
    //    foreach (GridIndex j in MoveDir)
    //    {
    //        for (int i = 1; i < MoveCount; ++i)
    //        {
    //            GridIndex newIndex = pos + (j * i);
    //            if (newIndex.X < 0 || newIndex.X > 7 || newIndex.Y < 0 || newIndex.Y > 7)
    //                break;
    //            if (Board.state[newIndex.X, newIndex.Y] != Board.State.empty)
    //                break;
    //            queue.Enqueue(newIndex);
    //        }
    //    }
    //    int bestWeight = 0;
    //    while (queue.Count != 0)
    //    {
    //        GridIndex searchPos = queue.Dequeue(); 
    //        int weight = 0;
    //        foreach (GridIndex index in MoveDir)
    //        {
    //            for (int i = 1; i < MoveCount; ++i)
    //            {
    //                GridIndex newIndex = searchPos + (index * i);
    //                if (newIndex.X < 0 || newIndex.X > 7 || newIndex.Y < 0 || newIndex.Y > 7)
    //                    break;
    //                if (Board.state[newIndex.X, newIndex.Y] != Board.State.empty)
    //                    break;
    //
    //                weight += Weight[newIndex.X, newIndex.Y];
    //            }
    //        }
    //        if (bestWeight <= weight)
    //        {
    //            bestWeight = weight;
    //            bestposition = searchPos;
    //        }
    //        
    //    }
    //
    //    pos = bestposition;
    //    target = Board.BoardPan[bestposition.X, bestposition.Y];
    //}

    protected override void arrivalPosition(GridIndex targetGrid, out Vector3 target)
    {
        base.arrivalPosition(targetGrid, out target);
    }

    protected override void CheckAttak(GridIndex playerPos)
    {
        base.CheckAttak(playerPos);
    }

}