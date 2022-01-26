using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RPGAbility
{
    public string name;

    [Tooltip("0 -> Damage\n" + "1 -> Healing\n" + "2 -> Buff\n" + "3 -> Debuff\n")]
    public int type;

    public int quantity;

    [Tooltip("0 -> No condition\n" + "1 -> Poison\n")]
    public int statusCondition;

    [Tooltip("0 -> No buff/debuff\n" + "1 -> Damage\n" + "2 -> Speed\n")]
    public int buffType;

    public RPGAbility(string aName, int aType, int aQuantity, int aStatusCondition, int aBuffType)
    {
        name = aName;
        type = aType;
        quantity = aQuantity;
        statusCondition = aStatusCondition;
        buffType = aBuffType;
    }
}

public class Stats : MonoBehaviour
{
    public bool player;

    public RPGAbility[] abilities;

    public int attack;
}
