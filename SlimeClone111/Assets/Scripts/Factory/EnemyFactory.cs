using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour, IFactoryInterface
{
    [SerializeField] GameObject[] enemyObjs;

    public float curSpawnDelay { get; set; }

    private void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if(curSpawnDelay > FactoryManager.MaxSpawnDelay)
        {
            Spawn();
            //maxSpawnDelay =Random.Range(0.5f, 3f);
            curSpawnDelay = 0;

        }
    }
    
    public void Spawn()
    {
        if (!GameManager.Instance.IsPauseMove)
        {
            int ranEnemy = Random.Range(0, 3);
            int ranPoint = Random.Range(0, 5);

            Enemy enemy = Instantiate(enemyObjs[ranEnemy],
                        FactoryManager.Instance.SpawnPoints[ranPoint].position,
                        FactoryManager.Instance.SpawnPoints[ranPoint].rotation).GetComponent<Enemy>();

            EnemyManager.Instance.AddListEnemy(enemy);
        }
    }
}
