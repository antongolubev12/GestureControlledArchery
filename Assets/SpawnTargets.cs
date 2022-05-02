using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private List <Transform> currentTargets = new List<Transform>();

    // Start is called before the first frame update
    private void Start() {

        Spawn(numberOfTargets);
    }

    public void Spawn(int targetsToSpawn){
        for(int i=0; i<=targetsToSpawn;i++)
        {   
            //pick a random spawnpoint from the available points
            int randomSpawn=Random.Range(0,spawnPoints.Length);
            //remove that spawnpoint from the list of available pounts

            if(currentTargets.Contains(spawnPoints[randomSpawn])){
                randomSpawn=Random.Range(0,spawnPoints.Length);
            }
            
            //pick a random target to spawn
            int randomTarget= Random.Range(0,targets.Length);

            if(spawnPoints[randomSpawn]==null){
                randomSpawn-=1;
            }

            //spawn it and rotate it 90 degrees
            GameObject spawnedObj= Instantiate(targets[randomTarget], spawnPoints[randomSpawn].position, Quaternion.Euler(0, 0, 90));


            //spawnedObj.transform.position = new Vector3(spawnedObj.transform.position.x, spawnedObj.transform.position.y, 0);

            //add object to list of current targets
            currentTargets.Add(spawnedObj.transform);

            print("Spawned at : "+randomSpawn);
            i++;
        }
    }

    public void DestroyTarget(Transform target){
        currentTargets.Remove(target);
        Spawn(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
