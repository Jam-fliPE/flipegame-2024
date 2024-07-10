using System;
using System.Collections;
using UnityEngine;

public class EnemyHealthController : HealthController
{
    [SerializeField]
    private Renderer _renderer;

    protected override void OnDie()
    {
        Destroy(gameObject, 2.0f);
    }
}
