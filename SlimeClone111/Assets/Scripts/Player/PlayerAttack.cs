using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] GameObject buletObj_Arrow;
    [SerializeField] GameObject buletObj_Bird;

    Bullet currentBullet;
    private float power;

    [SerializeField] bool hasArrow;
    [SerializeField] bool hasBird;

    private void Awake()
    {
        currentBullet = GetComponent<Bullet>();
    }
    void Update()
    {
        Fire();
        Reload();
    }

    private float curShotDelay_Arrow;
    private float curShotDelay_Bird;
    void Fire()
    {
        //loadedPrefab = Resources.Load<GameObject>(PREFAB_PATH + "/Monster_" + id)?.GetComponent<MonsterPrefab>();
        if (hasArrow)
        {
            GameObject bulletObj = Instantiate(buletObj_Arrow, transform.position + Vector3.up* 0.2f, transform.rotation);
            Rigidbody2D rigid = bulletObj.GetComponent<Rigidbody2D>();
            Bullet bullet = bulletObj.GetComponent<Bullet>();
            if (curShotDelay_Arrow < bullet.BasicDelay)
            {
                Destroy(bulletObj);
                return;
            }
            rigid.AddForce(Vector2.up * bullet.BasicSpeed, ForceMode2D.Impulse);
            curShotDelay_Arrow = 0;
        }

        if (hasBird)
        {
            GameObject bulletObj = Instantiate(buletObj_Bird, transform.position + Vector3.down * 0.8f, transform.rotation);
            Rigidbody2D rigid = bulletObj.GetComponent<Rigidbody2D>();
            Bullet bullet = bulletObj.GetComponent<Bullet>();
            if (curShotDelay_Bird < bullet.BasicDelay)
            {
                Destroy(bulletObj);
                return;
            }
            rigid.AddForce(Vector2.up * bullet.BasicSpeed, ForceMode2D.Impulse);
            curShotDelay_Bird = 0;
        }
    }

    void Reload()
    {
        curShotDelay_Arrow += Time.deltaTime;
        curShotDelay_Bird  += Time.deltaTime;
    }

    /*  void SetBullet(string ID)
      {
          currentBullet = 
      }*/


}
