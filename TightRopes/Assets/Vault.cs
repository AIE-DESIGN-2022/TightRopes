using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vault : MonoBehaviour
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
        if (other.gameObject.tag == "Player")
        {
            GameObject player = other.gameObject;
            PlayerController pController = player.GetComponent<PlayerController>();
            player.GetComponent<Animator>().runtimeAnimatorController = pController.vaultController;

            if (!pController.isCrawling && !pController.isCrouched)
            {
                AnimationManager blake = FindObjectOfType<AnimationManager>();
                blake.Vault();
                player.GetComponent<Animator>().SetTrigger("Vault");

            }
        }
    }
}
