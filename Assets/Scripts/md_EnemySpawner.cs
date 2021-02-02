using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class md_EnemySpawner : MonoBehaviour 
{

    public enum SpawnState { spawn, wait, count }

    public float timer = 2f;
    public GameObject[] enemyPrefab;

    md_CapsuleBonus capsule;
    Vector3 LastEnemyPosition;

    public SpawnState state = SpawnState.count;

    void Start()
    {
        capsule = gameObject.GetComponent<md_CapsuleBonus>();
    }

    void Update()
    {
        if (GameManager.state == GameManager.States.play)
        {
            if (state == SpawnState.wait)
            {
                // Check last enemy position and drop capsule if 0 enemys
                md_LastEnemy();
            }

            if (state != SpawnState.spawn)
            {
                if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
                {
                    // Trigger a wave
                    StartCoroutine(md_SpawnEnemies());
                }
            }
        }
    }

    IEnumerator md_SpawnEnemies() 
    {
        state = SpawnState.spawn;

        int enemySpawnRange = Random.Range(3, 5);

        yield return new WaitForSeconds(2f);
        // Instantiate enemys 
        for (int i = 0; i < enemySpawnRange; i++)
          Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length)], new Vector3(Random.Range(5f, -5f), transform.position.y), Quaternion.Euler(180,0,0));

        state = SpawnState.wait;
    }

    void md_LastEnemy()
    {
        // Keep last enemy position
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 1)
        {
            LastEnemyPosition = GameObject.FindWithTag("Enemy").transform.position;
        }
        // Drop bonus on last enemy position (call method from capsuleBonus class)
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            //Debug.Log("On drop un truc ici" + LastEnemyPosition);
            capsule.md_GetBonus(LastEnemyPosition);
        }
    }


} // class




































