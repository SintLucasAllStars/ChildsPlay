using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_BehaviourTest : MonoBehaviour
{

	public int Size;
	public float Offset; 
	public GameObject m_Obj; 


    // Start is called before the first frame update
    void Start()
    {
		for (int z = 0; z < Size; z++)
		{
			for (int x = 0; x < Size; x++)
			{
				Instantiate(m_Obj, new Vector3(x, 0, z), Quaternion.identity);

			}
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
