using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AugEffect : SingletonPattern.Singleton<AugEffect>
{
    private GameObject playerObj;
    private Player player;

    [SerializeField] GameObject rotateBladeTarget;
    [SerializeField] GameObject blade;
    private void Start()
    {
        playerObj = GameManager.Instance.PlayerObj;
        player = GameManager.Instance.Player;
    }
    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerExe", 2f);
    }

    public void RespawnPlayerExe()
    {
        playerObj.transform.position = Vector3.down * 1f;
        
        Player player = GameManager.Instance.Player;
        player.Hp = Player.BasicHp;
        playerObj.SetActive(true);
    }

    public void ActiveArrow()
    {
        player.GetComponent<PlayerAttack>().HasArrow = true;
    }
    public void ActiveDoubleArrow()
    {
        player.GetComponent<PlayerAttack>().HasDoubleArrow = true;
    }
    public void ActiveBird()
    {
        player.GetComponent<PlayerAttack>().HasBird = true;
    }

    public void RoateBlade()
    {
        Instantiate(blade, rotateBladeTarget.transform.position + Vector3.up * 0.8f,
        rotateBladeTarget.transform.rotation, rotateBladeTarget.transform);

    }


    public void Attack5()
    {
        player.MeleeDamage += 5;
    }

}
