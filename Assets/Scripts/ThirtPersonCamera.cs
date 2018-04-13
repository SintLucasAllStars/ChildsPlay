using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirtPersonCamera : MonoBehaviour {
    public float mouseSpeed = 1;

    private const float Y_ANGLE_MIN = 0f;
    private const float Y_ANGLE_MAX = 50f;
    public Transform lookAt;
    public Transform camTransform;
    float cameraDistanceMax = 8f;
    float cameraDistanceMin = 2f;
    float scrollSpeed = 2f;

    private Camera cam;
    [Header("SettingsCamera")]
    public float distance = 10.0f;
    public float currentX = 0.0f;
    public float currentY = 12.0f;
    public float sensivityX = 3.0f;
    public float sensivityY = 3.0f;

    private void Start()
    {
        camTransform = transform;
        cam = Camera.main;
    }

    private void Update()
    {
        
        distance += Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        distance = Mathf.Clamp(distance, cameraDistanceMin, cameraDistanceMax);
        currentX += Input.GetAxis("Mouse X");
        currentY += Input.GetAxis("Mouse Y");

        float X = Input.GetAxis("Mouse X") * mouseSpeed;
        float Y = Input.GetAxis("Mouse Y") * mouseSpeed;
        lookAt.Rotate(0, X, 0);
        if (cam.transform.eulerAngles.x + (-Y) > 80 && cam.transform.eulerAngles.x + (-Y) < 280)
        { }
        else
        {

            cam.transform.RotateAround(lookAt.position, cam.transform.right, -Y);
        }
        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = lookAt.position + rotation * dir;
        camTransform.LookAt(lookAt.position);
    }
}
