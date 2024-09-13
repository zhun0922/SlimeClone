using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Wallet : MonoBehaviour
{

    public const float MaxGold = 5000f;
    private float gold = 0f;
    public float Gold
    {
        get { return gold; }
        set
        {
            gold = value;
            if (gold <= 0)
            {
                gold = 0;
            }
            else if (gold > MaxGold)
            {
                gold = MaxGold;
            }
        }

    }
}
