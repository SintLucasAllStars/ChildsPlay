using UnityEngine;

public class playerBehaviour : MonoBehaviour
{
    public bool isHidden;
    public bool hasKey;
    public bool doorRange;
    public bool doorOpen;
    public GameObject handKey;
    public GameObject floorKey;

    // Start is called before the first frame update
    void Start()
    {
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
            doorOpen = true;
            handKey.SetActive(false);
        }

        if (hasKey == false)
        {
            handKey.SetActive(false);
            floorKey.SetActive(true);
        }
    }

    //if i move into a bush i will get hidden
    public void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "Bush") 
        {
            isHidden = true;
            Debug.Log("hidden!");
        }

        //if i take the key of the ground hold it on your back
        if (coll.gameObject.tag == "Key")
        {
            hasKey = true;
            Debug.Log("you have the key");
            floorKey.SetActive(false);
            handKey.SetActive(true);
        }

        //if i am inrange of the barn  look if i have a key if not do something
        if (coll.gameObject.tag == "Barn")
        {
            Debug.Log("you dont have a key");
            doorRange = true;
        }

        if (coll.gameObject.tag == "Barn" && hasKey == true)
        {
            Debug.Log("Press E to open the door");
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
        }
    }
}

