using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed;
    //public int startPos;
    //Transform[] points;
    private int i; //antistoixa me tin platforma
    public bool mustMove = true;
    public Transform groundCheck;
    private bool mustFlip;

    private Animator animator;
    private BoxCollider2D collider;
    private Rigidbody2D rigidbody;

    [SerializeField] public LayerMask Layer;

    public Transform player;
    public float range;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        animator.GetComponent<Animator>();
        rigidbody.GetComponent<Rigidbody2D>();
        collider.GetComponent<BoxCollider2D>();
        //transform.position = points[startPos].position; //arxikopoiisi thesis enemy

    }

    // Update is called once per frame
    void Update()
    {
        if (mustMove) {
            Move();
        }
        distance = Vector2.Distance(transform.position, player.position); //upologizei kathe fora tin apostasi metaksi enemy kai player
        if (distance <= range)
        {
            if(player.position.x>transform.position.x && transform.localScale.x < 0 //elegxei ean koitaei ton paikti, an bgei true diladi
               ||player.position.x<transform.position.y && transform.localScale.x>0 )  //den ton koitaei tote kanoume flip prin riksei.
            {
                Flip();
            }
            mustMove = false;
            RangeAttack();
        }

    }

    private void FixedUpdate()
    {
        if (mustMove) {
            mustFlip = !Physics2D.OverlapCircle(groundCheck.position, 0.2f, Layer);
        }
    }

    public void Move()
    {
        if (mustFlip || collider.IsTouchingLayers()) { 
            Flip();
        }
        rigidbody.velocity = new Vector2(speed * Time.fixedDeltaTime, rigidbody.velocity.y); //gia na kounietai o antipalos
    }

    public void Flip() {
        mustMove = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y); //gia na gurnaei apo tin alli
        speed *= -1;
        mustMove = true; //to mustMove allazei times wste na min ginetai to flip oso kounietai
    }

    public void MeleeAttack() { 

    }

    public void RangeAttack()
    {

    }
}
