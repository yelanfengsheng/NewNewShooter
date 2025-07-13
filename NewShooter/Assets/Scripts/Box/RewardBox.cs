using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class RewardBox : MonoBehaviour
{
    public int startHealth = 20; // 奖励箱的初始生命值
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDemage(int value)
    {
       startHealth -= value; // 减少奖励箱的生命值
         if (startHealth <= 0)
         {
              Dead(); // 如果生命值小于等于0，调用死亡方法
        }
    }
    private void Dead()
    {
        Destroy(gameObject, 0f); //立即销毁奖励箱
        //随机在奖励箱位置生成奖励
        RandomReward();

    }
    private void RandomReward()
    {
        int randomReward = UnityEngine.Random.Range(1, 100); // 生成1到100之间的随机数
        if (randomReward <= 30)
        {
            // 生成医疗包
            GameObject heart = Resources.Load<GameObject>("Heart");
            Instantiate(heart, transform.position, Quaternion.identity);

        }
        else if (randomReward <= 60)
        {
            // 短暂生成一把枪支
            GameObject newGan = Resources.Load<GameObject>("Gan");
            Instantiate(newGan, transform.position, Quaternion.identity);
        }
        //else
        //{
        //    // 生成医疗包
        //    GameObject healthPack = Resources.Load<GameObject>("HealthPack");
        //    Instantiate(healthPack, transform.position, Quaternion.identity);
        //}
    }

}
