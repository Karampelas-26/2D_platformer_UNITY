using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public float fSpeed;
    public float posY;

    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 playerPos = player.position; 
        Vector2 smoothPos = Vector2.Lerp(transform.position,playerPos, fSpeed*Time.deltaTime); 

        transform.position = new Vector3(smoothPos.x, smoothPos.y+posY,-15f);
    }
}
