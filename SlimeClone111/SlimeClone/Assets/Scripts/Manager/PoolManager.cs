using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : SingletonPattern.Singleton<PoolManager>
{
    // 프리팹들을 보관할 변수
    public GameObject[] prefabs;
    // 풀담당을 하는 리스트들

    List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];
        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>(); //각각의 list순회하며 초기화 

        }
    }

/*    public GameObject Get(int index)
    {
        GameObject select = null;
        // 선택한풀의 놀고 있는 (비활성화된) 게임오브젝트 접근 
        // 발견하면 select변수에 할당 

        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf) //비활성화 일 경우
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }
        // 못 찾으면? 
        // 새롭게 생성하고 select에 할당 
        if (!select) // !select 는 select == null과 같은말 
        {
            select = Instantiate(prefabs[index], transform); //transform은 부모(이 PoolManager)의 자식으로 생성한다는말
            pools[index].Add(select);
        }
        select = Instantiate(prefabs[index], transform); //transform은 부모(이 PoolManager)의 자식으로 생성한다는말
        pools[index].Add(select);

        return select;


    }*/
    public GameObject Get(int index)
    {
        GameObject select = null;
        // 선택한풀의 놀고 있는 (비활성화된) 게임오브젝트 접근 
        // 발견하면 select변수에 할당 

        foreach (GameObject item in pools[index])
        {
            select = item;
            select.SetActive(true);
            select = Instantiate(prefabs[index], transform); //transform은 부모(이 PoolManager)의 자식으로 생성한다는말

            pools[index].Add(select);
        }

        return select;


    }
}
