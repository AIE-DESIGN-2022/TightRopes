using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Objects")]
    public GameObject Blake;
    public GameObject torch;
    public GameObject handCamera;
    private float standheight = 2f;
    public float crouchheight = 1.5f;
    public float proneheight = 1f;

    [Header("Bools")]
    private bool isWalking;
    public bool isCrouched;
    private bool isMoving;
    public bool isCrawling;
    public bool leftArm;
    public bool rightArm;

    [Header("speeds")]
    public float walkSpeed;
    public float sprintSpeed;
    public float crawlingSpeed;
    public float crouchingSpeed;
    public float gravity = -9.81f;
    public float jumpHeight = 0.001f;

    [Header("Grounding")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;

    [Header("Ledgegrabs")]
    private bool _grabbedLedge;
    private LedgeChecker _activeLedge;
    Vector3 blakePosBeforeLedge;
    Vector3 climbPos;
   public GameObject ledgeChecker;

   [Header("Vectors")]
    Vector3 velocity;

    [Header("Scripts")]
    private InputReader inputReader;
    public CharacterController controller;
    public AnimationManager aniManager;

    [Header("Animators")]
    //public List<Ani> ;
    public RuntimeAnimatorController jumpController;
    public RuntimeAnimatorController climbController;
    public RuntimeAnimatorController vaultController;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().runtimeAnimatorController = climbController;
        aniManager = GetComponentInChildren<AnimationManager>();
        inputReader = GetComponentInChildren<InputReader>();
        controller = GetComponent<CharacterController>();
      
        inputReader.TorchEvent += LeftArm;
        inputReader.CameraEvent += RightArm;
        leftArm = true;
        rightArm = true;

        inputReader.CrouchEvent += Crouch;
        inputReader.ProneEvent += Prone;
    }


    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        Vector3 motion = transform.right * inputReader.MovementValue.x + transform.forward * inputReader.MovementValue.y;
        
        if (inputReader.IsSprinting && !isCrouched && !isCrawling)
        {
            controller.Move(motion * sprintSpeed * Time.deltaTime);
        }
        else if (isCrouched)
        {
            controller.Move(motion * crouchingSpeed * Time.deltaTime);

        }
        else if (isCrawling) 
        {
            controller.Move(motion * crawlingSpeed * Time.deltaTime);

        }
        else
        {

        controller.Move(motion * walkSpeed * Time.deltaTime);
            
            
        }

        //sprint
        //jump
       
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity*Time.deltaTime);
    }
    private void LateUpdate()
    {
   
        if (_grabbedLedge)
        {
            Blake.transform.position = transform.position;
        }
    }
    private void LeftArm()
    {
        if (!leftArm && rightArm)
        {
            handCamera.SetActive(false);
            torch.SetActive(true);
            aniManager.FlashOut();
            leftArm = true;
            rightArm = false;
        }
    }

    private void RightArm()
    {
        if (leftArm && !rightArm)
        {
            leftArm=false;
            handCamera.SetActive(true);
            aniManager.FlashIn();
            StartCoroutine(aniManager.Wait());
            torch.SetActive(false);
            
        }
    }

    private void Prone()
    {
        //Debug.Log(isCrawling);
        if (!isCrawling)
        {
        aniManager.Prone();
        isCrawling = true;
        isCrouched = false;
        controller.height = proneheight;

        }
        else
        {
            aniManager.Prone();
            isCrawling = false;
            controller.height = crouchheight;
        }
       // Debug.Log(isCrawling);

    }

    private void Crouch()
    {
        if (!isCrouched)
        {
        aniManager.Crouch();
        isCrouched = true;
        isCrawling = false;
        controller.height = crouchheight;
    }
        else
        {
        aniManager.Crouch();
        isCrouched = false;
        controller.height = standheight;
        }
    }
    public void GrabLedge(Vector3 Handpos, LedgeChecker currentLedge)
    {
        // blakePosBeforeLedge = Blake.transform.position;
        GetComponent<Animator>().runtimeAnimatorController = climbController;
        GetComponent<Animator>().SetTrigger("Climb");
        climbPos = Handpos;
        controller.enabled = false;
        _grabbedLedge = true;
        aniManager.Climb();
        isGrounded = false;
        _activeLedge = currentLedge;
    }
    public void ClimbLedge()
    {
        //transform.position = climbPos;
        _grabbedLedge = false;
        //transform.position = _activeLedge.GetStandUpPos();
        controller.enabled = true;
        inputReader.jump = false;
        ledgeChecker.SetActive(true);

    }
}
