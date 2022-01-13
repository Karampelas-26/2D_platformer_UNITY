using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int lives;
    Rigidbody2D rb;
    [SerializeField] private AudioSource deathSoundEffect;
    BoxCollider2D turnCollider;
    PolygonCollider2D damageCollider;
    Animator animator;
    Rigidbody2D rigidbody;
    public Transform[] movingPoints; //einai pinakas giati einai panw apo ena simeio pou paei
    private int i;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damageCollider = GetComponent<PolygonCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        turnCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lookingRight())
        {
            rb.velocity = new Vector2(speed,0f);
        }
        else {
            rb.velocity = new Vector2(-speed, 0f);
        }

    }

    private bool lookingRight() {
        return transform.localScale.x > Mathf.Epsilon; //epsilon einai mia polu mikri timi
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("turn"))
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }
        if (collision.gameObject.CompareTag("bullet"))
        {
            deathSoundEffect.Play();
            lives -= 1;
            if (lives == 0) {
                death(); 
            }
            
        }
    }

    private void death()
    {
        animator.SetTrigger("death");
        
        rigidbody.bodyType = RigidbodyType2D.Static;

    }

    private void destroyEnemy()
    {
        Destroy(gameObject);
    }

}
