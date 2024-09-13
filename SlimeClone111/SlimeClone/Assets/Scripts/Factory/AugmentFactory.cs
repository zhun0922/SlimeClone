using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AugmentFactory : SingletonPattern.Singleton<AugmentFactory>
{
    [SerializeField] List<GameObject> augmentObjs;
    [SerializeField] Transform[] spawnPoints;

    public void SpawnAugment()
    {
        int augmentIndex1 = UnityEngine.Random.Range(0, augmentObjs.Count);
        int augmentIndex2;

        do
        {
            augmentIndex2 = UnityEngine.Random.Range(0, augmentObjs.Count);
        }
        while (augmentIndex2 == augmentIndex1);

        GameObject aug1 = Instantiate(augmentObjs[augmentIndex1],
                     spawnPoints[0].position,
                     spawnPoints[0].rotation);

        GameObject aug2 = Instantiate(augmentObjs[augmentIndex2],
                    spawnPoints[1].position,
                    spawnPoints[1].rotation);

        AugmentManager.Instance.AddListAugment(aug1.GetComponent<AugObj>());
        AugmentManager.Instance.AddListAugment(aug2.GetComponent<AugObj>());
    }
}
