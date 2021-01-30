using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class md_EnemySpawner : MonoBehaviour 
{

    public enum SpawnState { spawn, wait, count }

    public GameObject[] asteroid_Prefabs;
    public GameObject[] enemyPrefab;

    public float timer = 2f;
    Vector3 LastEnemyPosition;

    public SpawnState state = SpawnState.count;

    void Update()
    {
        if (state == SpawnState.wait)
        {
            md_LastEnemy();
        }

        if (state != SpawnState.spawn)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                // Trigger a wave
                md_SpawnEnemies();
            }
        }
    }

    void md_SpawnEnemies() 
    {
        state = SpawnState.spawn;
        for (int i = 0; i < 5; i++)
          Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length)], new Vector3(Random.Range(5f, -5f), transform.position.y), Quaternion.identity);

        state = SpawnState.wait;
        //Invoke("SpawnEnemies", timer);
    }

    void md_LastEnemy()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 1)
        {
            LastEnemyPosition = GameObject.FindWithTag("Enemy").transform.position;
        }
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            Debug.Log("On drop un truc ici" + LastEnemyPosition);
        }
    }


} // class




































