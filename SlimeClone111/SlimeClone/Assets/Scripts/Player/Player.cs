using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  
    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    float lastDamageTime = 0f;
    void Update()
    {
        if (isTouchingEnemy)
        {
            lastDamageTime += Time.deltaTime;
            if (lastDamageTime >= meleeAttackInterval)
            {
                touchingEnemy.TakeDamage(MeleeDamage);
                lastDamageTime = 0f;
            }
        }

        Move();
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

    Coroutine meleeAttackCoroutine;
    bool isEffectActive = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.IsChildOf(transform))
        {
            return;
        }
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

        else if (collision.gameObject.tag == "Enemy")
        {
            //���⼭ ��ü ������Ʈ ���� �� GameManger.Instance.isMoveStop = true;
            if (!isTouchingEnemy)
            {
                GameManager.Instance.PauseMove();
                isTouchingEnemy = true;
                touchingEnemy = collision.GetComponent<Enemy>();
                lastDamageTime = meleeAttackInterval;
                //meleeAttackCoroutine = StartCoroutine(AttackEnemy(collision.gameObject));
            }

        }

        else if (collision.gameObject.tag == "EnemyBullet")
        {
            TakeDamage(collision.GetComponent<Bullet>().BasicDamage);
            //Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "AugObj" && !isEffectActive)
        {
            collision.GetComponent<AugObj>().ActiveEffect();
            isEffectActive = true;
        }
    }

    /*    private void OnTriggerStay(Collider collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                GameManager.Instance.PauseMove();
            }
        }*/

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.IsChildOf(transform))
        {
            return;
        }

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

        if (collision.gameObject.tag == "Enemy")
        {
            GameManager.Instance.ResumeMove();
            // ������ �浹�� ������ �ڷ�ƾ�� ����
            if (isTouchingEnemy)
            {
                isTouchingEnemy = false;
            }
        }

        if (collision.gameObject.tag == "AugObj")
        {
            isEffectActive = false;
        }
    }

    public void TakeDamage(float damage)
    {
        Hp -= damage;
        Debug.Log("�÷��̾� �ǰ�" + Hp);
        PlayHitAnimation();
    }


    public void Die()
    {
        if (GameManager.Instance.HasReborn)
        {
            AugEffect.Instance.RespawnPlayer();
        }
        GameManager.Instance.GameOver();
        gameObject.SetActive(false);
        //��������
        //���� �˻� (��Ȱ�ִ���) 
        //
        //������ ��Ȱ AugEffect.Instance.RespawnPlayer();
        //������ �������� else{
    }


    [SerializeField] float speed;

    private float meleeDamage = 5f;

    public float MeleeDamage
    {
        get { return meleeDamage; }
        set
        {
            meleeDamage = value;
        }
    }

    
    public const float MaxHp = 5000f;
    public const float BasicHp = 20f;
    private float hp = BasicHp;
    public float Hp
    {
        get { return hp; }
        set
        {
            hp = value;
            if (hp <= 0)
            {
                Die();
            }
            else if (hp > MaxHp)
            {
                hp = MaxHp;
            }
            /*
                        if (monsterPrefab != null)
                            monsterPrefab.UpdateHp();*/
        }

    }

  
    public void PlayHitAnimation()
    {
        StartCoroutine(HitAnimation());
    }

    private IEnumerator HitAnimation()
    {

        spriteRenderer.color = new Color(1, 0, 0, originalColor.a);

        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor;
    }

    private bool isToucRight;
    private bool isToucLeft;

    bool isTouchingEnemy = false;
    Enemy touchingEnemy;

    private float meleeAttackInterval = 0.5f;

    Animator anim;

    private Color originalColor;
    SpriteRenderer spriteRenderer;
}
