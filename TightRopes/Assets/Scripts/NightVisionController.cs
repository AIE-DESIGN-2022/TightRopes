using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Video;
using UnityEngine.UI;
using Kino;

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

    }

    private void Update()
    {
        if (playerController.rightArm)
        {
            input.CameraEvent += OnInteration;
        }
    }
    private void ToggleNightVision()
    {
        NightVisionOn = !NightVisionOn;

        if (NightVisionOn)
        {
            video.enabled = true;
            RenderSettings.ambientLight = boostedLightColour;
            volume.weight = 1;
            analogGlitch.enabled = true;
            digitalGlitch.enabled = true;
            canvas.SetActive(true);
            //disable blake model
            //disbale flashlight
        }
        else
        {
            video.enabled = false;    
            RenderSettings.ambientLight = defaultLightColour;
            volume.weight = 0;
            analogGlitch.enabled = false;
            digitalGlitch.enabled = false;
            canvas.SetActive(false);
            //enable blake model
            //enable flashlight
        }
    }

    private void OnInteration()
    {
        Maincamera.GetComponent<VideoPlayer>().enabled = true;
        Maincamera.GetComponent<PostProcessLayer>().enabled = true;
        ToggleNightVision();
    }
}

