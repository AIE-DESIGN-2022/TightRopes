using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Video;
using UnityEngine.UI;
using Kino;
using Fungus;

public class NightVisionController : MonoBehaviour
{
    public Color defaultLightColour;
    public Color boostedLightColour;
    public bool NightVisionOn;
    public PostProcessVolume volume;
    InputReader input;
    private VideoPlayer video;
    public GameObject Maincamera;
    public GameObject canvas;
    public PlayerController playerController;
    public AnalogGlitch analogGlitch;
    public DigitalGlitch digitalGlitch;

    public GameObject blakeMesh;
    public GameObject flashlight;
    public GameObject handCamera;
    public Flowchart cameraFlowchart;

    private Battery batteryScript;

    // Start is called before the first frame update
    private void Start()
    {
        canvas.SetActive(false);
        playerController = FindObjectOfType<PlayerController>();
        Maincamera = GameObject.FindGameObjectWithTag("MainCamera");
        Maincamera.GetComponent<VideoPlayer>().enabled = false;
        Maincamera.GetComponent<PostProcessLayer>().enabled = false;
        
        video =  Maincamera.GetComponent<VideoPlayer>();
        input = FindObjectOfType<InputReader>();

        RenderSettings.ambientLight = defaultLightColour;
        volume = gameObject.GetComponent<PostProcessVolume>();
        volume.weight = 0;

        digitalGlitch = Maincamera.GetComponent<DigitalGlitch>();
        analogGlitch = Maincamera.GetComponent<AnalogGlitch>();

        analogGlitch.enabled = false;
        digitalGlitch.enabled = false;

        batteryScript = FindObjectOfType<Battery>();
    }

    private void Update()
    {
        if (playerController.rightArm)
        {
            input.CameraEvent += OnInteration;
        }
    }
    public void ToggleNightVision()
    {
        if (batteryScript.cameraHasBattery)
        {
            NightVisionOn = !NightVisionOn;

            if (NightVisionOn)
            {
                Maincamera.GetComponent<VideoPlayer>().enabled = true;
                Maincamera.GetComponent<PostProcessLayer>().enabled = true;
                video.enabled = true;
                RenderSettings.ambientLight = boostedLightColour;
                volume.weight = 1;
                analogGlitch.enabled = true;
                digitalGlitch.enabled = true;
                canvas.SetActive(true);
                batteryScript.usingCamera = true;
                blakeMesh.SetActive(false);
                handCamera.SetActive(false);
                flashlight.SetActive(false);

            }
            else
            {
                Maincamera.GetComponent<VideoPlayer>().enabled = false;
                Maincamera.GetComponent<PostProcessLayer>().enabled = false;
                video.enabled = false;
                RenderSettings.ambientLight = defaultLightColour;
                volume.weight = 0;
                analogGlitch.enabled = false;
                digitalGlitch.enabled = false;
                canvas.SetActive(false);
                batteryScript.usingCamera = false;
                blakeMesh.SetActive(true);
                handCamera.SetActive(true);
                flashlight.SetActive(true);
            }
        }
    }

    public void OutOfPower()
    {
        video.enabled = false;
        RenderSettings.ambientLight = defaultLightColour;
        volume.weight = 0;
        analogGlitch.enabled = false;
        digitalGlitch.enabled = false;
        canvas.SetActive(false);
        batteryScript.usingCamera = false;
        blakeMesh.SetActive(true);
        handCamera.SetActive(true);
        flashlight.SetActive(true);
    }

    private void OnInteration()
    {
        cameraFlowchart.SendFungusMessage("Camera");        
    }

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(2);
    }
}

