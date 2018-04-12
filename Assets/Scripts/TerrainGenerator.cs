using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class TerrainGenerator : MonoBehaviour
{
    Terrain terrain;

    public int width;
    public int height;

    [Range(10, 15)] public int depth;
    private float scale = 2f;

    private NavMeshSurface surface;     //this script is used for baking navmesh in runtime


    void Start()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
        surface = GetComponent<NavMeshSurface>();
        surface.BuildNavMesh();
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }
    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                heights[x, y] = CalculateHeight(x, y);
            }
        }
        return heights;
    }
    float CalculateHeight(int x, int y)
    {
        float xCoord = (float)x / width * scale;
        float yCoord = (float)y / height * scale;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }

    /// <summary>
    /// will give you back the psotion of y postion 
    /// of the given x z axis
    /// </summary>
    /// <param name="X postion"> </param>
    /// <param name="Z position"></param>
    public float ReturnHeight(float x, float z)
    {
        Vector3 pos = new Vector3(x, 0, z);
        pos.y = Terrain.activeTerrain.SampleHeight(pos) + Terrain.activeTerrain.GetPosition().y;
        return (pos.y);
    }


}
