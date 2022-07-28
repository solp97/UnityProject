using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int InitCooltime;
    public int RemainingCooltime;
    public int Helth;
    public GridIndex pos;
    public int MoveCount;
    [SerializeField]
    private float _jumpForce = 0.2f;

    public bool IsCheckMate;

    public Board Board;

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

    virtual protected void CheckAttak(GridIndex playerPos)
    {
        
    }

    virtual protected void arrivalPosition(GridIndex targetGrid, out Vector3 target)
    {        
        target = Vector3.zero;

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


    private void OnDisable()
    {
        Gamemanager.instance.OnTurnEnd -= TurnCount;
    }
}
