using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float downwardsGravity;
    private float gravityReducer;

    public AudioClip impact;

    // Use this for initialization
    private void Start()
    {
        downwardsGravity = 0f;
    }

    // Update is called once per frame
    private void Update()
    {
        downwardsGravity = Mathf.Pow(downwardsGravity, downwardsGravity);
        transform.position = transform.position + transform.forward * Time.deltaTime * 140f;
    }

    private void OnCollisionEnter(Collision coll)
    {
        AudioSource.PlayClipAtPoint(impact, transform.position);
        Destroy(gameObject);
    }
}