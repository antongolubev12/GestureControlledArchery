using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float damage;

    [SerializeField] private float torque;

    private Rigidbody rb;

    private string enemyTag;

    private void Start() {
        rb=gameObject.GetComponent<Rigidbody>();
    }

    public void SetEnemyType(string enemyTag){
        this.enemyTag=enemyTag;
    }

    public void Fly(Vector3 force){
        rb.isKinematic=false;
        //send arrow
        rb.AddForce(force,ForceMode.Impulse);
        
        //add rotation
        rb.AddForce(transform.right * torque);
        transform.SetParent(null);
    }

    private void OnTriggerEnter(Collider other) {
        print("collision");
        
        //do damage only to enemy tag
        if(other.CompareTag(enemyTag)){
            var health = other.GetComponent<HealthController>();
            health.ApplyDamage(damage);
            AudioManager.Instance.PlayHit();
        }

        Destroy(gameObject);
    }

}
 