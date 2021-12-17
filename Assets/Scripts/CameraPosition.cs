using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float posY;

    private void Update()
    {
        //transform.position = new Vector3(player.position.x+2, posY, transform.position.z);
        Vector3 cameraPos = GameObject.Find("Player").transform.position;
        Vector3 playerPos = new Vector3(GameObject.Find("Player").transform.position.x+3, posY, gameObject.transform.position.z);
        gameObject.transform.position = playerPos;

    }
}