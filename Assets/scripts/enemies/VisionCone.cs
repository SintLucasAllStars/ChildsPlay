using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCone : MonoBehaviour
{
    #region vision Cone
    private EnemyBehaviour enemyBehaviour;

    private Vector3 origin;
    private Vector3 vertex;
    private RaycastHit rayHit;

    private int raycount = 30;
    float angle;
    float angleIncrease;

    private Mesh mesh;
    Vector3[] vertices;
    Vector2[] uv;
    int[] triangles;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        enemyBehaviour = GetComponent<EnemyBehaviour>();

        #region vision cone
        //creating new mesh
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        //setting array length
        vertices = new Vector3[raycount + 1 + 1];
        uv = new Vector2[vertices.Length];
        triangles = new int[raycount * 3];

        //calculating angle increase
        angleIncrease = enemyBehaviour.fov / raycount;
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        //setting starting point
        origin = Vector3.zero;
        vertices[0] = origin;

        angle = 90 + (enemyBehaviour.fov / 2);

        //calulating points
        int vertexIndex = 1;
        int triangleIndex = 0;

        for (int i = 0; i < raycount; i++)
        {
            //turning angle in to a position
            float angleRad = angle * (Mathf.PI / 180);
            Vector3 position = new Vector3(Mathf.Cos(angleRad), 0, Mathf.Sin(angleRad));

            //firing raycasts
            Physics.Raycast(transform.position, position, out rayHit, enemyBehaviour.sightRange, LayerMask.GetMask("obstacle"));

            if (rayHit.collider == null)
            {
                //adding position to a vertex
                vertex = origin + position * enemyBehaviour.sightRange;
            }
            else
            {
                //adding position to a vertex
                vertex = origin + position * rayHit.distance;
            }

            //adding vertex
            vertices[vertexIndex] = vertex;

            //creating triangles if not on first run
            if (i > 0)
            {
                triangles[triangleIndex] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
            }

            triangleIndex += 3;

            //adjusting varibles for next run
            vertexIndex++;
            angle -= angleIncrease;
        }

        //adding calculated values to mesh
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }
}
