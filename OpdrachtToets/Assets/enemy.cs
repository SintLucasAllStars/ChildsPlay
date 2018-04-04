using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {

    int health = 2;

    Vector3 dir;
    float speed = 0.05f;

    void Start()
    {
        //dir = Random.insideUnitCircle.normalized; << dit werkt ook. Alleen doet ook de z random
        dir = (new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f)).normalized;
    }

    void Update () {
        transform.position += dir * speed;


        if (transform.position.y > 5.06)
            transform.position = new Vector3(transform.position.x, -5.06f, 0f);
        else if(transform.position.y < -5.06)
            transform.position = new Vector3(transform.position.x, 5.06f, 0f);

        if (transform.position.x < -8.93)
            transform.position = new Vector3(8.93f, transform.position.y, 0f);
        else if(transform.position.x > 8.93)
            transform.position = new Vector3(-8.93f, transform.position.y, 0f);


        if (health <= 0)
            Destroy(gameObject);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("bullet"))
        {
            health -= collision.gameObject.GetComponent<Bulletscript>().damage;
            Destroy(collision.gameObject);
        }
    }
}
