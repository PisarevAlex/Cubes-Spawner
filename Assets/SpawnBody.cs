using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBody : MonoBehaviour
{
    [SerializeField] private Spawner spawner;
    [SerializeField] private BumpAnimation bumpAnimation;

    public void OnInstantiate(Spawner spawner)
    {
        this.spawner = spawner;
    }

    private void Update()
    {
        transform.position += Vector3.forward * spawner.Speed.Value / 100f;

        if (Vector3.Distance(transform.position, spawner.transform.position) >= spawner.Distance.Value)
        {
            bumpAnimation.enabled = false;
        }
    }
}
