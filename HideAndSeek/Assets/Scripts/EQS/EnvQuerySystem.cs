using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvQuerySystem : MonoBehaviour
{
    EnvQueryGenerator Generator;

    List<EnvQueryItem> QueryItems;


    private void Awake()
    {
        Generator = new GridGenerator(10);
        if (Generator != null)
        {
            QueryItems = Generator.Items(transform);
        }
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
                item.RunConditionCheck();
            }
        }
    }


    private void OnDrawGizmos()
    {
        if (QueryItems != null)
        {
            foreach (EnvQueryItem item in QueryItems)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(item.location, 0.25f);
            }
        }
       
       


    }
}
