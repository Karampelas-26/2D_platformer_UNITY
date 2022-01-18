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
        
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player").transform;
        Vector2 playerPos = player.position; 
        Vector2 smoothPos = Vector2.Lerp(transform.position,playerPos, fSpeed*Time.deltaTime); 

        transform.position = new Vector3(smoothPos.x, smoothPos.y+posY,-15f);
    }
}
