using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private bool flashlightOn;

    public GameObject torch;
    public Light light;
    private AnimationManager animManager;

    private InputReader inputReader;

    private void Awake()
    {
        if (light == null) light = GetComponentInChildren<Light>();
    }

    // Start is called before the first frame update
    void Start()
    {
        /*        light = GameObject.Find("Torch Light");
        */
        animManager = FindObjectOfType<AnimationManager>();
        inputReader = FindObjectOfType<InputReader>();
        inputReader.TorchEvent += Interaction;
        flashlightOn = true;
        light= torch.GetComponentInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TurnOnOff()
    {
        //print("on off called");
        if (!flashlightOn)
        {
            //torch.SetActive(true);
            flashlightOn = true;
            /*animManager.FlashOut();*/
        }
        else
        {
            //torch.SetActive(false);
            flashlightOn = false;
/*            animManager.FlashIn();
*/        }
        light.enabled = flashlightOn;
        Debug.Log("flashlight = " + light.enabled.ToString());
    }
    public void Interaction()
    {
        TurnOnOff();
    }
}
