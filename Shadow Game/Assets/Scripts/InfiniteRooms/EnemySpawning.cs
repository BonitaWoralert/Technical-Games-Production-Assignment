using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public EnemyList enemyList;
    public List<GameObject> spawnLocations;
    private void Start()
    {
        enemyList = FindObjectOfType<EnemyList>();
        Spawn();
    }

    private void Spawn()
    {
        foreach (var i in spawnLocations)
        {
            Instantiate(enemyList.enemies[UnityEngine.Random.Range(0, enemyList.enemies.Count)], i.transform.position, Quaternion.identity);
        }
    }
}
