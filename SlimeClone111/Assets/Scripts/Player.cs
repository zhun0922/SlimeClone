using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    private bool isToucRight;
    private bool isToucLeft;

    [SerializeField] GameObject buletObj_Basic;
    [SerializeField] GameObject buletObj_B;

    Animator anim;

    Bullet currentBullet;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        currentBullet = GetComponent<Bullet>();
    }

    void Update()
    {
        Move();
        Fire();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if ((isToucRight && h == 1) || (isToucLeft && h == -1))
        {
            h = 0;
        }
        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, 0, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;

        if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
        {
            anim.SetInteger("Input", (int)h);
        }
    }

    void Fire()
    {
        //loadedPrefab = Resources.Load<GameObject>(PREFAB_PATH + "/Monster_" + id)?.GetComponent<MonsterPrefab>();
        GameObject bullet = Instantiate(buletObj_Basic, transform.position, transform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
    }

  /*  void SetBullet(string ID)
    {
        currentBullet = 
    }*/

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Right":
                    isToucRight = true;
                    break;
                case "Left":
                    isToucLeft = true;
                    break;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Right":
                    isToucRight = false;
                    break;
                case "Left":
                    isToucLeft = false;
                    break;
            }
        }
    }
}
