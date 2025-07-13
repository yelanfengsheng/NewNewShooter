using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateReward : MonoBehaviour
{ 
    public int rotateSpeed = 120; // 旋转速度
    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime); // 每秒旋转50度
    }
}
