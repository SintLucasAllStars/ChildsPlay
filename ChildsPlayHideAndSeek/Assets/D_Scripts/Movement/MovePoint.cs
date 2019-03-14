using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePoint : MonoBehaviour
{
    public GameObject movementPoint;
    public GameObject Lookingblock;

    private GameObject go;

    private void Start()
    {
        go = Instantiate(movementPoint, Lookingblock.transform.position, Quaternion.identity);
    }

    void Update()
    {
        go.transform.position = Input.mousePosition;
    }
}
