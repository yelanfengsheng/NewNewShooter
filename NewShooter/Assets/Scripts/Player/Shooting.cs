using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    
    float time = 0f;
    public float timeBetweenShoot = 0.15f;
    
    public float lightBetween= 0.1f;
    private AudioSource gunAudio;
    private Light LightShoot;
    private LineRenderer LineShoot;
    private ParticleSystem particle;
    private Ray shootRay;
    private RaycastHit hitEnemy;
    private RaycastHit hitRewardBox;
    private RaycastHit hitBuilding;
    public int damage = 10; //子弹伤害
    public int maxAmmo = 30; //最大弹药数
    public static int currentAmmo ; //当前弹药数
    private bool isReloading = false; //是否正在装填
    private AudioClip gunResert; //装填音效
    private bool canShoot = true;//是否可以射击
    private void Awake()
    {
        gunAudio = GetComponent<AudioSource>();
        LightShoot = GetComponent<Light>();
        LineShoot = GetComponent<LineRenderer>();
        particle = GetComponent<ParticleSystem>(); 
       
    }
    
    private void OnDisable()
    {
        CancelInvoke("Reload"); //取消重载方法的调用
    }
    private void Start()
    {
        currentAmmo = maxAmmo; //初始化当前弹药数为最大弹药数
    }

    // Update is called once per frame
    void Update()
    {


        time =time +Time.deltaTime;
        

        if (Input.GetButtonDown("Fire1")&&time>timeBetweenShoot&&canShoot ==true&&!isReloading)
        {
            Shoot();
            
        }
       if(time > timeBetweenShoot*lightBetween)
        {
            LightShoot.enabled = false;
            LineShoot.enabled = false ;
        }
        //如果按下R键则重新装填
        if (Input.GetKeyDown(KeyCode.R))
        {
          
            isReloading = true; //设置正在装填状态
            
            Invoke("Reload", 3);
            ResertAudioPlay();
         
        }

    }

    void Shoot()
    {
        currentAmmo--; //每次射击减少当前弹药数
       
        if (currentAmmo <=0) //如果当前弹药数为0则不再射击 播放无弹药音效
        {
            Debug.Log("没有弹药了");
            NoAmmo();
           
            currentAmmo = 0; //确保当前弹药数不小于0
            return;
            
        }
        //如果正在装填则不再射击
        if (isReloading)
        {
            Debug.Log("正在装填");
            return;
        }
        time = 0;
        //粒子效果
        particle.Play();
        //绘制子弹
        LightShoot.enabled = true;
        LineShoot.SetPosition(0, this.transform.position);
        //LineShoot.SetPosition(1, transform.position + this.transform.forward * 100);
        LineShoot.enabled = true ;
        //音效
        if(isReloading == false)
        {
            AudioClip gunClip = Resources.Load<AudioClip>("Audio/GunShoot"); //加载射击音效
           
            gunAudio.clip = gunClip; //设置音效
            gunAudio.Play(); //播放音效
        }
      
        //检测是否射线检测到Enemy
        shootRay.origin = this.transform.position;
        shootRay.direction = this.transform.forward;
        int layMask = LayerMask.GetMask("Enemy");
        int layMask2 = LayerMask.GetMask("RewardBox");
        int layMask3 = LayerMask.GetMask("Building");
        //检测到敌人
        if (Physics.Raycast(shootRay,out hitEnemy, 100, layMask))
        {
            LineShoot.SetPosition(1, hitEnemy.point);
            EnemyHealth enemyHealth = hitEnemy.collider.GetComponent<EnemyHealth>();
            enemyHealth.TakeDemage(damage,hitEnemy.point);
        }
        else if(Physics.Raycast(shootRay,out hitRewardBox,100,layMask2))
        {
            Debug.Log("Hit RewardBox");
            LineShoot.SetPosition(1, hitRewardBox.point);
            RewardBox rewardBox = hitRewardBox.collider.GetComponent<RewardBox>();
            rewardBox.TakeDemage(damage);
        }
        else if (Physics.Raycast(shootRay, out hitBuilding, 100, layMask3))
        {
            Debug.Log("Hit Building");
            LineShoot.SetPosition(1, hitBuilding.point);

        }
        //没检测到敌人
        else
        {
            LineShoot.SetPosition(1, transform.position + this.transform.forward * 100);
        }
    }
    //换弹方法
    public void Reload()
    {
      
        maxAmmo = 30; //重新装填
        currentAmmo = maxAmmo; //当前弹药数重置为最大弹药数
        Debug.Log("Reload");
        canShoot = true;
    isReloading = false; //装填完成
    }
  
    public void ResertAudioPlay()
    {
        //播放装填音效
        gunResert = Resources.Load<AudioClip>("Audio/GunResert"); //加载装填音效
        if (gunResert == null)
        {
            Debug.LogError("GunResert audio clip not found!");
            return;
        }
        gunAudio.clip = gunResert; //设置音效
        gunAudio.Play(); //播放音效
    }

    public void NoAmmo()
    {
        
        //播放无弹药音效
        AudioClip noAmmoClip = Resources.Load<AudioClip>("Audio/NoAmmo");
        
        gunAudio.clip = noAmmoClip; //设置音效
        gunAudio.Play(); //播放音效
    }
}
