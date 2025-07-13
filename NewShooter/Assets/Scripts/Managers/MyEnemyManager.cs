using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEnemyManager : MonoBehaviour
{
    public GameObject Enemy;
    public Transform SpawnPoint;
    public float beginTime = 1f;
    public float repeatTime = 3f;

    private void Start()
    {
        InvokeRepeating("Spawn",beginTime, repeatTime);
    }

    public void Spawn()
    {
        
        Instantiate(Enemy,SpawnPoint.position,SpawnPoint.rotation);
    }
}
