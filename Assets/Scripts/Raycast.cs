using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class Raycast : MonoBehaviour
{

    public GameObject[] hidePlaces = new GameObject[4];
    public string[] colors = new string[] { "Blauw", "Geel", "Groen", "Rood" };
    public Animation[] selected = new Animation[4];

    public float range = 100f;
    public Camera fpsCam;

    public Image chosenBlockimage;
    public GameObject chosenMenu;
    public Sprite chosenBlock;
    Color myColor;

    public GameObject holder;
    public GameObject holder2;

    public GameObject Player;
    public GameObject Computer;
    public GameObject MainCamera;

    public string findString;

    public Text findText, wrong;

    bool hold;

    public Camera computerCam;
    public Transform goal;

    public NavMeshAgent agent;

    RaycastHit hit;

    public Transform test;

    // Use this for initialization
    void Start()
    {
        findString = colors[Random.Range(0, 4)];
        findText.text = "Vind " + findString;
        Debug.Log(findString);

        if (gameObject.tag == "Computer")
        {
            //holder = hidePlaces[Random.Range(0, 4)];
            test = hidePlaces[Random.Range(0, 4)].transform;
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            //agent.SetDestination(holder.transform.position);
            agent.SetDestination(test.transform.position);
            Debug.Log("TEST");
        }

        //findString = colors.ToString();
        //Debug.Log(colors);
        //m_MyColor = Color.red;
        //chosenBlockimage.color = m_MyColor;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 forward = transform.TransformDirection(Vector3.forward) * 100;
        Debug.DrawRay(transform.position, forward, Color.green);

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range) && Input.GetButtonDown("Fire1"))
        {
            for (int i = 0; i < hidePlaces.Length; i++)
            {
                if (hit.collider.gameObject == hidePlaces[i].gameObject && hit.transform.tag == findString)
                {
                    Debug.Log("Dit is " + hidePlaces[i] + " breng hem naar de tafel.");
                    selected[i].Play("Selected");
                    myColor = hidePlaces[i].gameObject.GetComponent<Renderer>().material.color;
                    chosenBlockimage.color = myColor;
                    holder = hidePlaces[i];
                    holder2 = Instantiate(holder, new Vector3(355.31f, 17.11f, 346.4f), Quaternion.identity);
                    holder2.SetActive(false);
                    holder.SetActive(false);
                    //Destroy(holder);

                    hold = true;
                }
                else
                {
                    StartCoroutine(Pop());
                }
            }
        }
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range) && Input.GetButtonDown("Fire2") && hit.transform.tag == "Table")
        {
            Place();
        }

        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    transform.LookAt(test);
                    if (hit.transform.tag == findString)
                    {
                        Debug.Log("WOEP");
                    }
                    else
                    {
                        Debug.Log("FOUT");
                    }
                }
            }
        }
    }

    public void Place()
    {
        if (hold == true)
        {
            Done();
        }
        else
        {
            Debug.Log("Je hebt geen blok in je handen!");
        }
    }

    public void Done()
    {
        holder.SetActive(true);
        chosenMenu.SetActive(true);
        MainCamera.SetActive(true);
        Player.SetActive(false);
    }

    public void ComputerTurn()
    {
        Computer.SetActive(true);
        chosenMenu.SetActive(false);
        MainCamera.SetActive(false);

        if (gameObject.tag == "Computer")
        {
            //findString = colors[Random.Range(0, 4)];
            //findText.text = "Vind " + findString;
            //NavMeshAgent agent = GetComponent<NavMeshAgent>();
            //agent.destination = hidePlaces[Random.Range(0, 4)].transform.position;
            //Debug.Log("TEST");
        }
    }

    public void Check()
    {
        //if (navAgent.remainingDistance <= float.Epsilon)
        {
            //Arrived
        }
    }

    IEnumerator Pop()
    {
        wrong.text = "Dit is niet het juiste blok..";
        yield return new WaitForSeconds(5);
        wrong.text = "";
        StopCoroutine(Pop());
    }
}
