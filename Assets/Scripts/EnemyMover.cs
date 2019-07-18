using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 pointA;
    private Vector2 pointB;
    public float pointC;
    public float pointD;
    private float objectPosition;
    
    void Start()
    {
        objectPosition = transform.position.x;
        pointA = new Vector2(objectPosition, pointC);
        pointB = new Vector2(objectPosition, pointD);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.Lerp(pointA, pointB, Mathf.PingPong(Time.time * 0.5f, 1));
    }
}
