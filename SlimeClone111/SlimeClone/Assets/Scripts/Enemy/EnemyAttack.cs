using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Enemy enemy;

    [SerializeField] GameObject buletObj_A;
    [SerializeField] GameObject buletObj_B;

    //Bullet currentBullet;

    private void Awake()
    {
        //currentBullet = GetComponent<Bullet>();
        enemy = GetComponent<Enemy>();
    }
    void Update()
    {
        Fire();
        Reload();
    }

    private float curShotDelay_A;
    private float curShotDelay_B;

    void Fire()
    {
        if(enemy.Type == EnemyType.M)
        {
            GameObject bulletObj = Instantiate(buletObj_A, transform.position, transform.rotation);
            Rigidbody2D rigid = bulletObj.GetComponent<Rigidbody2D>();
            Bullet bullet = bulletObj.GetComponent<Bullet>();
            if (curShotDelay_A < bullet.BasicDelay)
            {
                Destroy(bulletObj);
                return;
            }
            rigid.AddForce(Vector2.down * bullet.BasicSpeed, ForceMode2D.Impulse);
            curShotDelay_A = 0;
        }

        if (enemy.Type == EnemyType.L)
        {
            GameObject bulletObjR = Instantiate(buletObj_B, transform.position + Vector3.right * 0.18f, transform.rotation);
            GameObject bulletObjL = Instantiate(buletObj_B, transform.position + Vector3.left * 0.18f, transform.rotation);
            
            Rigidbody2D rigidR = bulletObjR.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidL = bulletObjL.GetComponent<Rigidbody2D>();

            Bullet bulletR = bulletObjR.GetComponent<Bullet>();
            Bullet bulletL = bulletObjL.GetComponent<Bullet>();

            if (curShotDelay_B < bulletR.BasicDelay || curShotDelay_B < bulletL.BasicDelay)
            {
                Destroy(bulletObjR);
                Destroy(bulletObjL);
                return;
            }
            rigidR.AddForce(Vector2.down * bulletR.BasicSpeed, ForceMode2D.Impulse);
            rigidL.AddForce(Vector2.down * bulletL.BasicSpeed, ForceMode2D.Impulse);
            curShotDelay_B = 0;
        }
    }

    void Reload()
    {
        curShotDelay_A += Time.deltaTime;
        curShotDelay_B += Time.deltaTime;
    }

}
