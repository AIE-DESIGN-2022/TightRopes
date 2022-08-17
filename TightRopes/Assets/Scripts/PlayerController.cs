using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Objects")]
    
    public GameObject Arms;

    public GameObject blake;

    [Header("Bools")]
    public bool isWalking;
    public bool isCrouched;
    public bool isMoving;
    public bool isCrawling;
    public bool jumpAnimFinish;

    [Header("speeds")]
    public float walkSpeed;
    public float sprintSpeed;
    public float gravity = -9.81f;
    public float jumpHeight = 0.001f;

    [Header("Grounding")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;
    public List<Transform> playerPOS;

    [Header("Ledgegrabs")]
    private bool _grabbedLedge;
    private LedgeChecker _activeLedge;

    [Header("Vectors")]
    Vector3 velocity;

    [Header("Scripts")]
    private InputReader inputReader;
    public CharacterController Controller;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        inputReader = GetComponentInChildren<InputReader>();
        Controller = GetComponent<CharacterController>();
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
        
        if (inputReader.IsSprinting)
        {
            Controller.Move(motion * sprintSpeed * Time.deltaTime);
        }
        else
        {

        Controller.Move(motion * walkSpeed * Time.deltaTime);
            
            
        }

        //sprint
        //jump
        if (inputReader.jump && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        Controller.Move(velocity*Time.deltaTime);
        
    }

    public void GrabLedge(Vector3 Handpos, LedgeChecker currentLedge)
    {
        Controller.enabled = false;
        _grabbedLedge = true;
        transform.position = Handpos;
        isGrounded = false;
        _activeLedge = currentLedge;
    }

    public void Jump()
    {
        this.gameObject.transform.position = blake.transform.position;
    }

}
