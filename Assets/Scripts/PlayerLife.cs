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
    [SerializeField] private AudioSource collectionSoundEffect;
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
        if (collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("edge") || collision.gameObject.CompareTag("enemy")) { //an xtupisei kati tupou trap i pesei sto keno
            Die();
            
        }
    }

    private void Die() {
        lives=lives -1;
        livesCountText.text = "Lives : " + lives;
        SoundManager.playSound("death");
        animator.SetTrigger("death");
        animator.SetBool("canJump", false);
        alive = false;
        rigidbody.bodyType = RigidbodyType2D.Static; //gia na min mporei o xaraktiras na kounithei otan pethainei
    }

    private IEnumerator Restart() {
        if (lives == 0)
        {
            StaticClass.CrossSceneInformation = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("OnDeath");
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

        if (collision.gameObject.CompareTag("heart"))
        {
            collectionSoundEffect.Play();   //sound effect when coin selected
            Destroy(collision.gameObject);  // destroys coin object completely
            lives++;
            livesCountText.text = "Lives : " + lives; //prints score on screen
        }
    }

    public bool isAlive()
    {
        return alive;
    }
}
