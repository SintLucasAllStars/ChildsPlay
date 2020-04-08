using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed = 10;
    public float acceleration = 20;

    private CharacterController pc;
    private Vector3 velocity;

    private void Start() {
        pc = GetComponent<CharacterController>();
    }

    void Update() {
        Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        velocity -= velocity.normalized * Mathf.Min(velocity.magnitude, acceleration * Time.deltaTime);
        velocity += dir.normalized * acceleration * 3 * Time.deltaTime;
        if (velocity.magnitude > speed) velocity = velocity.normalized * speed;
        pc.Move(velocity * Time.deltaTime);
    }
}
