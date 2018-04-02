using UnityEngine;
using UnityEngine.AI;

public class TerrainGenerator : MonoBehaviour {
	public int width = 256;
	public int height = 256;

	public int depth = 30;
	public float scale = 3f;
	
	public NavMeshSurface surface;


	void Start()
	{
		depth = 30;
		scale = 3f;
		Terrain terrain = GetComponent<Terrain>();
		terrain.terrainData = GenerateTerrain (terrain.terrainData);
		surface.BuildNavMesh();
	}

	TerrainData GenerateTerrain(TerrainData terrainData) {
		terrainData.heightmapResolution = width + 1;
		terrainData.size = new Vector3 (width, depth, height);
		terrainData.SetHeights (0, 0, GenerateHeights());
		return terrainData;
	}
	float[,] GenerateHeights ()
	{
		float[,] heights = new float[width, height];
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y< height; y++)
			{
				heights [x, y] = CalculateHeight (x, y);
			}
		}
		return heights;
	}
	float CalculateHeight (int x, int y)
	{
		//Debug.Log(seed);
		float xCoord = (float)x / width * scale;
		float yCoord = (float)y / height * scale;

		return Mathf.PerlinNoise (xCoord, yCoord);
	}
}
