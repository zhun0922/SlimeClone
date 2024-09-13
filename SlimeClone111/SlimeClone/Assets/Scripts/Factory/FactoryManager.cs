using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FactoryManager : SingletonPattern.Singleton<FactoryManager>
{
    [SerializeField] Transform[] spawnPoints;
    public Transform[] SpawnPoints => spawnPoints;

    public const float MaxSpawnDelay = 1.4f;


    [SerializeField] float nextSpawnDelay = 1.5f;
    [SerializeField] float curSpawnDelay;

   /* public List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;

    private void Awake()
    {
        spawnList = new List<Spawn>();
    }

    public void ReadSpawnFile()
    {
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        TextAsset textFile = Resources.Load("Stage0") as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        while(stringReader != null)
        {
            string line = stringReader.ReadLine();
            if(line == null)
            {
                break;
            }

            Spawn spawnData = new Spawn();
            spawnData.delay = float.Parse(line.Split(',')[0]);
            string enemyTypeString = line.Split(',')[1];

            // 문자열을 EnemyType으로 변환
            if (Enum.TryParse(enemyTypeString, out EnemyType enemyType))
            {
                spawnData.type = enemyType;
            }
            spawnData.point = int.Parse(line.Split(',')[2]);
            spawnList.Add(spawnData);   
        }

        stringReader.Close();

        nextSpawnDelay = spawnList[0].delay;
    }*/
}
