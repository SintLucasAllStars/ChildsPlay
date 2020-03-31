using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCone : MonoBehaviour
{
    #region vision Cone
    private EnemyBehaviour enemyBehaviour;

    private Vector3 origin;

    private int raycount = 60;
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
        enemyBehaviour = transform.parent.GetComponent<EnemyBehaviour>();

        #region vision cone
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;


        //setting array length
        vertices = new Vector3[raycount + 1 + 1];
        uv = new Vector2[vertices.Length];
        triangles = new int[raycount * 3];
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        //setting starting point
        origin = Vector3.zero;
        vertices[0] = origin;

        //calculating angle increase
        angleIncrease = enemyBehaviour.fov / raycount;
        angle = 90 + (enemyBehaviour.fov / 2);

        //calulating points
        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i < raycount; i++)
        {
            Vector3 vertex;

            //turning angle in to a position
            float angleRad = angle * (Mathf.PI / 180);
            Vector3 position = new Vector3(Mathf.Cos(angleRad), 0, Mathf.Sin(angleRad));

            RaycastHit rayHit;
            Physics.Raycast(origin, position, out rayHit, enemyBehaviour.sightRange);

            print(rayHit.collider);

            if (rayHit.collider == null)
            {
                //adding position to a vertex
                vertex = origin + position * enemyBehaviour.sightRange;
            }
            else
            {
                vertex = rayHit.point;
            }

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

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }
}
