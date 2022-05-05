using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{   
    [SerializeField] public float score;

    private void OnTriggerEnter() {
        print("Destroying "+gameObject.name);
        AudioManager.Instance.PlayHit();
        SpawnTargets.Instance.DestroyTarget();
        Score.Instance.SetScore(score);
        Destroy(gameObject);
    }
}
