using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Video;

public class NightVisionController : MonoBehaviour
{
    public Color defaultLightColour;
    public Color boostedLightColour;
    public bool NightVisionOn;
    public PostProcessVolume volume;
    InputReader input;
    private VideoPlayer Camera;
    // Start is called before the first frame update
    private void Start()
    {
        Camera =  GameObject.FindGameObjectWithTag("MainCamera").GetComponent<VideoPlayer>();
        input = FindObjectOfType<InputReader>();

        Camera.enabled= false;
        RenderSettings.ambientLight = defaultLightColour;
        input.InteractEvent += OnInteration;
        volume = gameObject.GetComponent<PostProcessVolume>();
        volume.weight = 0;
    }

    private void Update()
    {
      
    }
    private void ToggleNightVision()
    {
        NightVisionOn = !NightVisionOn;

        if (NightVisionOn)
        {
            Camera.enabled = true;
            RenderSettings.ambientLight = boostedLightColour;
            volume.weight = 1;
        }
        else
        {
            Camera.enabled = false;    
            RenderSettings.ambientLight = defaultLightColour;
            volume.weight = 0;
        }
    }

    private void OnInteration()
    {
        ToggleNightVision();
    }
}

