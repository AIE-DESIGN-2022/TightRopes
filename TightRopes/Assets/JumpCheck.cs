using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GameObject player = other.gameObject;
            PlayerController pController = player.GetComponent<PlayerController>();
            player.GetComponent<Animator>().runtimeAnimatorController = pController.jumpController;

            if(!pController.isCrawling && !pController.isCrouched)
            {
                AnimationManager blake = FindObjectOfType<AnimationManager>();
                blake.Jump();
               player.GetComponent<Animator>().SetTrigger("Jump");

            }
        }
    }
}
