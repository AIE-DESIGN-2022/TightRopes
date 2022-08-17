using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Video;
using UnityEngine.UI;

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
        }
        else
        {
            video.enabled = false;    
            RenderSettings.ambientLight = defaultLightColour;
            volume.weight = 0;
        }
    }

    private void OnInteration()
    {
        canvas.SetActive(true);
        Maincamera.GetComponent<VideoPlayer>().enabled = true;
        Maincamera.GetComponent<PostProcessLayer>().enabled = true;
        ToggleNightVision();
    }
}

