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
    public int damage = 10; //�ӵ��˺�
    public int maxAmmo = 30; //���ҩ��
    public static int currentAmmo ; //��ǰ��ҩ��
    private bool isReloading = false; //�Ƿ�����װ��
    private AudioClip gunResert; //װ����Ч
    private bool canShoot = true;//�Ƿ�������
    private void Awake()
    {
        gunAudio = GetComponent<AudioSource>();
        LightShoot = GetComponent<Light>();
        LineShoot = GetComponent<LineRenderer>();
        particle = GetComponent<ParticleSystem>(); 
       
    }
    
    private void OnDisable()
    {
        CancelInvoke("Reload"); //ȡ�����ط����ĵ���
    }
    private void Start()
    {
        currentAmmo = maxAmmo; //��ʼ����ǰ��ҩ��Ϊ���ҩ��
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
        //�������R��������װ��
        if (Input.GetKeyDown(KeyCode.R))
        {
          
            isReloading = true; //��������װ��״̬
            
            Invoke("Reload", 3);
            ResertAudioPlay();
         
        }

    }

    void Shoot()
    {
        currentAmmo--; //ÿ��������ٵ�ǰ��ҩ��
       
        if (currentAmmo <=0) //�����ǰ��ҩ��Ϊ0������� �����޵�ҩ��Ч
        {
            Debug.Log("û�е�ҩ��");
            NoAmmo();
           
            currentAmmo = 0; //ȷ����ǰ��ҩ����С��0
            return;
            
        }
        //�������װ���������
        if (isReloading)
        {
            Debug.Log("����װ��");
            return;
        }
        time = 0;
        //����Ч��
        particle.Play();
        //�����ӵ�
        LightShoot.enabled = true;
        LineShoot.SetPosition(0, this.transform.position);
        //LineShoot.SetPosition(1, transform.position + this.transform.forward * 100);
        LineShoot.enabled = true ;
        //��Ч
        if(isReloading == false)
        {
            AudioClip gunClip = Resources.Load<AudioClip>("Audio/GunShoot"); //���������Ч
           
            gunAudio.clip = gunClip; //������Ч
            gunAudio.Play(); //������Ч
        }
      
        //����Ƿ����߼�⵽Enemy
        shootRay.origin = this.transform.position;
        shootRay.direction = this.transform.forward;
        int layMask = LayerMask.GetMask("Enemy");
        int layMask2 = LayerMask.GetMask("RewardBox");
        int layMask3 = LayerMask.GetMask("Building");
        //��⵽����
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
        //û��⵽����
        else
        {
            LineShoot.SetPosition(1, transform.position + this.transform.forward * 100);
        }
    }
    //��������
    public void Reload()
    {
      
        maxAmmo = 30; //����װ��
        currentAmmo = maxAmmo; //��ǰ��ҩ������Ϊ���ҩ��
        Debug.Log("Reload");
        canShoot = true;
    isReloading = false; //װ�����
    }
  
    public void ResertAudioPlay()
    {
        //����װ����Ч
        gunResert = Resources.Load<AudioClip>("Audio/GunResert"); //����װ����Ч
        if (gunResert == null)
        {
            Debug.LogError("GunResert audio clip not found!");
            return;
        }
        gunAudio.clip = gunResert; //������Ч
        gunAudio.Play(); //������Ч
    }

    public void NoAmmo()
    {
        
        //�����޵�ҩ��Ч
        AudioClip noAmmoClip = Resources.Load<AudioClip>("Audio/NoAmmo");
        
        gunAudio.clip = noAmmoClip; //������Ч
        gunAudio.Play(); //������Ч
    }
}
