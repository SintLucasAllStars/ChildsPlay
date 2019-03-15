using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvQueryItem 
{
   public Vector3 location;

    public EnvQueryItem(Vector3 NewPosition)
    {
       this.location = NewPosition;
    }

    public void RunConditionCheck()
    {

        Debug.Log("Check");

    }

    


   
}
