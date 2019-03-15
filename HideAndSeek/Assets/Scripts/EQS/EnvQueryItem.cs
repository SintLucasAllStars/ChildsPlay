using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvQueryItem 
{
   public Vector3 location;
    public Transform QuerierT;

    public EnvQueryItem(Vector3 NewPosition, Transform QuerierPos)
    {
       this.location = NewPosition;
        this.QuerierT = QuerierPos;
    }

    public void RunConditionCheck()
    {

       //Do a Check Over Here 

    }

    public Vector3 GetWorldLocation()
    {
        return QuerierT.position + location;
    }

    


   
}
