using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public BoxCollider2D laserCollider;
     
    public Vector3 pointA;
    public Vector3 pointB;
    public float speed = 2f;
    public Vector3 target;
    private bool canMove;

    private void Start()
    {
        laserCollider = GetComponent<BoxCollider2D>();
        target = pointA;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            laserCollider.enabled = false;
        else
            laserCollider.enabled = true;

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.01f)
            target = target == pointA ? pointB : pointA;
    }
} 
