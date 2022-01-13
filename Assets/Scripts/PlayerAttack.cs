using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldwon;
    [SerializeField] private Transform firePoints;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] public AudioClip onShootSound;
    public AudioSource audio;

    private float cooldownTimer = Mathf.Infinity;


    private Animator animator;
    private PlayerMovement playermovement;
    private PlayerLife playerLife;


    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        playermovement = GetComponent<PlayerMovement>();
        playerLife = GetComponent<PlayerLife>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldwon && playerLife.isAlive())
        {
            SoundManager.playSound("shoot");
            attack();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void attack()
    {
        animator.SetTrigger("attack");
        cooldownTimer = 0;

        fireballs[FindFireball()].transform.position = firePoints.position;
        fireballs[FindFireball()].GetComponent<Porjectile>().SetDirection(Mathf.Sign(transform.rotation.y));

    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}
