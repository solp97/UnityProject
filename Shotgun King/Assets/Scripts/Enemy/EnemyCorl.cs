using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyCorl : MonoBehaviour
{
    public Board Board;

    int[] spawnEnemyCount = { 1, 1, 1, 1, 1, 3 };// Å· Äý ·Ï ºñ¼ó ³ªÀÌÆ® Æù
    public int[,] Weight = new int[8, 8];
    //public event UnityAction 

    private void Awake()
    {
    //    Gamemanager.instance.OnTurnEnd += CalIncrease;
    }

    void spawn()
    {
        foreach (int enemyCount in spawnEnemyCount)
        {

            for (int i = 0; i < enemyCount;)
            {

            }
        }
    }

    void CalIncrease(GridIndex index)
    {
        System.Array.Clear(Weight, 0, 8 * 8);
        Weight[index.X, index.Y] = 3;
        GridIndex[] di = new GridIndex[8] { new GridIndex(0, 1), new GridIndex(-1, 1), new GridIndex(-1, 0), new GridIndex(-1, -1), new GridIndex(0, -1), new GridIndex(1, -1), new GridIndex(1, 0), new GridIndex(1, 1) };

        for (int i = 0; i < 8; ++i)
        {
            GridIndex newIndex = index + di[i];
            if ((newIndex.X < 0 || newIndex.X > 8) && (newIndex.Y < 0 || newIndex.Y > 8))
                continue;
            Weight[newIndex.X, newIndex.Y] = 1;
        }
    }

}
