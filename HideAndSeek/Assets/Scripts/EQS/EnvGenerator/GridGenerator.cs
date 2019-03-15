using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : EnvQueryGenerator
{

    private int GridSize;


    public GridGenerator()
    {
        this.GridSize = 10;
    }

    public GridGenerator(int Size)
    {
        this.GridSize = Size;
    }


    public List<EnvQueryItem> Items(Transform Positions)
    {


        List<EnvQueryItem> NewItems = new List<EnvQueryItem>();


        for (int x = 0; x < GridSize; x++)
        {
            for (int z = 0; z < GridSize; z++)
            {
                NewItems.Add(new EnvQueryItem(new Vector3(x, 0, z)));
            }
        }




        return NewItems;
    }



}
