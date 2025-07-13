using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int startHealth = 100;
    private ParticleSystem particle;
    private AudioSource audioSource;
    public  AudioClip audioDead;
    private Animator animator;
    private SphereCollider sphereCollider;
    private bool isSiking = false;
    private bool isDead = false;
    private int beraScore = 15; 
    private int helephontScore = 30; //大象分数 
    private int zombieScore = 5; //僵尸分数

    void Awake()
    {
       audioSource = GetComponent<AudioSource>();
       particle = GetComponentInChildren<ParticleSystem>();
       animator = GetComponent<Animator>();
       sphereCollider =GetComponent<SphereCollider>();
      
    }


    void Update()
    {
        if(isSiking)
        {
            transform.Translate(-Vector3.up * Time.deltaTime*2f);
            
        }
        

    }
    public void TakeDemage(int value, Vector3  hitpoint)
    {
        if (isDead) return; //如果已经死亡则不再受伤
        //播放音乐
        audioSource.Play();
        //播放粒子效果
        particle.transform.position = hitpoint;
        particle.Play();
        startHealth = startHealth- value;
        if (startHealth <= 0)
        {
           
            Dead();
        }
    }

    private void Dead()
    {
        isDead = true;
        if(gameObject.CompareTag("Bera"))
        {
            AddScore.score += beraScore; //增加分数
        }
        else if(gameObject.CompareTag("Helephont"))
        {
            AddScore.score += helephontScore; //增加分数
        }
        else if(gameObject.CompareTag("Zombie"))
        {
            AddScore.score += zombieScore; //增加分数
        }
       
         
        
        //播放死亡动画
        animator.SetTrigger("IsDead");
        //播放音效
        audioSource.clip = audioDead;
        audioSource.Play();
        //尸体不能再挡子弹
        sphereCollider.enabled = false;
        Debug.Log("敌人死亡");

        //物理检测关闭 减少性能消耗
        GetComponent<Rigidbody>().isKinematic = true;
       StartSinking();
       

    }
    public void StartSinking()
    {
       
        isSiking =true;
        Destroy(gameObject, 2f);
    }
}
