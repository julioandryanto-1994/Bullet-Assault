using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPooler : ObjectPooler
{
    public static EnemyPooler instance;

    protected override void Awake()
    {
        base.Awake();
        if (instance == null)
        { 
            instance = this;
        }
    }
}
