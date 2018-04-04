using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public Text gameover;
    AudioSource geluid;

    public AudioClip clip;
    

    enum PlayerState {normal, damaged, bezerk};
    PlayerState playerState = PlayerState.normal;

    public GameObject bulletPrefab;

    int currentDamage = 1;
    int bezerkshots;

    int health = 2;

    float moveVertical;
    float moveHorizontal;

    private void Start()
    {
        gameover.enabled = false;
        geluid = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        Vector3 pos = transform.position;
        float speed = 0.5f * health;

        moveHorizontal = Input.GetAxis("Horizontal") / 8;
        moveVertical = Input.GetAxis("Vertical") / 8;

        transform.Translate(moveHorizontal * speed, moveVertical * speed, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (playerState == PlayerState.bezerk)
            {
                currentDamage = 2;
                bezerkshots -= 1;

                if (bezerkshots == 0)
                    playerState = PlayerState.normal;
            } else
            {
                currentDamage = 1;
            }

            GameObject bullet = Instantiate(bulletPrefab, transform.GetChild(0).transform.position, Quaternion.identity);
            bullet.GetComponent<Bulletscript>().damage = currentDamage;
        }

        if (health <= 0)
        {
            Destroy(this.gameObject);
            gameover.enabled = true;
        }



        if (transform.position.y > 5.06)
            transform.position = new Vector3(transform.position.x, -5.06f, 0f);
        else if (transform.position.y < -5.06)
            transform.position = new Vector3(transform.position.x, 5.06f, 0f);

        if (transform.position.x < -8.93)
            transform.position = new Vector3(8.93f, transform.position.y, 0f);
        else if (transform.position.x > 8.93)
            transform.position = new Vector3(-8.93f, transform.position.y, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("enemy"))
        {
            health--;
            playerState = PlayerState.bezerk;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("powerup"))
        {
            Destroy(collision.gameObject);
            playerState = PlayerState.bezerk;
            bezerkshots += 10;
            geluid.PlayOneShot(clip);
        }
    }

}
