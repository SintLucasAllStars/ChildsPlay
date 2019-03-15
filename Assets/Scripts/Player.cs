using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public bool isDead;
    public GameObject gun;

    // Start is called before the first frame update
    void Start()
    {
        ActivateGun(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collider other)
    {
        if (other.CompareTag("Gun"))
        {
            ActivateGun(true);
            Destroy(other);
        }
    }


    public void ActivateGun(bool b)
    {
        gun.SetActive(b);
    }
}
