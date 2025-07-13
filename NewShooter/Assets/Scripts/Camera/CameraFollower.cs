using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public GameObject player;
    private Vector3 offSet;
    public float Soomth = 3f;
    private void Start()
    {
         offSet = transform.position - player.transform.position;
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, offSet+player.transform.position,Soomth*Time.deltaTime);

    }
}
