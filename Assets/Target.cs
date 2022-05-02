using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private void OnTriggerEnter() {
        print("Collision");
        SpawnTargets.Instance.DestroyTarget(transform);
        Destroy(gameObject);
    }
}
