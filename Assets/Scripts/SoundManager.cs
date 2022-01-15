using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip playerDeath, playerShoot, playerCollect, playerJump,levelClear;
    static AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        playerCollect = Resources.Load<AudioClip>("coincollect");
        playerDeath = Resources.Load<AudioClip>("Stab_knife_02");
        playerShoot = Resources.Load<AudioClip>("magic_03");
        playerJump = Resources.Load<AudioClip>("jump");
        levelClear = Resources.Load<AudioClip>("DM-CGS-18");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void playSound(string clip)
    {
        switch (clip)
        {
            case "death":
                audio.PlayOneShot(playerDeath);
                break;
            case "shoot":
                audio.PlayOneShot(playerShoot);
                break;
            case "collect":
                audio.PlayOneShot(playerCollect);
                break;
            case "jump":
                audio.PlayOneShot(playerJump);
                break;
            case "clear":
                audio.PlayOneShot(levelClear);
                break;
        }
    }
}
