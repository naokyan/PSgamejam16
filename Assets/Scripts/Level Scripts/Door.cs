using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float doorMoveDistance = 4f;
    public float moveSpeed = 2f;
    public float returnDelay = 3f;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    public bool isOpening = false;
    private bool isReturning = false;

    private void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition + new Vector3(0, doorMoveDistance, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isOpening && !isReturning)
        {
            isOpening = true;
        }
    }

    public void Update()
    {
        if (isOpening)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isOpening = false;
                StartCoroutine(ReturnToInitialPosition());
            } 
        }
        if (isReturning)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, initialPosition) < 0.01f)
            {
                isReturning = false;
            }
        }
    }

    private IEnumerator ReturnToInitialPosition()
    {
        yield return new WaitForSeconds(returnDelay);
        isReturning = true;
    }
}
