using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private int MoveSpeed = 4;
    [SerializeField] private int MaxDist = 10;

    //look at and move toward player
    void Update()
    {
        transform.LookAt(Player);

        transform.position += transform.forward * MoveSpeed * Time.deltaTime;

        //if within distance of player, game over
        if (Vector3.Distance(transform.position, Player.position) <= MaxDist)
        {
            MenuManager.Instance.GameOverMenu();
        }

    }

    
}

