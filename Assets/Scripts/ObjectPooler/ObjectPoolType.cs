using System;
using UnityEngine;

[Serializable]
public class ObjectPoolType
{
    public Types type;
    public GameObject prefab;
    public int amount;

    public ObjectPoolType(Types type, GameObject prefab, int amount)
    {
        this.type = type;
        this.prefab = prefab;
        this.amount = amount;
    }
}
