using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AugmentManager : SingletonPattern.Singleton<AugmentManager>
{
    List<AugObj> fieldAugmentList = new List<AugObj>();

    public void AddListAugment(AugObj enemy)
    {
        fieldAugmentList.Add(enemy);
    }

    public void DeleteListAugment(AugObj enemy)
    {
        fieldAugmentList.Remove(enemy);
    }

    public void DeleteAllAugment()
    {
        int tmp = fieldAugmentList.Count;
        for (int i = 0; i < tmp; i++)
        {
            Destroy(fieldAugmentList[0].gameObject);
            fieldAugmentList.RemoveAt(0);
        }
    }

    public void PauseMove() //isMoveStopÀÏ‹š È£Ãâ
    {
        foreach (var augment in fieldAugmentList)
        {
            augment.Speed = 0f;
        }
    }

    public void ResumeMove()
    {
        foreach (var augment in fieldAugmentList)
        {
            augment.Speed = GameManager.FixedSpeed;
        }
    }
}
