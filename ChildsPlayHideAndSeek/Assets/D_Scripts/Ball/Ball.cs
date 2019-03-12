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
    }
}
