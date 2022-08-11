using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Objects")]
    public Camera camera;
    public GameObject Arms;

    [Header("Bools")]
    public bool isWalking;
    public bool isCrouched;
    public bool isMoving;
    public bool isCrawling;
    public bool isGrounded;

    [Header("floats")]
    public float walkSpeed;
    public float sprintSpeed;

    [Header("Camera contols")]
    public float Xsensitivity;
    public float Ysensitivity;
    public float xMin = -360;
    public float xMax = 360;
    public float yMin = -75;
    public float yMax = 75;
    float xRot;
    float yRot;



    [Header("Camera positions")]
    public List<Transform> playerPOS;
    public Transform crouchPos;
    public Transform standPos;
    public Transform crawlPos;

    [Header("Scripts")]
    private InputReader inputReader;
    private CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        inputReader = GetComponent<InputReader>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Look();
        //Debug.Log(inputReader.LookValue);
    }

    private void Look() {
        Vector3 cameraAngles = camera.transform.eulerAngles;
        xRot = inputReader.LookValue.y * -1;
        yRot = inputReader.LookValue.x;
        cameraAngles.x += xRot * Xsensitivity * Time.deltaTime;
        cameraAngles.y += yRot * Ysensitivity * Time.deltaTime;

        camera.transform.eulerAngles = cameraAngles;

    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F) angle += 360F;
        if (angle > 360F) angle -= 360F;
        return Mathf.Clamp(angle, min, max);

    }
    void Movement()
    {
        Vector3 motion= new Vector3(inputReader.MovementValue.x,0,inputReader.MovementValue.y);
        if (inputReader.IsSprinting)
        {
            characterController.Move(motion * sprintSpeed * Time.deltaTime);
        }
        else
        {
            characterController.Move(motion * walkSpeed * Time.deltaTime);
        }
    }
}
