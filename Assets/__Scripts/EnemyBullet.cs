using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyBullet : MonoBehaviour {
    [SerializeField] private float bulletSpeed = 7f;
	private Rigidbody rb;
	private WeaponController target;
	private Vector3 moveDirection;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
        target=GameObject.FindObjectOfType<WeaponController>();
		moveDirection = (target.transform.position - transform.position).normalized * bulletSpeed;
        
		rb.velocity = new Vector3 (moveDirection.x, moveDirection.y,moveDirection.z);
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.CompareTag ("Player")) {
			Debug.Log ("Hit!");
		}
        else if(col.CompareTag("Saber")){
            print("Saber");
            var opposite = -rb.velocity;
            var brakePower = 10;
            var brakeForce = opposite.normalized * brakePower;
            //rb.AddForce(brakeForce * Time.deltaTime);
            rb.velocity = new Vector3 (-moveDirection.x, -moveDirection.y,-moveDirection.z);
        }
	}
}