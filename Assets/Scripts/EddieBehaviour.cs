using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EddieBehaviour : MonoBehaviour
{
    public float awakeDelay;

    void Awake()
    {
        Invoke("LateAwake", awakeDelay);
    }

    /// <summary>
    /// will call any code inside of the function by a delay
    /// </summary>
    void LateAwake()
    {
        Debug.Log("dsds");
    }
}
