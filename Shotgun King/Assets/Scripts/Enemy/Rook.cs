using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Enemy
{
    private void Awake()
    {

    }
    private void OnEnable()
    {
        InitCooltime = 5;
        RemainingCooltime = 3;
        pos = new GridIndex(7, 6);
        _moveType = EMovementType.Slide;
        MoveDir = new GridIndex[4] { new GridIndex(-1, 0), new GridIndex(1, 0), new GridIndex(0, -1), new GridIndex(0, 1) };
        MoveCount = 7;
    }
    




}
