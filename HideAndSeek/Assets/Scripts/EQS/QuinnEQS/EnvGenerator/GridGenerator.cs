using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : EnvQueryGenerator
{

    private int GridSize;
    private Transform querierPos;
    

    public GridGenerator()
    {
        this.GridSize = 10;
    }

    public GridGenerator(int Size, Transform QueryPos)
    {
        this.GridSize = Size;
        this.querierPos = QueryPos;
        
    }


    public List<EnvQueryItem> Items(Transform QuerierPos)
    {


        List<EnvQueryItem> NewItems = new List<EnvQueryItem>();


        for (int x = -GridSize /2; x < GridSize/ 2; x++)
        {
            for (int z = -GridSize / 2; z < GridSize / 2; z++)
            {
               
                    NewItems.Add(new EnvQueryItem(new Vector3(x + .5f, 0, z + .5f), QuerierPos));
               
            
                
            }
        }




        return NewItems;
    }



}
