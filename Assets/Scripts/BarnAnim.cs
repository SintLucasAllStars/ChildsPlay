using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BarnAnim : MonoBehaviour
{
    Animator Animator;
    public GameObject player;
    public bool winCondition;
    // Start is called before the first frame update
    void Start()
    {
        Animator = gameObject.GetComponent<Animator>();
        winCondition = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<playerBehaviour>().doorOpen == true)
        {
            Animator.SetBool("Haskey", true);
            winCondition = true;
        }
    }
}
