using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;

    [SerializeField] private float timer;

    [SerializeField] private float delay;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", delay, timer);
    }


    //  
    void Spawn()
    {
        //pick random enemy
        int randomEnemy = Random.Range(0, enemies.Length);

        //spawn enemy at location of spawner
        GameObject enemy = Instantiate(enemies[randomEnemy], transform.position, Quaternion.Euler(0, 0, 90));
    }
    
}
