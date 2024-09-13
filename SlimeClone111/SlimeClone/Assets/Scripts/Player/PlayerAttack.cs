using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] GameObject buletObj_Arrow;
    [SerializeField] GameObject buletObj_Bird;

    Bullet currentBullet;
    private float power;

    public bool HasArrow = false;
    public bool HasDoubleArrow = false;
    public bool HasBird = false ;

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
        if (HasArrow)
        {
            if (HasDoubleArrow)
            {
                GameObject bulletObjR = Instantiate(buletObj_Arrow, transform.position + Vector3.right * 0.16f, transform.rotation);
                GameObject bulletObjL = Instantiate(buletObj_Arrow, transform.position + Vector3.left * 0.16f, transform.rotation);
                Rigidbody2D rigidR = bulletObjR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidL = bulletObjL.GetComponent<Rigidbody2D>();

                Bullet bulletR = bulletObjR.GetComponent<Bullet>();
                Bullet bulletL = bulletObjL.GetComponent<Bullet>();

                if (curShotDelay_Arrow < bulletR.BasicDelay || curShotDelay_Arrow < bulletL.BasicDelay)
                {
                    Destroy(bulletObjR);
                    Destroy(bulletObjL);
                    return;
                }
                rigidR.AddForce(Vector2.up * bulletR.BasicSpeed, ForceMode2D.Impulse);
                rigidL.AddForce(Vector2.up * bulletL.BasicSpeed, ForceMode2D.Impulse);
                curShotDelay_Arrow = 0;
            }
            else
            {
                GameObject bulletObj = Instantiate(buletObj_Arrow, transform.position + Vector3.up * 0.2f, transform.rotation);
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
        }

        if (HasBird)
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
