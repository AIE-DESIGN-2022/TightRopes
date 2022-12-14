using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeChecker : MonoBehaviour
{
    public Transform handPos, standPos;
    public float yOffset = -1f;
    

    private Vector3 newHandPos;

    // Start is called before the first frame update
    void Start()
    {
        newHandPos = new Vector3(handPos.position.x, 
            handPos.position.y - yOffset, handPos.position.z);
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        if (other.CompareTag("LedgeChecker"))
        {

            var player = other.GetComponentInParent<PlayerController>();
            player.GrabLedge(newHandPos,this);
            other.gameObject.SetActive(false);  

            /*var controller = other.GetComponentInParent<PlayerController>();

            GameObject player = other.transform.parent.gameObject;
            Animator Blake = player.transform.GetComponentInChildren<Animator>();
            Blake.SetTrigger("climb");
            controller.GrabLedge(newHandPos, this);*/
        }
    }
/*    private void OnTriggerExit(Collider other)
    {
        var controller = other.GetComponentInParent<PlayerController>();
        controller.controller.enabled = true;
    }*/
    public Vector3 GetStandUpPos()
    {
        return standPos.position;
    }
}
