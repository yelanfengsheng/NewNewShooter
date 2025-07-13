using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHeart : MonoBehaviour
{
    private int healthValue = 10; // 每个心形物品增加的生命值
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Player collided with heart item");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collided with heart item");
            // 获取玩家的Health组件
            MyPlayerHealth playerHealth = collision.gameObject.GetComponent<MyPlayerHealth>();
            if (playerHealth.startHealth>0)
            {
                // 增加玩家的生命值
                playerHealth.AddHealth(healthValue); // 假设每个心形物品增加10点生命值
                Destroy(gameObject,0f); // 销毁心形物品
                
            }
        }
    }
   
}
