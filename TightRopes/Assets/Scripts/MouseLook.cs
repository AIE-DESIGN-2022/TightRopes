using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    InputReader inputReader;

    public float mSense = 100f;
    public GameObject player;

    float xRotation;
    // Start is called before the first frame update
    void Start()
    {
        inputReader = player.GetComponent<InputReader>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = inputReader.LookValue.x * mSense* Time.deltaTime;
        float mouseY = inputReader.LookValue.y * mSense* Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        player.transform.Rotate(Vector3.up * mouseX);
    }
}
