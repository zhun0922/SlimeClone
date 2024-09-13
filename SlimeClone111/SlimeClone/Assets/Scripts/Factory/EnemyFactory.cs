using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] GameObject[] enemyObjs;
    [SerializeField] float nextSpawnDelay = 1f;
    [SerializeField] float curSpawnDelay;

    private float spawnDelay_Min = 1f; 
    private float spawnDelay_Max = 2.2f;

    private int enemyIndex_Min = 0;
    private int enemyIndex_Max = 1;


    public List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;

    private void Awake()
    {
        spawnList = new List<Spawn>();
        //ReadSpawnFile();
    }


    private bool isAugmentTime = false;

    private float augmentTimer = 0f;  
    private float augmentExeDuration = 0.5f; 

    private void Update()
    {
        CheckAugmentTime(GameManager.Instance.ElapsedTime);

        if (isAugmentTime)
        {

            augmentTimer += Time.deltaTime;
            
            if (augmentTimer >= augmentExeDuration)
            {
                AugmentFactory.Instance.SpawnAugment();
                augmentTimer = 0f;   
                isAugmentTime = false;
                nextSpawnDelay += 1f;
            }
        }

        else if (!isAugmentTime)
        {
            curSpawnDelay += Time.deltaTime;

            UpdateSpawnValue(GameManager.Instance.ElapsedTime);

            if (curSpawnDelay > nextSpawnDelay && !GameManager.Instance.IsPauseMove) // && !spawnEnd
            {
                Spawn();
                nextSpawnDelay = UnityEngine.Random.Range(spawnDelay_Min, spawnDelay_Max);
                curSpawnDelay = 0;
            }
        }
    }

    public void Spawn()
    {
        if (!GameManager.Instance.IsPauseMove)
        {
            int numberOfSpawns = UnityEngine.Random.Range(1, 6);

            List<int> availablePoints = new List<int>() { 0, 1, 2, 3, 4 }; // 5개 포인트
            for (int i = 0; i < numberOfSpawns; i++)
            {
                if (availablePoints.Count == 0)
                    break;

                // 사용할 포인트를 무작위로 선택
                int spawnIndex = availablePoints[UnityEngine.Random.Range(0, availablePoints.Count)];
                availablePoints.Remove(spawnIndex); // 중복 스폰 방지

                // 어떤 오브젝트를 스폰할지 결정 (적, 골드, 증강체)
                //float randomValue = UnityEngine.Random.value;

                int enemyIndex = UnityEngine.Random.Range(enemyIndex_Min, enemyIndex_Max); //나중에 게임시간에 따라 변경

                Enemy enemy = Instantiate(enemyObjs[enemyIndex],
                        FactoryManager.Instance.SpawnPoints[spawnIndex].position,
                        FactoryManager.Instance.SpawnPoints[spawnIndex].rotation).GetComponent<Enemy>();
                EnemyManager.Instance.AddListEnemy(enemy);
            }
        }
    }

    public void UpdateSpawnValue(float elapsedTime)
    {
        if (elapsedTime >= 10f && enemyIndex_Max < 2)
        {
            spawnDelay_Min = 0.9f;
            spawnDelay_Max = 1.8f;
        }

        if (elapsedTime >= 20f)
        {
            enemyIndex_Max = 2;
        }

        if (elapsedTime >= 30f && enemyIndex_Max < 3)
        {
            enemyIndex_Max = 3;
            spawnDelay_Max = 1.5f;
        }

        if (elapsedTime >= 45f && enemyIndex_Max < 3)
        {
            enemyIndex_Max = 3;
            spawnDelay_Max = 1.2f;
        }

    }

    //급해서 이렇게 되었습니다.,,ㅠ
    private bool augmentTriggered1 = false;  
    private bool augmentTriggeredAt2 = false;
    private bool augmentTriggeredAt3 = false;
    private bool augmentTriggeredAt4 = false;
    private bool augmentTriggeredAt5 = false;
    private bool augmentTriggeredAt6 = false;
    private bool augmentTriggeredAt7 = false;
    private bool augmentTriggeredAt8 = false;
    private bool augmentTriggeredAt9 = false;
    private bool augmentTriggeredAt10 = false;

    public void CheckAugmentTime(float elapsedTime)
    {
        if (elapsedTime >= 5f && !augmentTriggered1)
        {
            isAugmentTime = true;
            augmentTriggered1 = true;
        }
        if (elapsedTime >= 15f && !augmentTriggeredAt2)
        {
            isAugmentTime = true;
            augmentTriggeredAt2 = true;    
        }
        if (elapsedTime >= 25f && !augmentTriggeredAt3)
        {
            isAugmentTime = true;
            augmentTriggeredAt3 = true;
        }
        if (elapsedTime >= 34f && !augmentTriggeredAt4)
        {
            isAugmentTime = true;
            augmentTriggeredAt4 = true;
        }
        if (elapsedTime >= 42f && !augmentTriggeredAt5)
        {
            isAugmentTime = true;
            augmentTriggeredAt5 = true;
        }
        if (elapsedTime >= 48f && !augmentTriggeredAt6)
        {
            isAugmentTime = true;
            augmentTriggeredAt6 = true;
        }
        if (elapsedTime >= 55f && !augmentTriggeredAt7)
        {
            isAugmentTime = true;
            augmentTriggeredAt7 = true;
        }
        if (elapsedTime >= 60f && !augmentTriggeredAt8)
        {
            isAugmentTime = true;
            augmentTriggeredAt8 = true;
        }
        if (elapsedTime >= 66f && !augmentTriggeredAt9)
        {
            isAugmentTime = true;
            augmentTriggeredAt9 = true;
        }
        if (elapsedTime >= 72f && !augmentTriggeredAt10)
        {
            isAugmentTime = true;
            augmentTriggeredAt10 = true;
        }
    }


    /* public void Spawn()
     {
         if (!GameManager.Instance.IsPauseMove)
         {
             //int ranEnemy = UnityEngine.Random.Range(0, 3);
             int enemyIndex = 0;
             switch (spawnList[spawnIndex].type)
             {
                 case EnemyType.S:
                     enemyIndex = 0;
                     break;
                 case EnemyType.M:
                     enemyIndex = 1;
                     break;
                 case EnemyType.L:
                     enemyIndex = 2;
                     break;
             }
             //int ranPoint = UnityEngine.Random.Range(0, 5);
             int enemyPoint = spawnList[spawnIndex].point;

             Enemy enemy = Instantiate(enemyObjs[enemyIndex],
                         FactoryManager.Instance.SpawnPoints[enemyPoint].position,
                         FactoryManager.Instance.SpawnPoints[enemyPoint].rotation).GetComponent<Enemy>();

             EnemyManager.Instance.AddListEnemy(enemy);

             //리스폰 인덱스 증가
             spawnIndex++;
             if(spawnIndex == spawnList.Count)
             {
                 spawnEnd = true;
                 return;
             }

             nextSpawnDelay = spawnList[spawnIndex].delay;
         }
     }*/
    public void ReadSpawnFile()
    {
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        TextAsset textFile = Resources.Load("Stage0") as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        while (stringReader != null)
        {
            string line = stringReader.ReadLine();
            Debug.Log(line);
            if (line == null)
            {
                break;
            }

            Spawn spawnData = new Spawn();
            spawnData.delay = float.Parse(line.Split(',')[0].Trim(), CultureInfo.InvariantCulture);
            string enemyTypeString = line.Split(',')[1];

            // 문자열을 EnemyType으로 변환
            if (Enum.TryParse(enemyTypeString, out EnemyType enemyType))
            {
                spawnData.type = enemyType;
            }
            //spawnData.type = (EnemyType)int.Parse(line.Split(",")[1]);
            spawnData.point = int.Parse(line.Split(',')[2]);
            spawnList.Add(spawnData);
        }

        stringReader.Close();

        nextSpawnDelay = spawnList[0].delay;
    }
}
