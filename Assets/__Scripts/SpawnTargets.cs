using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnTargets : MonoBehaviour
{
    private static SpawnTargets _instance;

    public static SpawnTargets Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private GameObject[] targets;

    [SerializeField] private int numberOfTargets;

    private int maxTargets=10;

    public List<Transform> availableSpawnPoints;

    int count;

    //public List <Transform> currentTargets = new List<Transform>();

    // Start is called before the first frame update
    private void Start() {
        InvokeRepeating("Spawn",0f,5f);
        //Spawn(numberOfTargets);
        count=0;
    }

    public void Spawn(){
        if(maxTargets<count) return;
        
        int targetsToSpawn=3;

        for(int i=1; i<=targetsToSpawn;i++)
        {   
            //pick a random target to spawn
            int randomTarget= Random.Range(0,targets.Length);

            int randomPoint= Random.Range(0,spawnPoints.Length);


            //spawn it and rotate it 90 degrees
            GameObject spawnedObj= Instantiate(targets[randomTarget], spawnPoints[randomPoint].position, Quaternion.Euler(0, 0, 90));

            spawnedObj.transform.position = new Vector3(spawnedObj.transform.position.x, spawnedObj.transform.position.y, spawnedObj.transform.position.z);


            //add object to list of current targets
            //currentTargets.Add(spawnedObj.transform);

            print("Spawned at : "+count);
            count++;
        }
    }

    public void DestroyTarget(){
        count--;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
