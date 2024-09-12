using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : SingletonPattern.Singleton<EnemyManager>
{
   List<Enemy> fieldEnemyList = new List<Enemy>();

    public void AddListEnemy(Enemy enemy)
    {
        fieldEnemyList.Add(enemy);
    }

    public void DeleteListEnemy(Enemy enemy)
    {
        fieldEnemyList.Remove(enemy);
    }

    public void DeleteAllEnemy()
    {
        int tmp = fieldEnemyList.Count;
        for (int i = 0; i < tmp; i++)
        {
            Destroy(fieldEnemyList[0].gameObject);
            fieldEnemyList.RemoveAt(0);
        }
    }

    public void PauseMove() //isMoveStopÀÏ‹š È£Ãâ
    {
        foreach(var enemy in fieldEnemyList)
        {
            enemy.Speed = 0f;
        }
    }
    
    public void ResumeMove()
    {
        foreach (var enemy in fieldEnemyList)
        {
            enemy.Speed = Enemy.FixedSpeed;
        }
    }
}
