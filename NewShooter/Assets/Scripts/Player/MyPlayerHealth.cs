using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyPlayerHealth : MonoBehaviour
{
    public int startHealth = 50;
    public int maxHealth = 50;
    public bool isDead = false;
    private AudioSource Audio;
    public AudioClip deadAudio;
    private Animator animator;
    private PlayerMovement move;
    private Shooting shoot;
    private GameObject canvas;
    private float time = 0f;
    private float maxTime = 3f;
    
    private void Awake()
    {
        Audio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        move = GetComponent<PlayerMovement>();
        shoot = GetComponentInChildren<Shooting>();
        canvas = GameObject.Find("Canvas");
       

    }

    // Update is called once per frame
    void Update()
    {
        
        if (isDead)
        {
            time = time + Time.deltaTime;
            Debug.Log(time);
            //死亡后玩家不能再移动
            move.enabled = false;
            //死亡后玩家不能射击
            shoot.enabled = false;
            RestartLevel();
        }

    }
    public void TakeDamage(int value)
    {
        //死亡之后不再受伤
        if(isDead) return;  
        startHealth-=value;
        //受伤音效
        Audio.Play();
        //受伤的粒子效果

        //受伤动画
        //伤害过多死亡
        if(startHealth <=0)
        {
            
            Dead();
        }
        
    }
    private void Dead()
    {
        isDead = true;
        //死亡动画的播放
        animator.SetTrigger("IsDead");
        Debug.Log("玩家死亡");
        //死亡音效的播放
        Audio.clip = deadAudio;
        Audio.Play();
       


    }
    public void RestartLevel()
    {
        if (time >= maxTime)
        //重启
        {
            AddScore.score = 0; //重置分数
            SceneManager.LoadScene(0);
        }
        //弹出重新启动的ui界面
     
    }

    public  void AddHealth(int value)
    {  if(startHealth ==maxHealth )
        {
            return; // 如果当前生命值已经是最大值，则不增加
        }
        startHealth += value;
    }
}
