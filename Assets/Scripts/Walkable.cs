using UnityEngine;

public class Walkable
{

    public bool walkable;
    public Vector3 worldPos;

    public int gridX, gridY;

    public int gCost, hCost;
	public Walkable parent;

	public Walkable(bool walkable, Vector3 worldPos, int gridX, int gridY)
    {
        this.worldPos = worldPos;
        this.walkable = walkable;

        this.gridX = gridX;
        this.gridY = gridY;
    }

    public int fCost()
    {
        return gCost + hCost;
    }
	
}



















