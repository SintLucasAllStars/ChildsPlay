using UnityEngine;

public class GunSound : MonoBehaviour
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
        // gunSound ACtivation
    }

    public void GunSoundCheck()
    {
        managerInstance.SoundCheck(transform.position, 5);
        Debug.Log("pressed");
    }
}