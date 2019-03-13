using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePoint : MonoBehaviour
{
    public GameObject movementPoint;
    public GameObject Lookingblock;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Instantiate(movementPoint, Lookingblock.transform.position, Quaternion.identity);
        }
    }
}
