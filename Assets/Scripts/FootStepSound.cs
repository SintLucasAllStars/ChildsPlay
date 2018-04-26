using UnityEngine;

public class FootStepSound : MonoBehaviour
{
    public GameManager managerInstance;

    // Use this for initialization
    private void Start()
    {
        managerInstance = GameObject.Find("Managers").GetComponent<GameManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            managerInstance.SoundCheck(transform.position, 1);
            Debug.Log("pressed");
        }
    }
}