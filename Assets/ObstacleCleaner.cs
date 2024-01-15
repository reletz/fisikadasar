using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCleaner : MonoBehaviour
{
    public GameObject Target;
    public float MaxRange;

    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void FixedUpdate()
    {
        if (MathF.Abs(gameObject.transform.position.x - Target.transform.position.x) > MaxRange)
        {
            Destroy(gameObject);
        }
    }
}
