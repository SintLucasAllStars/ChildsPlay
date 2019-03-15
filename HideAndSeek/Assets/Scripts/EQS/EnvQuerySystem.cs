using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvQuerySystem : MonoBehaviour
{
    EnvQueryGenerator Generator;

    List<EnvQueryItem> QueryItems;
    public Transform Querier;
    public Transform Enemy;
    public int GridSize;


    private void Awake()
    {
       
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
        
        
        
        
        
    }
        // Update is called once per frame
    void Update()
    {
        if (QueryItems != null)
        {
            foreach (EnvQueryItem item in QueryItems)
            {
                item.RunConditionCheck(Enemy);
            }
        }
    }


    private void OnDrawGizmos()
    {
        Generator = new GridGenerator(GridSize, Querier, Enemy);

        if (Generator != null)
        {
            QueryItems = Generator.Items(transform);
        }

        if (QueryItems != null)
        {
            foreach (EnvQueryItem item in QueryItems)
            {
                float distance = (item.GetWorldLocation() - Enemy.position).magnitude;

                if (distance < 5)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireSphere(item.GetWorldLocation(), 0.25f);
                }
                else
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawWireSphere(item.GetWorldLocation(), 0.25f);
                }
               
            }
        }
       
       


    }
}
