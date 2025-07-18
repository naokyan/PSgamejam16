using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLocked : MonoBehaviour
{
    public ButtonScript button1;
    public ButtonScript button2;
    public float doorMoveDistance = 4f;
    public float moveSpeed = 2f;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool isOpening = false;

    private void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition + new Vector3(0, doorMoveDistance, 0);
    }

    private void Update()
    {
        if (button1.buttonActivated && button2.buttonActivated && !isOpening)
        {
            isOpening = true;
        }

        if (isOpening)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isOpening = false;
            }
        }
    }
}
