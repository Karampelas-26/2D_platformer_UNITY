using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; //import gia na ftiaksoume ta events
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    private bool doublejump = false;//used to check if player is on air(has already jumped once)

    [SerializeField] public float speed = 6f;
    [SerializeField] public float jumpForce = 6f;

    //antontis
    [SerializeField] private Transform onGround;            // gia na elegxoume an einai sto edafos
    [SerializeField] private Transform onAir;               //gia na elegxoume ean einai ston aera
    [SerializeField] private LayerMask jumpableGround;      //pou mporei na pidiksei

    private bool isGrounded;
    private float groundedRadius = .2f;
    //a


    private float directionX = 0.0f;
    private bool flipSide = true;

    private Animator animator;
    
    private SpriteRenderer sprite;
    private enum MovementState { idle, running, jumping, falling }

    private Rigidbody2D rigidbody;
    private PolygonCollider2D polygonCollider;

    private BoxCollider2D bc;


    //antonis
    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;
    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }
    //a

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        sprite = GetComponent<SpriteRenderer>();


        //antonis
        if (OnLandEvent == null)
        {
            OnLandEvent = new UnityEvent();/*
            OnLandEvent.AddListener(Landing);*/
        }
        //a
    }

    // Update is called once per frame
    void Update()
    {
        directionX = Input.GetAxisRaw("Horizontal");
        rigidbody.velocity = new Vector2(directionX * speed, rigidbody.velocity.y);


        //antonis
        animator.SetFloat("speed", Mathf.Abs(directionX));
        //a

        //each time player can jump maximum two times
        if (Input.GetButtonDown("Jump")) //if user presses space button
        {
            if (CanJump()) //if player stands on ground
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
                doublejump = true;

                //antonis
                animator.SetBool("canJump", true); //gia na deiksei to animation tou jump
                                                   //i canJump <> CanJump. to ena einai i metabliti gia ta animation
                //a
            }
            else if (doublejump)//if player has already jumped once
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
                doublejump = false;
            }
            
        }

        //antonis

        //kwdikas gia allagi animation otan epanerxetai sto edafos
        bool wasGrounded = isGrounded;
        isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(onGround.position, groundedRadius, jumpableGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke(); //kanei trigger to event pou kalei tin sunartisi pou orisame sto serializable OnLandEvent
            }
        }

        //a

        /*animator.SetFloat("speed", Mathf.Abs(directionX));*/
        UpdateAnimationState();
    }
    private void flip()
    {
        flipSide = !flipSide;
        transform.Rotate(0f, 180f, 0f);
    }
    private void UpdateAnimationState()
    {
        MovementState state;

        if (directionX > 0f && !flipSide)
        {
            state = MovementState.running;
            flip();
        }
        else if (directionX < 0f &&flipSide)
        {
            state = MovementState.running;
            flip();
        }
        else
        {
            state = MovementState.idle;
        }

        if (rigidbody.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rigidbody.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        animator.SetInteger("state", (int)state);
    }

    //checks if player stands on the ground
    private bool CanJump()
    {
        return Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    //antonis
    public void Landing()
    {
        animator.SetBool("canJump", false);
    }
    //a
}
