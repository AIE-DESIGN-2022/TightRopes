using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private bool flashlightOn;

    private GameObject light;

    private AnimationManager animManager;


    // Start is called before the first frame update
    void Start()
    {
        light = GameObject.Find("Torch Light");

        animManager = FindObjectOfType<AnimationManager>();
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
            animManager.FlashOut();
        }
        else
        {
            light.SetActive(false);
            flashlightOn = false;
            animManager.FlashIn();
        }
    }
}
