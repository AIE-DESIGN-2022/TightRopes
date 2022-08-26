using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneLock : MonoBehaviour
{
    AsyncOperation loadScene;
    // Start is called before the first frame update
    void Start()
    {
        loadScene = SceneManager.LoadSceneAsync(2);
        loadScene.allowSceneActivation = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponentInChildren<MouseLook>().inCutscene = true;
            
        }
    }
    public void LoadScene()
    {
        loadScene.allowSceneActivation = true;
    }
}
