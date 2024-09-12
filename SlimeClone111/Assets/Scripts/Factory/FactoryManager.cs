using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryManager : SingletonPattern.Singleton<FactoryManager>
{
    [SerializeField] Transform[] spawnPoints;
    public Transform[] SpawnPoints => spawnPoints;

    public const float MaxSpawnDelay = 1.4f;

    public List<Enemy> FieldEnemyList = new List<Enemy>(); 

/*    public bool isEnemySpawn;
    public bool 

*/
    [SerializeField] float maxSpawnDelay = 1.5f;
    [SerializeField] float curSpawnDelay;
    public virtual void Spawn() { }

}
