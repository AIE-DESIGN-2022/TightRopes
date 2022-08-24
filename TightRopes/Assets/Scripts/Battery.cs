using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    private float flashlightBatteryCharge;
    private float cameraBatteryCharge;

    public float flashlightRate;
    public float cameraRate;

    public float batteries;

    public float maxBatteryCharge;

    public bool usingFlashlight;
    public bool usingCamera;

    private Flashlight flashlightScript;
    public GameObject flashlight;

    public GameObject batteryFlashLevel1;
    public GameObject batteryFlashLevel2;
    public GameObject batteryFlashLevel3;

    public GameObject cameraBatteryLevel1;
    public GameObject cameraBatteryLevel2;
    public GameObject cameraBatteryLevel3;

    public Material greenEmissionMat;
    public Material outOfBatteryMat;

    public GameObject whiteCamera;
    public GameObject redCamera;

    private InputReader inputReader;
    private NightVisionController nightVisionController;
    public GameObject cameraObject;

    public GameObject cameraLight;
    public GameObject cameraScreen;

    public bool flashlightHasBattery;
    public bool cameraHasBattery;

    public GameObject battery1;
    public GameObject battery2;
    public GameObject battery3;

    

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

            //Debug.Log("Flashlight Charge Remaining" + flashlightBatteryCharge);

            if(flashlightBatteryCharge <= 0)
            {
                FlashlightOutOfBattery();
            }
        }

        if (usingCamera)  
        {
            float usageThisFrame = cameraRate * Time.deltaTime;

            cameraBatteryCharge -= usageThisFrame;

            //Debug.Log("Camera Charge Remaining" + cameraBatteryCharge);

            //battery slider

            if (cameraBatteryCharge <= 0)
            {
                CameraOutOfBattery();
            }
        }

        if(flashlightBatteryCharge > 666)
        {
            batteryFlashLevel1.GetComponent<Renderer>().material = greenEmissionMat;
            batteryFlashLevel2.GetComponent<Renderer>().material = greenEmissionMat;
            batteryFlashLevel3.GetComponent<Renderer>().material = greenEmissionMat;
        }
        else if(flashlightBatteryCharge > 333 && flashlightBatteryCharge < 666)
        {
            batteryFlashLevel1.GetComponent<Renderer>().material = outOfBatteryMat;
            batteryFlashLevel2.GetComponent<Renderer>().material = greenEmissionMat;
            batteryFlashLevel3.GetComponent<Renderer>().material = greenEmissionMat;
        }
        else if (flashlightBatteryCharge > 0 && flashlightBatteryCharge < 333)
        {
            batteryFlashLevel1.GetComponent<Renderer>().material = outOfBatteryMat;
            batteryFlashLevel2.GetComponent<Renderer>().material = outOfBatteryMat;
            batteryFlashLevel3.GetComponent<Renderer>().material = greenEmissionMat;
        }
        else if (flashlightBatteryCharge <= 0)
        {
            batteryFlashLevel1.GetComponent<Renderer>().material = outOfBatteryMat;
            batteryFlashLevel2.GetComponent<Renderer>().material = outOfBatteryMat;
            batteryFlashLevel3.GetComponent<Renderer>().material = outOfBatteryMat;
        }

        if (cameraBatteryCharge > 666)
        {
            cameraBatteryLevel1.SetActive(true);
            cameraBatteryLevel2.SetActive(true);
            cameraBatteryLevel3.SetActive(true);
        }
        else if (cameraBatteryCharge > 333 && cameraBatteryCharge < 666)
        {
            cameraBatteryLevel1.SetActive(false);
            cameraBatteryLevel2.SetActive(true);
            cameraBatteryLevel3.SetActive(true);
        }
        else if (cameraBatteryCharge > 0 && cameraBatteryCharge < 333)
        {
            cameraBatteryLevel1.SetActive(false);
            cameraBatteryLevel2.SetActive(false);
            cameraBatteryLevel3.SetActive(true);
        }
        else if (cameraBatteryCharge <= 0)
        {
            cameraBatteryLevel1.SetActive(false);
            cameraBatteryLevel2.SetActive(false);
            cameraBatteryLevel3.SetActive(false);
        }
    }

    public void FlashlightOutOfBattery()
    {
        flashlightHasBattery = false;
        flashlightScript.OutOfPower();
        usingFlashlight = false;
    }

    public void FlashlightNewBattery()
    {
        flashlightHasBattery = true;
        batteries--;
        flashlightBatteryCharge = maxBatteryCharge;
        ChangeBatteryAmount();
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
        batteries--;
        ChangeBatteryAmount();
    }

    public void ChangeBatteryAmount()
    {
        if(batteries == 3)
        {
            battery1.SetActive(true);
            battery2.SetActive(true);
            battery3.SetActive(true);
        }
        if (batteries == 2)
        {
            battery1.SetActive(false);
            battery2.SetActive(true);
            battery3.SetActive(true);
        }
        if (batteries == 1)
        {
            battery1.SetActive(false);
            battery2.SetActive(false);
            battery3.SetActive(true);
        }
        if (batteries == 0)
        {
            battery1.SetActive(false);
            battery2.SetActive(false);
            battery3.SetActive(false);
        }
    }
}
