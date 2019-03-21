using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{

    #region Character stuff

    //Object related stuff
    public GameObject Mouth001_Obj, Mouth002_Obj;
    public GameObject Character_Obj;
    public Rigidbody Character_RB;

    //rotation stuff
    public Transform Target_Looking;

    //Movement stuff
    public Camera cam;
    public NavMeshAgent agent;
    private float speed_Moving = 3.5f;
    #endregion

    #region Shooting stuff
    public GameObject Ball_Pfb;
    public Transform SpawnLocation;
    private bool canShoot;
    #endregion

    void Start()
    {
        Mouth001_Obj.name = "Normal";
        Mouth002_Obj.name = "Shooting";
    }

    void Awake()
    {
        Mouth001_Obj.SetActive(true);
        Mouth002_Obj.SetActive(false);
        canShoot = true;
    }

    void FixedUpdate()
    {
        Shooting();
        Looking();
        Moving();
        Inventory();
    }


    void Looking()
    {
        //if (Input.GetMouseButton(1))
        //{
        //    //Always looking at the mouse:
        //    if (Target_Looking != null)
        //    {
        //        transform.LookAt(Target_Looking);
        //    }
        //}

        transform.LookAt(Target_Looking);
    }

    void Moving()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButton(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.speed = speed_Moving;
                agent.SetDestination(hit.point);
            }
        }
    }

    void Inventory()
    {
        if (Input.GetKeyDown(KeyCode.I) && TijdelijkeGameManager.Inventory_B == false)
        {
            TijdelijkeGameManager.Inventory_B = true;
            canShoot = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && TijdelijkeGameManager.Inventory_B == true)
        {
            TijdelijkeGameManager.Inventory_B = false;
            canShoot = true;
        }
        if(Input.GetKeyDown(KeyCode.I) && TijdelijkeGameManager.Inventory_B == true)
        {
            TijdelijkeGameManager.Inventory_B = false;
            canShoot = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Shop")
        {
            TijdelijkeGameManager.shopCanvas_B = true;
            canShoot = false;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Shop")
        {
            TijdelijkeGameManager.shopCanvas_B = false;
            canShoot = true;
        }
    }

    void Shooting()
    {
        if (canShoot == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(IsShooting());
            }
        }
    }


    IEnumerator IsShooting()
    {
        canShoot = false;
        Mouth001_Obj.SetActive(false);
        Mouth002_Obj.SetActive(true);
        Instantiate(Ball_Pfb, SpawnLocation.transform.position, SpawnLocation.transform.rotation);
        yield return new WaitForSeconds(1);
        Mouth001_Obj.SetActive(true);
        Mouth002_Obj.SetActive(false);
        yield return new WaitForSeconds(1);
        canShoot = true;

    }
}
