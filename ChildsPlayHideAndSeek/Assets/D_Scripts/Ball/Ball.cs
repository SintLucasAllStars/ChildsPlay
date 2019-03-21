using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    #region Ball stuff
    public Rigidbody ball_Rb;
    float speed = 15;
    #endregion
    private void Start()
    {
        ball_Rb.AddForce(transform.forward * speed, ForceMode.Impulse);

        StartCoroutine(DieTimer());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }

        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }


    IEnumerator DieTimer()
    {
        yield return new WaitForSeconds(10);
        Destroy(this.gameObject);
    }
}
