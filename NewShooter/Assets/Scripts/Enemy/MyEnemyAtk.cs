using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MyEnemyAtk : MonoBehaviour
{
    public int enemyAtk = 5;
    private GameObject player;
    private MyPlayerHealth playerHealth;
    private bool isRange ;
    private float time = 0;
    private float timeBetween = 2;
    private Animator animator;
    private EnemyMovement enemyMove;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //通过脚本player获得player上的脚本
        playerHealth = player.GetComponent<MyPlayerHealth>();
        animator = GetComponent<Animator>();
        enemyMove = GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        time += Time.deltaTime;
        if (isRange&&!playerHealth.isDead)
        {
            if (time > timeBetween)
            {
                //进入攻击距离实行攻击
                Atk();
            }

        }
        if (playerHealth.isDead)
        {
            //不在攻击 动画变为ideo
            animator.SetTrigger("PlayerDead");
            //不再以玩家为目标进行移动
            enemyMove.enabled = false;
        }

    }

    private void Atk()
    {
        //敌人攻击玩家扣血
       playerHealth.TakeDamage(enemyAtk);
        //不能连续的攻击两次
        time = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject==player)
        {
           
            isRange = true;
           
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            isRange = false;
            
        }
    }
}
