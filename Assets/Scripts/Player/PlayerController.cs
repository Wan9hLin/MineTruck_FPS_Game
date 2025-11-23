using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController playerController;
    
    private Vector3 moveDirection;
    
    public float speed;

    public KeyCode silentKey;

    private bool isSlientWalk;

    public float currentSpeed;

    public float jumpForce;

    private Vector3 velocity;

    private bool isJump;

    public bool isWalking;
    
    public Transform checkGround;

    private float groundDistance=0.5f;

    public LayerMask groundMash;

    public bool isGround=true;
        
    public float gravity = -9f;

    private AudioSource[] audioList = new AudioSource[3];

    public static PlayerController instance;

    private Animator characterAnimator;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
        audioList = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {   
        characterAnimator = GameObject.FindWithTag("Gun").GetComponent<Animator>();
        CheckGround();
        PlayerMove();
        
    }

    public void PlayerMove()
    {   
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        moveDirection = (transform.right * h + transform.forward * v).normalized;
        if (v != 0 || h !=0)
        {
            isWalking = true;
            characterAnimator.SetBool("isWalk",true);
        }
        else
        {
            isWalking = false;
            characterAnimator.SetBool("isWalk",false);
        }
        
        if (Input.GetKey(silentKey))
        {
            isSlientWalk = true;
            // Debug.Log("silent walk");
            // OxygenController.instance.isLowOxygen === false ? currentSpeed = speed / 2.5f : currentSpeed = speed / 3.5f;
            // if (OxygenController.instance.isLowOxygen)
            // {
            //     currentSpeed = speed / 3.5f;
            // }
            // else
            // {
            //     currentSpeed = speed;
            // }
            currentSpeed = speed/2.5f;
            // currentSpeed = speed/2.5f;
            playerController.Move(moveDirection * (currentSpeed * Time.deltaTime));
            // characterAnimator.SetFloat("Horizontal", h, 0.15f, Time.deltaTime);
            
        }
        else
        {   
            // Debug.Log("normal walk");
            isSlientWalk = false;
            // if (OxygenController.instance.isLowOxygen)
            // {
            //     currentSpeed = speed / 2.5f;
            // }
            // else
            // {
            currentSpeed = speed;
            // }
            //
            playerController.Move(moveDirection * (currentSpeed * Time.deltaTime));
            // characterAnimator.SetFloat("Horizontal", h, 0.15f, Time.deltaTime);
        }
        //jump
        if (!isGround)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        playerController.Move(velocity * Time.deltaTime);
        PlayerJump();
        walkSoundPlay();
    }

    public void PlayerJump()
    {
        isJump = Input.GetButtonDown("Jump");
        if (isJump && isGround)
        {   
            audioList[1].Play();
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
        
    }

    private void CheckGround()
    {
        isGround = Physics.CheckSphere(checkGround.position,groundDistance,groundMash);
    }

    public void walkSoundPlay()
    {
        if (isWalking && isGround && !isSlientWalk)
        {
            if (!audioList[0].isPlaying)
            {   
                audioList[0].Play();
            }
            
        }
        else
        {
            if (audioList[0].isPlaying)
            {
                audioList[0].Pause();
            }
        }
    }
}
