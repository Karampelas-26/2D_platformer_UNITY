using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private Text livesCountText;   //prints lives on screen

    private Animator animator;
    private Rigidbody2D rigidbody;
    private bool alive = true;
    private PlayerMovement playerm;
    private Vector3 respawnPoint;
    [SerializeField ]private int lives = 3;
    private CameraPosition camera;
    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        playerm = GetComponent<PlayerMovement>();
        respawnPoint = transform.position;
        camera = GetComponent<CameraPosition>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("edge")) { //an xtupisei kati tupou trap i pesei sto keno
            Die();
            
        }
    }

    private void Die() {
        lives=lives -1;
        livesCountText.text = "Lives : " + lives;
        animator.SetTrigger("death");
        animator.SetBool("canJump", false);
        alive = false;
        rigidbody.bodyType = RigidbodyType2D.Static; //gia na min mporei o xaraktiras na kounithei otan pethainei
    }

    private IEnumerator Restart() {
        if (lives == 0)
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name); //gia na ksanafortwsei to level apo tin arxi
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
        else
        {
            Destroy(gameObject,1.11f);
            yield return new WaitForSeconds(1.1f);
            rigidbody.bodyType = RigidbodyType2D.Dynamic;
            animator.SetBool("canJump", true);
            transform.position = respawnPoint;
            GameObject newplayer=(GameObject)Instantiate(gameObject, respawnPoint, Quaternion.identity);
            newplayer.name = playerm.name;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "checkpoint") {
            respawnPoint = transform.position; //gia na kanei respawn sto checkpoint
        }
    }

    public bool isAlive()
    {
        return alive;
    }
}
