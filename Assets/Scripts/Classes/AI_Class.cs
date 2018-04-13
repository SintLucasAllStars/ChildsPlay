using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Class
{

    public enum Type { FatKid, FastKid, NormalKid, Chaser };
    public Type TypeKid;
    public float stamina;
    public float speed;
    public float reactionSpeed;
    public float fov;

    /* 
	*	the only important thing tp keep in mind is that the stamina should not be higher than 10/11
	*	the reason for this is otherwhise it will take to long for them to lose their breath.
	*/
    public AI_Class(int i)
    {
        int t = Random.Range(0, 4);
        switch (t)
        {
            case 0:
                TypeKid = Type.FatKid;
                stamina = 4f;
                speed = 2.8f;
                fov = 170f;
                break;

            case 1:
                TypeKid = Type.FastKid;
                stamina = 11;
                speed = 4.5f;
                fov = 130f;
                break;

            case 2:
                TypeKid = Type.NormalKid;
                stamina = 6f;
                speed = 3.5f;
                fov = 150f;
                break;
            case 3:
                TypeKid = Type.Chaser;
                stamina = 12;
                speed = 4.2f;
                fov = 145f;
                break;
            default:
                Debug.LogWarning("the switch is onable to reach a certian case");
                break;
        }
    }
}
