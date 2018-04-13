using UnityEngine;

public class Node
{

    public bool walkable;
    public Vector3 worldPos;

    public int gridX, gridY;

    public int gCost, hCost;
    public Node parent;

    public Node(bool walkable, Vector3 worldPos, int gridX, int gridY)
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



















