using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private bool flashlightOn;

    public GameObject torch;
    public List<Light> lights;
    private AnimationManager animManager;

    private InputReader inputReader;



    // Start is called before the first frame update
    void Start()
    {
        /*        light = GameObject.Find("Torch Light");
        */
        animManager = FindObjectOfType<AnimationManager>();
        inputReader = FindObjectOfType<InputReader>();
        inputReader.TorchEvent += Interaction;
        flashlightOn = true;
 //       light= torch.GetComponentInChildren<Light>();
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
