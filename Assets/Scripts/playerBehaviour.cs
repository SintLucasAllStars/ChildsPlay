using UnityEngine;
using UnityEngine.UI;

public class playerBehaviour : MonoBehaviour
{
    public bool isHidden;
    public bool hasKey;
    public bool doorRange;
    public bool doorOpen;
    public GameObject handKey;
    public GameObject floorKey;
    public GameObject pressE;
    public GameObject nokey;
    public GameObject keyUI;
    public GameObject hidden;
    public GameObject objective1;
    public GameObject objective2;

    // Start is called before the first frame update
    void Start()
    {
        objective2.SetActive(false);
        objective1.SetActive(true);
        hidden.SetActive(false);
        keyUI.SetActive(false);
        nokey.SetActive(false);
        pressE.SetActive(false);
        isHidden = false;
        hasKey = false;
        doorRange = false;
        doorOpen = false;
    }
    

    // Update is called once per frame
    // Every movement
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0, 0.025f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, -0.025f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-0.025f, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(0.025f, 0, 0);
        }

        //if i put key in door open the door
        if (hasKey == true && doorRange == true && Input.GetKey(KeyCode.E))
        {
            keyUI.SetActive(false);
            pressE.SetActive(false);
            doorOpen = true;
            handKey.SetActive(false);
            objective2.SetActive(false);
        }

        if (hasKey == false)
        {
            handKey.SetActive(false);
            floorKey.SetActive(true);
            keyUI.SetActive(false);
        }
    }

    //if i move into a bush i will get hidden
    public void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "Bush") 
        {
            isHidden = true;
            Debug.Log("hidden!");
            hidden.SetActive(true);
        }

        //if i take the key of the ground hold it on your back
        if (coll.gameObject.tag == "Key")
        {
            hasKey = true;
            Debug.Log("you have the key");
            floorKey.SetActive(false);
            handKey.SetActive(true);
            keyUI.SetActive(true);
            objective1.SetActive(false);
            objective2.SetActive(true);
        }

        //if i am inrange of the barn  look if i have a key if not do something
        if (coll.gameObject.tag == "Barn" && hasKey == false)
        {
            Debug.Log("you dont have a key");
            doorRange = true;
            nokey.SetActive(true);
        }

        if (coll.gameObject.tag == "Barn" && hasKey == true)
        {
            Debug.Log("Press E to open the door");
            pressE.SetActive(true);
            doorRange = true;
        }
    }
    //if i move out of a bush i dont get hidden
    public void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Bush")
        {
            isHidden = false;
            Debug.Log("not hidden!");
            hidden.SetActive(false);
        }

        if (coll.gameObject.tag == "Barn")
        {
            Debug.Log("you dont have a key");
            doorRange = true;
            nokey.SetActive(false);
        }
    }
}

