using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events; //import gia na ftiaksoume ta events
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    private bool doublejump = false;//used to check if player is on air(has already jumped once)

    [SerializeField] public float speed = 6f;
    [SerializeField] public float jumpForce = 6f;

    [SerializeField] private Transform onGround;            // gia na elegxoume an einai sto edafos
    [SerializeField] private Transform onAir;               //gia na elegxoume ean einai ston aera
    [SerializeField] private LayerMask jumpableGround;      //pou mporei na pidiksei

    private bool isGrounded;
    private float groundedRadius = .2f;


    private float directionX = 0.0f;
    private bool flipSide = true;

    private Animator animator;
    
    private SpriteRenderer sprite;
    private enum MovementState { idle, running, jumping, falling }

    private Rigidbody2D rigidbody;
    private PolygonCollider2D polygonCollider;

    private BoxCollider2D bc;


    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;
    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        sprite = GetComponent<SpriteRenderer>();


        if (OnLandEvent == null)
        {
            OnLandEvent = new UnityEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        directionX = Input.GetAxisRaw("Horizontal");
        rigidbody.velocity = new Vector2(directionX * speed, rigidbody.velocity.y);


        animator.SetFloat("speed", Mathf.Abs(directionX));

        //each time player can jump maximum two times
        if (Input.GetButtonDown("Jump")) //if user presses space button
        {
            if (CanJump()) //if player stands on ground
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
                doublejump = true;

                SoundManager.playSound("jump");
                animator.SetBool("canJump", true); //gia na deiksei to animation tou jump
                                                   //i canJump <> CanJump. to ena einai i metabliti gia ta animation
               
            }
            else if (doublejump)//if player has already jumped once
            {
                SoundManager.playSound("jump");
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
                doublejump = false;
            }
            
        }

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
        UpdateAnimationState();
    }
    private void flip()
    {
        flipSide = !flipSide;
        transform.Rotate(0f, 180f, 0f);
    }
    private void UpdateAnimationState()
    {

        if (directionX > 0f && !flipSide)
        {
            flip();
        }
        else if (directionX < 0f &&flipSide)
        {
            flip();
        }
    }

    //checks if player stands on the ground
    private bool CanJump()
    {
        return Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    public void Landing()
    {
        animator.SetBool("canJump", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("endOfLevel"))
        {
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                SoundManager.playSound("clear");
                System.Threading.Thread.Sleep(1500);
                SceneManager.LoadScene("startMenu");
            }
            SoundManager.playSound("clear");
            System.Threading.Thread.Sleep(1500); //delay 1500 ms
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
