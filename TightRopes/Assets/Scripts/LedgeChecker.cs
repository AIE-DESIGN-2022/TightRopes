using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeChecker : MonoBehaviour
{
    public Transform handPos, standPos;
    public float yOffset = -1f;
    
    InputReader inputReader;
    private Vector3 newHandPos;

    // Start is called before the first frame update
    void Start()
    {
        inputReader = FindObjectOfType<InputReader>();
        newHandPos = new Vector3(handPos.position.x, 
            handPos.position.y - yOffset, handPos.position.z);
    }

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Hit");
        if (other.CompareTag("LedgeChecker"))
        {
            if (inputReader.jump)
            {
            var player = other.GetComponentInParent<PlayerController>();
            player.GrabLedge(newHandPos,this);
            other.gameObject.SetActive(false);  
            }        
        }
    }
    public Vector3 GetStandUpPos()
    {
        return standPos.position;
    }
}
