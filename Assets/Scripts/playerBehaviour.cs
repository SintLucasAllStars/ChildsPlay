using UnityEngine;

public class playerBehaviour : MonoBehaviour
{
    public bool isHidden;

    // Start is called before the first frame update
    void Start()
    {
        isHidden = false;
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
    }

    //if i move into a bush i will get hidden
    public void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "Bush") 
        {
            isHidden = true;
            Debug.Log("hidden!");
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

