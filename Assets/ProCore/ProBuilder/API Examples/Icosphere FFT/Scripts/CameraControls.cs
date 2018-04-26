/**
 * Camera orbit controls.
 */

using UnityEngine;

namespace ProBuilder2.Examples
{
    public class CameraControls : MonoBehaviour
    {
        private const string INPUT_MOUSE_SCROLLWHEEL = "Mouse ScrollWheel";
        private const string INPUT_MOUSE_X = "Mouse X";
        private const string INPUT_MOUSE_Y = "Mouse Y";
        private const float MIN_CAM_DISTANCE = 10f;
        private const float MAX_CAM_DISTANCE = 40f;

        private Vector2 dir = new Vector2(.8f, .2f);

        // the current distance from pivot point (locked to Vector3.zero)
        private float distance;

        // how fast the idle camera movement is
        public float idleRotation = 1f;

        // how fast the camera orbits
        [Range(2f, 15f)] public float orbitSpeed = 6f;

        // how fast the camera zooms in and out
        [Range(.3f, 2f)] public float zoomSpeed = .8f;

        private void Start()
        {
            distance = Vector3.Distance(transform.position, Vector3.zero);
        }

        private void LateUpdate()
        {
            var eulerRotation = transform.localRotation.eulerAngles;
            eulerRotation.z = 0f;

            // orbits
            if (Input.GetMouseButton(0))
            {
                var rot_x = Input.GetAxis(INPUT_MOUSE_X);
                var rot_y = -Input.GetAxis(INPUT_MOUSE_Y);

                eulerRotation.x += rot_y * orbitSpeed;
                eulerRotation.y += rot_x * orbitSpeed;

                // idle direction is derived from last user input.
                dir.x = rot_x;
                dir.y = rot_y;
                dir.Normalize();
            }
            else
            {
                eulerRotation.y += Time.deltaTime * idleRotation * dir.x;
                eulerRotation.x += Time.deltaTime * Mathf.PerlinNoise(Time.time, 0f) * idleRotation * dir.y;
            }

            transform.localRotation = Quaternion.Euler(eulerRotation);
            transform.position = transform.localRotation * (Vector3.forward * -distance);

            if (Input.GetAxis(INPUT_MOUSE_SCROLLWHEEL) != 0f)
            {
                var delta = Input.GetAxis(INPUT_MOUSE_SCROLLWHEEL);

                distance -= delta * (distance / MAX_CAM_DISTANCE) * (zoomSpeed * 1000) * Time.deltaTime;
                distance = Mathf.Clamp(distance, MIN_CAM_DISTANCE, MAX_CAM_DISTANCE);
                transform.position = transform.localRotation * (Vector3.forward * -distance);
            }
        }
    }
}