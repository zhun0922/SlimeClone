using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AugEffect : SingletonPattern.Singleton<AugEffect>
{
    private GameObject playerObj;

    private void Start()
    {
        playerObj = GameManager.Instance.PlayerObj;
    }
    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerExe", 2f);
    }

    public void RespawnPlayerExe()
    {
        playerObj.transform.position = Vector3.down * 3.5f;
        playerObj.SetActive(true);
    }
}
