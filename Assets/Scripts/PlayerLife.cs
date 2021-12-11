using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rigidbody;
    private bool alive = true;
    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap")) { //an xtupisei kati tupou trap
            Die();
        }
    }

    private void Die() {
        animator.SetTrigger("death");
        animator.SetBool("canJump", false);
        alive = false;
        rigidbody.bodyType = RigidbodyType2D.Static; //gia na min mporei o xaraktiras na kounithei otan pethainei
    }

    private void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //gia na ksanafortwsei to level
    }

    public bool isAlive()
    {
        return alive;
    }
}
