using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    private float flashlightBatteryCharge;
    private float cameraBatteryCharge;

    public float flashlightRate;
    public float cameraRate;

    public float maxBatteryCharge;

    public bool usingFlashlight;
    public bool usingCamera;

    private Flashlight flashlightScript;
    public GameObject flashlight;

    public GameObject greenFlashLight;
    public GameObject redFlashLight;


    private NightVisionController nightVisionController;
    public GameObject cameraObject;

    // Start is called before the first frame update
    void Start()
    {
        flashlightBatteryCharge = maxBatteryCharge;
        cameraBatteryCharge = maxBatteryCharge;

        flashlightScript = FindObjectOfType<Flashlight>();
        nightVisionController = FindObjectOfType<NightVisionController>();

        FlashlightNewBattery();

        CameraNewBattery();
    }

    // Update is called once per frame
    void Update()
    {
        if (usingFlashlight)
        {
            float usageThisFrame = flashlightRate * Time.deltaTime;

            flashlightBatteryCharge -= usageThisFrame;

            Debug.Log("Flashlight Charge Remaining" + flashlightBatteryCharge);

            if(flashlightBatteryCharge <= 0)
            {
                FlashlightOutOfBattery();
            }
        }

        if (usingCamera)  
        {
            float usageThisFrame = cameraRate * Time.deltaTime;

            cameraBatteryCharge -= usageThisFrame;

            Debug.Log("Camera Charge Remaining" + cameraBatteryCharge);

            //battery slider

            if (cameraBatteryCharge <= 0)
            {
                CameraOutOfBattery();
            }
        }
    }

    public void FlashlightOutOfBattery()
    {
        greenFlashLight.SetActive(false);
        redFlashLight.SetActive(true);
    }

    public void FlashlightNewBattery()
    {
        greenFlashLight.SetActive(true);
        redFlashLight.SetActive(false);
    }

    public void CameraOutOfBattery()
    {
        //display screen disabled
        //disble lights on camera
        //disable input
        //out of battery identifier
    }

    public void CameraNewBattery()
    {

    }
}
