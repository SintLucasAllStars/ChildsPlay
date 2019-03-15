using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : EnvQueryGenerator
{

    private int GridSize;
    private Transform querierPos;
    private Transform EnemyT;

    public GridGenerator()
    {
        this.GridSize = 10;
    }

    public GridGenerator(int Size, Transform QueryPos, Transform EnemyT)
    {
        this.GridSize = Size;
        this.querierPos = QueryPos;
        this.EnemyT = EnemyT;
    }


    public List<EnvQueryItem> Items(Transform QuerierPos)
    {


        List<EnvQueryItem> NewItems = new List<EnvQueryItem>();


        for (int x = 0; x < GridSize; x++)
        {
            for (int z = 0; z < GridSize; z++)
            {
                NewItems.Add(new EnvQueryItem(new Vector3(x, 0, z), QuerierPos));
            }
        }




        return NewItems;
    }



}
