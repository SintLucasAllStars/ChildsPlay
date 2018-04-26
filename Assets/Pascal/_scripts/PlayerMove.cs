using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float mouseSensitivity;
    private float mouseX;
    private float mouseY;
    private float moveSpeed;
    private float rotAmountX;
    private float rotAmountY;
    private Vector3 targetRotation;
    private float xAxisClamp;

    // Use this for initialization
    private void Start()
    {
        moveSpeed = 40f;
        xAxisClamp = 0f;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.W)) transform.Translate(0f, 0f, moveSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.S)) transform.Translate(0f, 0f, -moveSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.A)) transform.Translate(-moveSpeed * Time.deltaTime, 0f, 0f);
        if (Input.GetKey(KeyCode.D)) transform.Translate(moveSpeed * Time.deltaTime, 0f, 0f);

        RotateCamera();
    }

    private void RotateCamera()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        rotAmountX = mouseX * mouseSensitivity;
        rotAmountY = mouseY * mouseSensitivity;

        xAxisClamp -= rotAmountY;

        targetRotation = transform.rotation.eulerAngles;
        targetRotation.x -= rotAmountY;
        targetRotation.y += rotAmountX;

        if (xAxisClamp > 90)
        {
            xAxisClamp = 90;
            targetRotation.x = 90;
        }
        else if (xAxisClamp < -90)
        {
            xAxisClamp = -90;
            targetRotation.x = 270;
        }

        transform.rotation = Quaternion.Euler(targetRotation);
    }
}