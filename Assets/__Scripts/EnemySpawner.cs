using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;

    [SerializeField] private float timer;

    [SerializeField] private int targetsToSpawn;

    [SerializeField] private float delay;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", delay, timer);
    }

    // Update is called once per frame

    void Spawn()
    {

        int randomEnemy = Random.Range(0, enemies.Length);

        GameObject enemy = Instantiate(enemies[randomEnemy], transform.position, Quaternion.Euler(0, 0, 90));
    }
    
}
