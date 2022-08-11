using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    private Text text;
    public Transform Top;
    // Start is called before the first frame update
    void Start()
    {
        text = FindObjectOfType<Text>();
        text.text = "";
        Top = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "";
    }

    private void OnTriggerStay(Collider other)
    {
        if (this.gameObject.tag == "Vaultable" && other.gameObject.tag == "Player")
        {
            text.text = "Press 'space' to Jump";
            if (Input.GetButton("Jump"))
            {
                text.text = "Yay";
                other.gameObject.transform.position += Top.position;
            }
        }
        
    }
}
