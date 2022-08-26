using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Endzone : MonoBehaviour
{ 
    public int currentScene;
    public int nextScene;
    AsyncOperation asyncLoad;
    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene == 0)
        {
            nextScene = 1;
        }
        else if (currentScene == 1)
        {
            nextScene= 2;
        }
        else
        {
            currentScene = 0;
        }
        asyncLoad = SceneManager.LoadSceneAsync(nextScene);
        asyncLoad.allowSceneActivation = false;

    }

    // Update is called once per frame
    void Update()
    {        
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<InputReader>().jump)
            {
                asyncLoad.allowSceneActivation = true;
            }

            
        }
    }
}
