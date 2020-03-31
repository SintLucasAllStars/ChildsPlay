using UnityEngine;

public class playerBehaviour : MonoBehaviour
{
    public bool isHidden;
    public bool hasKey;
    public GameObject handKey;
    public GameObject floorKey;

    // Start is called before the first frame update
    void Start()
    {
        isHidden = false;
        hasKey = false;
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

        if (hasKey == true && Input.GetKey(KeyCode.E))
        {
            Debug.Log("WIN!!!");
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

        if (coll.gameObject.tag == "Key")
        {
            hasKey = true;
            Debug.Log("you have the key");
            floorKey.SetActive(false);
            handKey.SetActive(true);
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

