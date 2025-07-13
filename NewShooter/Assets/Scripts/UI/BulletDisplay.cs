using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletDisplay : MonoBehaviour
{
    public Text text;
    public static int currentAmmo;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentAmmo = Shooting.currentAmmo; // Get the current ammo count from the Shooting script
        // Update the text to display the current ammo count
        text.text = "Bullet: " + currentAmmo.ToString();
    }
}
