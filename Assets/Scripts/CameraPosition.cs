using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float posY = 0;

    private void Update()
    {
        transform.position = new Vector3(player.position.x, posY, transform.position.z);
    }
}