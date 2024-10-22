using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePooler : ObjectPooler
{
    public static ProjectilePooler instance;
    protected override void Awake()
    {
        base.Awake();

        if (instance == null)
        {
            instance = this;
        }

    }
}
