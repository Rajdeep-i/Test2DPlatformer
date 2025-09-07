using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    Vector2 moveInput;

    public float CurrentMoveSpeed
    {
        get
        {
            if (IsMoving)
            {
                if(IsRunning)
                {
                    return runSpeed;
                }
                else 
                { 
                    return walkSpeed; 
                }
            }
            else
            {
                // idle speed is 0
                return 0;
            }
        }
    }

    [SerializeField]
    private bool isMoving = false;

    Rigidbody2D rb;
    public bool IsMoving { get 
        {
            return isMoving;
        } 
        private set 
        {
            isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    [SerializeField]
    private bool isRunning = false;

    public bool IsRunning
    {
        get
        {
            return isRunning;
        }
        private set
        {
            isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

    public bool _isFacingRight = true;

    public bool IsFacingRight { get { return _isFacingRight; }
        private set
        {
            if (_isFacingRight != value)
            {
                //Flip the local scale to make the player face the opposite direction
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
        }

    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.linearVelocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        //moveInput = context.Get<Vector2>();
        IsMoving = moveInput != Vector2.zero;

        SetFacingDirection(moveInput);
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !IsFacingRight)
        {
            //Face the right
            IsFacingRight = true;
        }
        else if(moveInput.x < 0 && IsFacingRight)
        {
            //Face the left
            IsFacingRight= false;
        }
    }

    public void OnRun(InputAction.CallbackContext context) 
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled) 
        {
            IsRunning = false;
        }
    }

}
