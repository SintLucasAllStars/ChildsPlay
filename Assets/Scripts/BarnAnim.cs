using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class BarnAnim : MonoBehaviour
{
    Animator Animator;
    public GameObject player;
    public bool winCondition;

    // find reference to animator
    void Start()
    {
        Animator = gameObject.GetComponent<Animator>();
        winCondition = false;
    }

    // if door is open play animation
    void Update()
    {
        if(player.GetComponent<playerBehaviour>().doorOpen == true)
        {
            Animator.SetBool("Haskey", true);
            winCondition = true;
            SceneManager.LoadScene("Winscreen");
        }
    }
}
