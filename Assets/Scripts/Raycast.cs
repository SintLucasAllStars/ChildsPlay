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
    public bool check = false;
    public bool next = false;

    public Camera computerCam;
    public Transform goal;

    public NavMeshAgent agent;

    RaycastHit hit;

    public Transform test;
    public int lives = 2;


    // Use this for initialization
    void Start()
    {
        findString = colors[Random.Range(0, 4)];
        findText.text = "Find " + findString;
        Debug.Log(findString);
        check = true;

        if (MainCamera.activeInHierarchy)
        {
            next = true;
        }

        if (gameObject.tag == "Computer")
        {
            wrong.GetComponent<Text>().enabled = false;
            myColor = chosenBlockimage.gameObject.GetComponent<Image>().color = Color.white;
            chosenBlockimage.color = myColor;
            test = hidePlaces[Random.Range(0, 4)].transform;
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.SetDestination(test.transform.position);
        }
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
                    wrong.GetComponent<Text>().enabled = false;
                    Player.tag = "Hide";
                    selected[i].Play("Selected");
                    myColor = hidePlaces[i].gameObject.GetComponent<Renderer>().material.color;
                    chosenBlockimage.color = myColor;
                    holder = hidePlaces[i];
                    holder2 = Instantiate(holder, new Vector3(355.31f, 17.11f, 346.4f), Quaternion.identity);
                    holder2.SetActive(false);
                    holder.SetActive(false);
                    findText.text = "Right click on the podium";

                    hold = true;
}
                else
                {
                    if (Player.tag == "Player" && check == true)
                    {
                        Debug.Log("WOES");
                        StartCoroutine(Pop());
                    }
                }
            }
        }
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range) && Input.GetButtonDown("Fire2") && hit.transform.tag == "Table")
        {
            Place();
        }

        if (gameObject.tag == "Computer")
        {
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        transform.LookAt(test);
                        if (hit.transform.tag == findString)
                        {
                            myColor = test.gameObject.GetComponent<Renderer>().material.color;
                            chosenBlockimage.color = myColor;
                            SceneManager.LoadScene("Gewonnen");
                        }
                        else
                        {
                            wrong.GetComponent<Text>().enabled = true;
                            check = true;
                            test.tag = "Hide";
                            lives--;
                            StartCoroutine(Poppc());
                            Next();
                        }
                    }
                }
            }
        }

        if (Input.GetKeyDown("space") && next == true)
        {
            Computer.SetActive(true);
            chosenMenu.SetActive(false);
            MainCamera.SetActive(false);
            wrong.GetComponent<Text>().enabled = true;
        }
    }

    public void Place()
    {
        if (hold == true)
        {
            Player.SetActive(false);
            MainCamera.SetActive(true);
            Done();
        }
        else
        {
            Debug.Log("You do not have a block in your hands!");
        }
    }

    public void Done()
    {
        holder.SetActive(true);
        chosenMenu.SetActive(true);
    }

    public void Next()
    {
        {
            test = hidePlaces[Random.Range(0, 4)].transform;
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.SetDestination(test.transform.position);

            if (lives == 0)
            {
                SceneManager.LoadScene("Gameover");
            }
        }
    }

    IEnumerator Pop()
    {
        if (Player.tag == "Hide")
        {
            check = false;
        }

        if (Player.tag == "Player" && check == true)
        {
            wrong.text = "This is the wrong block!";
            yield return new WaitForSeconds(5);
            wrong.text = "";
            StopCoroutine(Pop());
        }
    }

    IEnumerator Poppc()
    {
        if (test.tag == "Hide" && check == true)
        {
            wrong.text = "This is the wrong block!";
            yield return new WaitForSeconds(5);
            wrong.text = "";
            StopCoroutine(Pop());
        }
    }
}
