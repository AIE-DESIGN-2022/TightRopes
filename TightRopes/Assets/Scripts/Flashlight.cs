using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private bool flashlightOn;

    private GameObject light;

    // Start is called before the first frame update
    void Start()
    {
        light = GameObject.Find("Torch Light");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOnOff()
    {
        if (flashlightOn)
        {
            light.SetActive(true);
            flashlightOn = true;
        }
        else
        {
            light.SetActive(false);
            flashlightOn = false;
        }
    }
}
