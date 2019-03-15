using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvQueryItem 
{
   public Vector3 location;
   public Transform QuerierT;
    public bool EnemyNearby;
   
    

    public EnvQueryItem(Vector3 NewPosition, Transform QuerierPos)
    {
       this.location = NewPosition;
        this.QuerierT = QuerierPos;
        
    }

    public void RunConditionCheck(Transform enemy)
    {

        //Do a Check Over Here 
        float EnemyDistance = (enemy.position - location).magnitude;
        if (EnemyDistance < 100)
        {
            EnemyNearby = true;
        }
        else
        {
            EnemyNearby = false;
        }
        Debug.Log(EnemyDistance);
    }

    public Vector3 GetWorldLocation()
    {
        return QuerierT.position + location;
    }

    


   
}
