using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
	private GameObject player;
	private Vector3 target;
	
	private enum EnemyState
	{
		Roam,
		Chase,
	}

	private EnemyState state;
	private float playerSeen;
	private List<Walkable> path;

	public LayerMask wallMask;
	public Vector2 worldSize;
	public float nodeRadius;
	private Walkable[,] grid;
	private float nodeDiameter;
	private int gridSizeX, gridSizeY;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		target = player.transform.position;
		
		nodeDiameter = nodeRadius * 2;
		gridSizeX = Mathf.RoundToInt(worldSize.x/nodeDiameter);
		gridSizeY = Mathf.RoundToInt(worldSize.y/nodeDiameter);
		
		state = EnemyState.Roam;

		StartCoroutine(RoamBehaviour());
	}
	
	void Update ()
	{
		playerSeen += Time.deltaTime;
		
		if (CanSeePlayer())
		{
			state = EnemyState.Chase;
			playerSeen = 0;
		}
		else if (state == EnemyState.Chase && playerSeen > 3)
		{
			state = EnemyState.Roam;
		}
		
		MoveEnemy();
	}
	
	bool CanSeePlayer()
	{
		RaycastHit hit;

		Vector3 direction = player.transform.position - transform.position;
		if(Physics.Raycast(transform.position, direction, out hit))
		{
			if(hit.collider.gameObject.Equals(player))
			{
				return true;
			}
		}
		return false;
	}

	private void MoveEnemy()
	{
		if (state == EnemyState.Chase)
		{
			target = player.transform.position;
			updatePath();
			if (path.Count == 0)
			{
				return;
			}
			transform.position = Vector3.MoveTowards(transform.position, path[0].worldPos, 3f*Time.deltaTime);
		}
		else
		{
			updatePath();
			if (path.Count == 0)
			{
				return;
			}
			transform.position = Vector3.MoveTowards(transform.position, path[0].worldPos, 3f*Time.deltaTime);
		}
	}

	private void updatePath()
	{
		createGrid();
		findPath();
	}
	
	IEnumerator RoamBehaviour()
	{
		while(true)
		{
			if(state == EnemyState.Roam)
			{
				target = transform.position + new Vector3(Random.Range(-10, 10), 0f, Random.Range(-10, 10));
			}
			yield return new WaitForSeconds(Random.Range(3,7));
		}
	}

	private void createGrid()
	{
		grid = new Walkable[gridSizeX,gridSizeY];

		Vector3 bottomLeft = transform.position - Vector3.right * worldSize.x / 2 - Vector3.forward * worldSize.y / 2;

		for (int x = 0; x < gridSizeX; x++)
		{
			for (int y = 0; y < gridSizeY; y++)
			{
				Vector3 worldPoint = bottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
				bool walkable = !Physics.CheckSphere(worldPoint, nodeRadius, wallMask);
				grid[x,y] = new Walkable(walkable, worldPoint, x, y);
			}
		}
		
	}

	public Walkable nodeFromWorldPos(Vector3 pos)
	{
		float percentX, percentY;
		percentX = (pos.x - transform.position.x + worldSize.x / 2) / worldSize.x;
		percentY = (pos.z - transform.position.z + worldSize.y / 2) / worldSize.y;

		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		int x, y;
		x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
		y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
		
		return grid[x, y];
	}

	public List<Walkable> getNeighbours(Walkable node)
	{
		List<Walkable> neighbours = new List<Walkable>();

		for (int x = -1; x <= 1; x++)
		{
			for (int y = -1; y <= 1; y++)
			{
				if (x == 0 && y == 0)
				{
					continue;
				}
				int checkX, checkY;

				checkX = node.gridX + x;
				checkY = node.gridY + y;

				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
				{
					neighbours.Add(grid[checkX, checkY]);
				}
			}
		}
		return neighbours;
	}

	public void findPath()
	{
		
		// Start node
		Walkable startWalkable = nodeFromWorldPos(transform.position);
		// End node
		Walkable endWalkable = nodeFromWorldPos(target);
		
		// A list of all open nodes
		List<Walkable> openWalkables = new List<Walkable>();
		// A hashset of all closed nodes
		HashSet<Walkable> closedWalkables = new HashSet<Walkable>();
		
		// Add start node to open nodes
		openWalkables.Add(startWalkable);

		// While there are open nodes keep looping
		while (openWalkables.Count > 0)
		{
			
			// Get the node with the lowers f cost
			Walkable currentWalkable = openWalkables[0];
			for (int i = 0; i < openWalkables.Count; i++)
			{
				if (openWalkables[i].fCost() < currentWalkable.fCost() || openWalkables[i].fCost() == currentWalkable.fCost() && openWalkables[i].hCost < currentWalkable.hCost)
				{
					currentWalkable = openWalkables[i];
				}
			}

			// Remove node from open nodes and add to close nodes
			openWalkables.Remove(currentWalkable);
			closedWalkables.Add(currentWalkable);

			// If node is end node finished path
			if (currentWalkable == endWalkable)
			{
				// A list with all the nodes used in the path
				path = getPath(currentWalkable);
				return;
			}

			foreach (Walkable neighbour in getNeighbours(currentWalkable))
			{
				if (!neighbour.walkable || closedWalkables.Contains(neighbour))
				{
					continue;
				}

				int newMovementCost = currentWalkable.gCost + getDistance(currentWalkable, neighbour);

				if (newMovementCost < currentWalkable.gCost || !openWalkables.Contains(neighbour))
				{
					neighbour.gCost = newMovementCost;
					neighbour.hCost = getDistance(neighbour, endWalkable);
					neighbour.parent = currentWalkable;
					if (!openWalkables.Contains(neighbour))
					{
						openWalkables.Add(neighbour);
					}
				}
			}
		}
	}

	private List<Walkable> getPath(Walkable endWalkable)
	{
		List<Walkable> path = new List<Walkable>();

		Walkable currentWalkable = endWalkable;

		while (currentWalkable.parent != null)
		{
			path.Add(currentWalkable);
			currentWalkable = currentWalkable.parent;
		}

		path.Reverse();
		
		return path;

	}

	private int getDistance(Walkable nodeA, Walkable nodeB)
	{
		int disX, disY;
		disX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
		disY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

		if (disX > disY)
		{
			return 14 * disY + 10 * (disX - disY);
		}
		return 14 * disX + 10 * (disY - disX);
	}
	
	private void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(worldSize.x, 1, worldSize.y));

		if (grid != null)
		{
			Walkable playerWalkable = nodeFromWorldPos(target);
			
			foreach (Walkable n in grid)
			{

				if (n == playerWalkable)
				{
					Gizmos.color = Color.yellow;
					Gizmos.DrawCube(n.worldPos, Vector3.one * (nodeDiameter - .1f));
				}

				if (path != null && path.Contains(n))
				{
					Gizmos.color = Color.blue;
					Gizmos.DrawCube(n.worldPos, Vector3.one * (nodeDiameter - .1f));
				}
				else
				{
					Gizmos.color = n.walkable ? Color.green : Color.red;
					
					Gizmos.DrawCube(n.worldPos, Vector3.one * (nodeDiameter - .1f));
				}
				
			}
			
		}
		
	}
	
}