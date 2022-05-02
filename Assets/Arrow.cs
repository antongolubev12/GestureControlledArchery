using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float damage;

    [SerializeField] private float torque;

    private Rigidbody rb;

    private string enemyTag;

    private bool hit=false;

    private void Start() {
        rb=gameObject.GetComponent<Rigidbody>();
    }

    public void SetEnemyType(string enemyTag){
        this.enemyTag=enemyTag;
    }

    public void Fly(Vector3 force){
        rb.isKinematic=false;
        rb.AddForce(force,ForceMode.Impulse);
        //add rotation
        rb.AddForce(transform.right * torque);
        transform.SetParent(null);
    }

    private void OnTriggerEnter(Collider other) {
        if(hit)return;

        hit=true;

        //print("collision");
        
        //do damage
        if(other.CompareTag(enemyTag)){
            
        }

        rb.velocity=Vector3.zero;
        rb.angularVelocity= Vector3.zero;
        rb.isKinematic=true;
        transform.SetParent(other.transform);
    }

}
