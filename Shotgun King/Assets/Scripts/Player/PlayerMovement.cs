using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Board _board;

    //enum Dir
    //{
    //    Up,
    //    RightUp,
    //    Right,
    //    RightDown,
    //    Down,
    //    LeftDown,
    //    Left,
    //    LeftUp
    //};
    //private static readonly int[] dx = { 1, 1, 0, -1, -1, -1, 0, 1 };
    //private static readonly int[] dy = { 0, 1, 1, 1, 0, -1, -1, -1 };

    enum Dir
    {
        Up,
        LeftUp,
        Left,
        LeftDown,
        Down,
        RightDown,
        Right,
        RightUp
    };
    private static readonly int[] dx = { 1, 1, 0, -1, -1, -1, 0, 1 };
    private static readonly int[] dy = { 0, -1, -1, -1, 0, 1, 1, 1 };
    private bool isLeft;

    public bool isMove = false;

    private float totalTime = 1f;

    [SerializeField]
    private float jumpForce = 1.0f;

    public void isCanMove(Transform Player, Vector3 target)
    {
        GridIndex start = _board.PlayerPos;
        Dir dir;
        Vector3 targetDirection = (target - Player.position).normalized;

        Vector3 referenceVector = Quaternion.AngleAxis(22.5f, Vector3.up) * transform.forward;
        Debug.DrawLine(transform.position, referenceVector * 10f, Color.red);

        float Angle = Quaternion.FromToRotation(targetDirection, referenceVector).eulerAngles.y;
        dir = (Dir)(Angle / 45f);

        //float dotProduct = Vector3.Dot(Player.forward, targetDirection);
        //float Angle = Mathf.Acos(dotProduct) * Mathf.Rad2Deg;
        //if (targetDirection.x > 0)
        //    isLeft = false;
        //else
        //    isLeft = true;
        //
        //if (Angle < 22.5)
        //    dir = Dir.Up;
        //else if (Angle >= 22.5 && Angle < 67.5)
        //{
        //    if (isLeft)
        //        dir = Dir.LeftUp;
        //    else
        //        dir = Dir.RightUp;
        //}
        //else if (Angle >= 67.5 && Angle < 112.5)
        //{
        //    if (isLeft)
        //        dir = Dir.Left;
        //    else
        //        dir = Dir.Right;
        //}
        //else if (Angle >= 112.5 && Angle < 157.5)
        //{
        //    if (isLeft)
        //        dir = Dir.LeftDown;
        //    else
        //        dir = Dir.RightDown;
        //}
        //else
        //    dir = Dir.Down;
        int nx = start.X + dx[(int)dir];
        int ny = start.Y + dy[(int)dir];
        if (nx < 0 || nx >= 8 || ny < 0 || ny >= 8)
        {
            return;
        }
        isMove = true;
        Vector3 Start = _board.BoardPan[start.X, start.Y];
        Vector3 End = _board.BoardPan[nx, ny];
        Vector3 Via = new Vector3(Vector3.Lerp(Start, End, 0.5f).x, jumpForce, Vector3.Lerp(Start, End, 0.5f).z);
        StartCoroutine(CalculateCurvePoints(Start, Via, End));

        //transform.position = _board.BoardPan[nx, ny];
        _board.PlayerPos = new GridIndex(nx, ny);

    }

    // 베지에 곡선을 통한 점프
    IEnumerator CalculateCurvePoints(Vector3 start, Vector3 via, Vector3 end)
    {
        float elapsedTime = 0f;
        while (elapsedTime < totalTime)
        {
            elapsedTime += Time.deltaTime;
            //Debug.Log(elapsedTime);
            Vector3 v1 = Vector3.Lerp(start, via, elapsedTime / totalTime);
            Vector3 v2 = Vector3.Lerp(via, end, elapsedTime / totalTime);

            transform.position = Vector3.Lerp(v1, v2, elapsedTime / totalTime);
            //transform.position = Vector3.Lerp(start, end, elapsedTime / totalTime);

            yield return null;
        }
        isMove = false;
        //Debug.Log("end");
    }
    //
    //private void CalculateCurvePoints(Vector3 start, Vector3 end, float elapsedTime)
    //{
    //    float totalTime = 1f;
    //    while ()
    //    {
    //
    //        transform.position = Mathf.Lerp(start, end,)
    //    }
    //}

}
