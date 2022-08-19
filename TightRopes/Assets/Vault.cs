using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vault : MonoBehaviour
{
    InputReader inputReader;

    // Start is called before the first frame update
    void Start()
    {
        inputReader = FindObjectOfType<InputReader>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject player = other.gameObject;
            PlayerController pController = player.GetComponent<PlayerController>();
            player.GetComponent<Animator>().runtimeAnimatorController = pController.vaultController;

            if (inputReader.jump && !pController.isCrawling && !pController.isCrouched)
            {
                AnimationManager blake = FindObjectOfType<AnimationManager>();
                blake.Vault();
                player.GetComponent<Animator>().SetTrigger("Vault");
                StartCoroutine(Wait());
                inputReader.jump = false;

            }
        }
    }
    IEnumerator Wait()
    {
        var clip = inputReader.gameObject.GetComponent<Animator>().runtimeAnimatorController.animationClips;
        float time = clip.Length;
        yield return new WaitForSeconds(time);
    }
}
