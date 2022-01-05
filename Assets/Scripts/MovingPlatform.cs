using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed; //taxutita tis platformas
    public int startPos;
    public Transform[] movingPoints; //einai pinakas giati einai panw apo ena simeio pou paei

    private int i; //index gia tin kinisi tis platformas
    // Start is called before the first frame update
    void Start()
    {
        transform.position = movingPoints[startPos].position; //gia na thesoume tin thesi tis platformas
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, movingPoints[i].position) < 0.02f) {
            i++;
            if (i == movingPoints.Length) {
                i = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, movingPoints[i].position, speed*Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform); //otan pidaei panw stin platforma ginetai i platforma parent ara kineitai
                                                    //o paiktis mazi tis
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null); //gia na papsei na einai parent i platforma
    }
}
