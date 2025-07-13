using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent nav;
    private EnemyHealth health;
    private MyPlayerHealth PlayerHealth;
    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemyHealth>();
        PlayerHealth = player.GetComponent<MyPlayerHealth>();
    }


    void Update ()
    {
        if(health.startHealth>0&&PlayerHealth.startHealth>0)
        Move();
        else
        {
            nav.enabled = false;
        }
    }
    public void Move()
    {
        nav.SetDestination(player.transform.position);
    }
}
