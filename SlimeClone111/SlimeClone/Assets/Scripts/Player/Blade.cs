using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public float damage;
    public int per; //°üÅë


    public void Init(float damage, int per)
    {
        this.damage = damage;
        this.per = per;
    }
}
