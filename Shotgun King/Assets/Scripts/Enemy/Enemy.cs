using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Queue<GridIndex> queue = new Queue<GridIndex>();

    public int InitCooltime;
    public int RemainingCooltime;
    public int Helth;
    public GridIndex pos;
    public int MoveCount;
    [SerializeField]
    private float _jumpForce = 0.2f;

    public bool IsCheckMate;

    public Board Board;

    public int[,] Weight = new int[8, 8];


    public enum EMovementType
    {
        Jump,
        Slide,
    }

    public GridIndex[] MoveDir;

    public EMovementType _moveType;

    private void Awake()
    {
        _moveType = EMovementType.Jump;
    }

    private void OnEnable()
    {
        Gamemanager.instance.OnTurnEnd += TurnCount;
    }

    public void TurnCount(GridIndex playerPos)
    {
        Debug.Log("TurnCount");
        if (RemainingCooltime > 1)
        {
            CheckAttak(playerPos);
            --RemainingCooltime;
        }
        else
        {
            Vector3 target;
            arrivalPosition(playerPos, out target);
            Move(transform.position, target);
            RemainingCooltime = InitCooltime;
        }

    }

    protected virtual void CheckAttak(GridIndex playerPos)
    {
        foreach (GridIndex index in MoveDir)
        {
            for (int i = 1; i <= MoveCount; ++i)
            {
                GridIndex newIndex = pos + (index * i);
                if (newIndex.X < 0 || newIndex.X > 7 || newIndex.Y < 0 || newIndex.Y > 7)
                    break;
                if (Board.state[newIndex.X, newIndex.Y] != Board.State.empty)
                    break;
                if (newIndex == playerPos)
                    Debug.Log("죽음");
            }
        }
    }

    protected virtual void arrivalPosition(GridIndex targetGrid, out Vector3 target)
    {
        GridIndex bestposition = new GridIndex();
        CalIncrease(targetGrid);
        foreach (GridIndex j in MoveDir)
        {
            for (int i = 1; i <= MoveCount; ++i)
            {
                GridIndex newIndex = pos + (j * i);
                if (newIndex.X < 0 || newIndex.X > 7 || newIndex.Y < 0 || newIndex.Y > 7)
                    break;
                if (Board.state[newIndex.X, newIndex.Y] != Board.State.empty)
                    break;
                queue.Enqueue(newIndex);
            }
        }
        int bestWeight = 0;
        while (queue.Count != 0)
        {
            GridIndex searchPos = queue.Dequeue();
            int weight = 0;
            foreach (GridIndex index in MoveDir)
            {
                for (int i = 1; i <= MoveCount; ++i)
                {
                    GridIndex newIndex = searchPos + (index * i);
                    if (newIndex.X < 0 || newIndex.X > 7 || newIndex.Y < 0 || newIndex.Y > 7)
                        break;
                    if (Board.state[newIndex.X, newIndex.Y] != Board.State.empty)
                        break;

                    weight += Weight[newIndex.X, newIndex.Y];
                }
            }
            if (bestWeight <= weight)
            {
                bestWeight = weight;
                bestposition = searchPos;
            }

        }

        pos = bestposition;
        target = Board.BoardPan[bestposition.X, bestposition.Y];
    }

    public void Move(Vector3 startPos, Vector3 targetPos)
    {
        // 움직여야 하는 시점
        if (_moveType == EMovementType.Jump)
        {
            StartCoroutine(Jump(startPos, targetPos));
        }
        else
        {
            StartCoroutine(Slide(startPos, targetPos));
        }
    }



    IEnumerator Jump(Vector3 startPos, Vector3 targetPos)
    {
        // 베지어 곡선
        float elapsedTime = 0f;
        float totalTime = 1f;
        Vector3 via = new Vector3(Vector3.Lerp(startPos, targetPos, 0.5f).x, _jumpForce, Vector3.Lerp(startPos, targetPos, 0.5f).z);

        while (elapsedTime < totalTime)
        {
            elapsedTime += Time.deltaTime;
            Vector3 v1 = Vector3.Lerp(startPos, via, elapsedTime / totalTime);
            Vector3 v2 = Vector3.Lerp(via, targetPos, elapsedTime / totalTime);

            transform.position = Vector3.Lerp(v1, v2, elapsedTime / totalTime);

            yield return null;
        }
    }


    IEnumerator Slide(Vector3 startPos, Vector3 targetPos)
    {
        float elapsedTime = 0f;
        float totalTime = 1f;

        while (elapsedTime < totalTime)
        {
            elapsedTime += Time.deltaTime;

            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / totalTime);

            yield return null;
        }
    }

    //protected void Jump(Vector3 startPos, Vector3 targetPos)
    //{
    //    // 베지어 곡선
    //}
    //
    //protected void Slide(Vector3 startPos, Vector3 targetPos)
    //{
    //    // 선형보간운동
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            Damaged();
            Debug.Log("아야");
            if (Helth > 0)
                Die();
        }
    }

    public void Damaged()
    {
        --Helth;
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

    public void CalIncrease(GridIndex index)
    {
        System.Array.Clear(Weight, 0, 8 * 8);
        Weight[index.X, index.Y] = 2;
        GridIndex[] di = new GridIndex[8] { new GridIndex(0, 1), new GridIndex(-1, 1), new GridIndex(-1, 0), new GridIndex(-1, -1), new GridIndex(0, -1), new GridIndex(1, -1), new GridIndex(1, 0), new GridIndex(1, 1) };

        for (int i = 0; i < 8; ++i)
        {
            GridIndex newIndex = index + di[i];
            if (newIndex.X < 0 || newIndex.X > 7 || newIndex.Y < 0 || newIndex.Y > 7)
                continue;
            Weight[newIndex.X, newIndex.Y] = 1;
        }
    }


    private void OnDisable()
    {
        Gamemanager.instance.OnTurnEnd -= TurnCount;
    }
}
