using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    private DoorStates doorState;
    private Vector3 endPosition;
    private float endPositionZValue;
    private Vector3 startPosition;
    private float startTime;
    private float timeToExecute;
    private float timeToMove;
    private bool wasOpen;


    // Use this for initialization
    private void Start()
    {
        doorState = DoorStates.Open;
    }

    // Update is called once per frame
    private void Update()
    {
        switch (doorState)
        {
            case DoorStates.Closed:
                endPositionZValue = -11.5f;
                wasOpen = false;
                break;

            case DoorStates.Open:
                endPositionZValue = 11.5f;
                wasOpen = true;
                break;

            case DoorStates.Moving:
                break;
        }
    }

    private IEnumerator MoveDoor()
    {
        while (Time.time < startTime + timeToMove)
        {
            doorState = DoorStates.Moving;
            transform.position = Vector3.Lerp(startPosition, endPosition, (Time.time - startTime) / timeToMove);
            yield return null;
        }

        if (wasOpen)
            doorState = DoorStates.Closed;
        else
            doorState = DoorStates.Open;
    }

    private void OnTriggerStay(Collider coll)
    {
        if (Input.GetKey(KeyCode.F) && doorState != DoorStates.Moving)
        {
            startPosition = transform.position;
            timeToMove = 2f;
            timeToExecute = timeToMove;
            endPosition = new Vector3(transform.position.x, transform.position.y,
                transform.position.z + endPositionZValue);
            startTime = Time.time;

            StartCoroutine(MoveDoor());
        }
    }

    private enum DoorStates
    {
        Open,
        Closed,
        Moving
    }
}