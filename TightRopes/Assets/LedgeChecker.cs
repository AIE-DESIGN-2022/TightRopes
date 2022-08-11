using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeChecker : MonoBehaviour
{
    public Transform handPos, standPos;
    public float yOffset = -1f;
    public float zOffset = 2f;

    private Vector3 newHandPos;

    // Start is called before the first frame update
    void Start()
    {
        newHandPos = new Vector3(handPos.position.x, 
            handPos.position.y - yOffset, handPos.position.z + zOffset);
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        if (other.CompareTag("LedgeChecker"))
        {
            var player = other.GetComponentInParent<PlayerController>();
            player.GrabLedge(newHandPos, this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var Player = other.GetComponentInParent<PlayerController>();
        Player.Controller.enabled = true;
    }
    public Vector3 GetStandUpPos()
    {
        return standPos.position;
    }
}
