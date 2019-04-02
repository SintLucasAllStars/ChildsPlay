using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvQueryItem 
{
   public Vector3 location;
   public Transform QuerierT;
    public bool EnemyNearby;
    public bool IsColliding;
   
    

    public EnvQueryItem(Vector3 NewPosition, Transform QuerierPos)
    {
       this.location = NewPosition;
        this.QuerierT = QuerierPos;
    }

    public bool RunConditionCheck(Transform enemy)
    {
        Ray ray = new Ray(GetWorldLocation() + new Vector3(0,1,0), (enemy.position - GetWorldLocation()));

        if (Physics.Raycast(ray, out RaycastHit hit, 20))
        {
            if (hit.collider.CompareTag("Seeker"))
            {
                return true;
            }
        }
        return false;
    }

    public Vector3 GetWorldLocation()
    {
        return QuerierT.position + location;
    }




    


   
}
