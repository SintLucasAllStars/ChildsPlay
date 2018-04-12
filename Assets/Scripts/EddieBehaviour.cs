using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EddieBehaviour : MonoBehaviour
{
    public float awakeDelay = 3f;

    void Awake()
    {
        Invoke("LateAwake", awakeDelay);
    }

    void LateAwake()
    {

    }
}
