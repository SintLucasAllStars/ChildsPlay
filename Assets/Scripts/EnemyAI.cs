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
	public float walkableRadius;
	private Walkable[,] grid;
	private float walkableDiameter;
	private int gridSizeX, gridSizeY;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		target = player.transform.position;
		
		walkableDiameter = walkableRadius * 2;
		gridSizeX = Mathf.RoundToInt(worldSize.x/walkableDiameter);
		gridSizeY = Mathf.RoundToInt(worldSize.y/walkableDiameter);
		
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
			transform.position = Vector3.MoveTowards(transform.position, path[0].worldPos, 2f*Time.deltaTime);
		}
		else
		{
			updatePath();
			if (path.Count == 0)
			{
				return;
			}
			transform.position = Vector3.MoveTowards(transform.position, path[0].worldPos, 2f*Time.deltaTime);
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
				Vector3 worldPoint = bottomLeft + Vector3.right * (x * walkableDiameter + walkableRadius) + Vector3.forward * (y * walkableDiameter + walkableRadius);
				bool walkable = !Physics.CheckSphere(worldPoint, walkableRadius, wallMask);
				grid[x,y] = new Walkable(walkable, worldPoint, x, y);
			}
		}
		
	}

	public Walkable walkableFromWorldPos(Vector3 pos)
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

	public List<Walkable> getNeighbours(Walkable walkable)
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

				checkX = walkable.gridX + x;
				checkY = walkable.gridY + y;

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
		
		// Start walkable
		Walkable startWalkable = walkableFromWorldPos(transform.position);
		// End walkable
		Walkable endWalkable = walkableFromWorldPos(target);
		
		// A list of all open walkables
		List<Walkable> openWalkables = new List<Walkable>();
		// A hashset of all closed walkables
		HashSet<Walkable> closedWalkables = new HashSet<Walkable>();
		
		// Add start walkable to open walkables
		openWalkables.Add(startWalkable);

		// While there are open walkables keep looping
		while (openWalkables.Count > 0)
		{
			
			// Get the walkable with the lowers f cost
			Walkable currentWalkable = openWalkables[0];
			for (int i = 0; i < openWalkables.Count; i++)
			{
				if (openWalkables[i].fCost() < currentWalkable.fCost() || openWalkables[i].fCost() == currentWalkable.fCost() && openWalkables[i].hCost < currentWalkable.hCost)
				{
					currentWalkable = openWalkables[i];
				}
			}

			// Remove walkable from open walkables and add to close walkables
			openWalkables.Remove(currentWalkable);
			closedWalkables.Add(currentWalkable);

			// If walkable is end walkable finished path
			if (currentWalkable == endWalkable)
			{
				// A list with all the walkables used in the path
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

	private int getDistance(Walkable walkableA, Walkable walkableB)
	{
		int disX, disY;
		disX = Mathf.Abs(walkableA.gridX - walkableB.gridX);
		disY = Mathf.Abs(walkableA.gridY - walkableB.gridY);

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
			Walkable playerWalkable = walkableFromWorldPos(target);
			
			foreach (Walkable n in grid)
			{

				if (n == playerWalkable)
				{
					Gizmos.color = Color.yellow;
					Gizmos.DrawCube(n.worldPos, Vector3.one * (walkableDiameter - .1f));
				}

				if (path != null && path.Contains(n))
				{
					Gizmos.color = Color.blue;
					Gizmos.DrawCube(n.worldPos, Vector3.one * (walkableDiameter - .1f));
				}
				else
				{
					Gizmos.color = n.walkable ? Color.green : Color.red;
					
					Gizmos.DrawCube(n.worldPos, Vector3.one * (walkableDiameter - .1f));
				}
				
			}
			
		}
		
	}
	
}