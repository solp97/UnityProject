using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gamemanager : SingletonBehaviour<Gamemanager>
{
    public bool IsGameOver { get; private set; }
    public int Turn;
    public PlayerController _player;

    public event UnityAction<GridIndex> OnTurnEnd;
    public Board Board;

    private void Awake()
    {
        _player.TunrOver.AddListener(Turnover);
    }

    private void Update()
    {
        
    }

    void Turnover(GridIndex playerPos)
    {
        OnTurnEnd?.Invoke(playerPos);
    }

}
