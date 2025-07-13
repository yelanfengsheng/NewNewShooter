using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanleControl : MonoBehaviour
{
    public bool uiStart= false;
    

    private void Awake()
    {
       

    }
    private void Update()
    {
        
    }
    public void RerootBeginButten()
    {
        SceneManager.LoadScene(0);
    }
    public void ReturnButten()
    {
        if (uiStart)
        {
            this.gameObject.SetActive(true);
        }
    }

}
