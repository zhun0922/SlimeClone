using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyType type;
    public EnemyType Type => type;

    private float speed = GameManager.FixedSpeed;
    public float Speed
    {
        get { return speed; }

        set
        {
            speed = value;
        }
    }
    [SerializeField] float health;
    [SerializeField] float meleeDamage;

    SpriteRenderer spriteReneder;
    [SerializeField] Sprite[] sprites;
    Rigidbody2D rigid;

    private void Awake()
    {
        spriteReneder = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        //rigid.velocity = Vector2.down * Speed;
        
    }

    float lastDamageTime = 0f;
    private void Update()
    {
        if (isTouchingPlayer)
        {
            lastDamageTime += Time.deltaTime;
            if (lastDamageTime >= meleeAttackInterval)
            {
                player.TakeDamage(meleeDamage);
                lastDamageTime = 0f;
            }
        }
    }

    private void FixedUpdate()
    {
        rigid.velocity = Vector2.down * Speed;
    }

    public void TakeDamage(float dmg)
    {
        Debug.Log("적 피격" + health);
        health -= dmg;
        spriteReneder.sprite = sprites[1];
        Invoke("ReturnSprite", 0.1f);

        if(health <= 0)
        {
            EnemyManager.Instance.DeleteListEnemy(this);
            Destroy(gameObject);
        }
    }

    void ReturnSprite()
    {
        spriteReneder.sprite = sprites[0];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BorderBullet")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "PlayerBullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            TakeDamage(bullet.BasicDamage);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Player") //에너미 속도 일시정지
        {
            if (isTouchingPlayer)
            {
                isTouchingPlayer = true;
                lastDamageTime = meleeAttackInterval;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            if (isTouchingPlayer)
            {
                isTouchingPlayer = false;
            }
        }
    }

    bool isTouchingPlayer = false;
    Player player = GameManager.Instance.Player;

    private float meleeAttackInterval = 0.5f;
}
