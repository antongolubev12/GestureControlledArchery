using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightsaber : MonoBehaviour
{
    [SerializeField] private float damage;

    [SerializeField] private string enemyTag;

    private void OnTriggerEnter(Collider other) {

        print("collision");
        
        //do damage
        if(other.CompareTag(enemyTag)){
            var health = other.GetComponent<HealthController>();
            health.ApplyDamage(damage);
        }

    }
}
