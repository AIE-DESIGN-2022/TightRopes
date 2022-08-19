using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public bool flashlightOn;

    public GameObject torch;
    public List<Light> lights;
    private AnimationManager animManager;

    private InputReader inputReader;

    private Battery batteryScript;

    


    // Start is called before the first frame update
    void Start()
    {
        /*        light = GameObject.Find("Torch Light");
        */
        animManager = FindObjectOfType<AnimationManager>();
        inputReader = FindObjectOfType<InputReader>();
        inputReader.TorchEvent += Interaction;
        flashlightOn = true;
        batteryScript = FindObjectOfType<Battery>();
        //       light= torch.GetComponentInChildren<Light>();
        batteryScript.flashlightHasBattery = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!batteryScript.flashlightHasBattery)
        {
            flashlightOn = false;
        }
    }

    public void TurnOnOff()
    {
        if (batteryScript.flashlightHasBattery)
        {
            if (!flashlightOn)
            {
                //torch.SetActive(true);
                flashlightOn = true;
                /*animManager.FlashOut();*/
                batteryScript.usingFlashlight = true;
            }
            else
            {
                //torch.SetActive(false);
                flashlightOn = false;
                batteryScript.usingFlashlight = false;
            }
            foreach (Light light in lights)
            {
                light.enabled = flashlightOn;
            }
        }
        else { }
    }

    public void OutOfPower()
    {
        flashlightOn = false ;
        foreach (Light light in lights)
        {
            light.enabled = flashlightOn;
        }
    }
    public void Interaction()
    {
        TurnOnOff();
    }
}
