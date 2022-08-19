using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    private float flashlightBatteryCharge;
    private float cameraBatteryCharge;

    public float flashlightRate;
    public float cameraRate;

    public float Batteries;

    public float maxBatteryCharge;

    public bool usingFlashlight;
    public bool usingCamera;

    private Flashlight flashlightScript;
    public GameObject flashlight;

    public GameObject greenFlashLight;
    public GameObject redFlashLight;

    public GameObject whiteCamera;
    public GameObject redCamera;

    private InputReader inputReader;
    private NightVisionController nightVisionController;
    public GameObject cameraObject;

    public GameObject cameraLight;
    public GameObject cameraScreen;

    public bool flashlightHasBattery;
    public bool cameraHasBattery;

    // Start is called before the first frame update
    void Start()
    {
        flashlightBatteryCharge = maxBatteryCharge;
        cameraBatteryCharge = maxBatteryCharge;
        inputReader = FindObjectOfType<InputReader>();
        inputReader.replaceFlashlightBattery += FlashlightNewBattery;
        inputReader.replaceCameraBattery += CameraNewBattery;
        flashlightScript = FindObjectOfType<Flashlight>();
        nightVisionController = FindObjectOfType<NightVisionController>();

        redFlashLight.SetActive(false);
        redCamera.SetActive(false);

        cameraHasBattery = true;
        flashlightHasBattery = true;
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
        flashlightHasBattery = false;
        flashlightScript.OutOfPower();
        usingFlashlight = false;
    }

    public void FlashlightNewBattery()
    {
        greenFlashLight.SetActive(true);
        redFlashLight.SetActive(false);
        flashlightHasBattery = true;
        Batteries--;
        flashlightBatteryCharge = maxBatteryCharge;
    }

    public void CameraOutOfBattery()
    {
        cameraLight.SetActive(false);
        cameraHasBattery = false;
        cameraScreen.SetActive(false);
        redCamera.SetActive(true);
        whiteCamera.SetActive(false);
        nightVisionController.OutOfPower();
        //out of battery identifier
    }

    public void CameraNewBattery()
    {
        cameraLight.SetActive(true);
        cameraHasBattery = true;
        cameraScreen.SetActive(false);
        redCamera.SetActive(false);
        whiteCamera.SetActive(true);
        cameraBatteryCharge = maxBatteryCharge;
        Batteries--;
    }
}
