using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{   
    [SerializeField] public float score;

    private void OnTriggerEnter() {
        print("Collision");
        SpawnTargets.Instance.DestroyTarget(transform);
        Score.Instance.SetScore(score);
        Destroy(gameObject);
    }
}
