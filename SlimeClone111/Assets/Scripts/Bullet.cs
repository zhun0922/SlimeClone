using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] string id;
    public string ID => id;

    [SerializeField] float basicSpeed;
    public float BasicSpeed => basicSpeed;

    [SerializeField] float basicPower;
    public float BasicPower => basicPower;

    [SerializeField] float basicDelay;
    public float BasicDelay => basicDelay;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BorderBullet")
        {
            Destroy(gameObject);
        }
    }
}
