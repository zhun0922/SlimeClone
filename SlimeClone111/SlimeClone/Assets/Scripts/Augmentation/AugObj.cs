using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AugObj : MonoBehaviour
{
    [SerializeField] AugmentType type;
    Rigidbody2D rigid;

    private float speed = GameManager.FixedSpeed;
    public float Speed
    {
        get { return speed; }

        set
        {
            speed = value;
        }
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rigid.velocity = Vector2.down * Speed;
    }

    public void ActiveEffect()
    {
        switch (type)
        {
            case AugmentType.Reborn:
                GameManager.Instance.HasReborn = true;
                break;
            case AugmentType.Arrow:
                AugEffect.Instance.ActiveArrow();
                break;
            case AugmentType.DoubleArrow:
                AugEffect.Instance.ActiveDoubleArrow();
                break;
            case AugmentType.Bird:
                AugEffect.Instance.ActiveBird();
                break;
            case AugmentType.RotateBlade:
                AugEffect.Instance.RoateBlade();
                break;
            case AugmentType.Attack5:
                AugEffect.Instance.Attack5();
                break;

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this);
        }
        if (collision.gameObject.tag == "BorderBullet")
        {
            Destroy(gameObject);
        }
    }

}
