using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
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
	private List<Node> path;

	public LayerMask wallMask;
	public Vector2 worldSize;
	public float nodeRadius;
	private Node[,] grid;
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
		grid = new Node[gridSizeX,gridSizeY];

		Vector3 bottomLeft = transform.position - Vector3.right * worldSize.x / 2 - Vector3.forward * worldSize.y / 2;

		for (int x = 0; x < gridSizeX; x++)
		{
			for (int y = 0; y < gridSizeY; y++)
			{
				Vector3 worldPoint = bottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
				bool walkable = !Physics.CheckSphere(worldPoint, nodeRadius, wallMask);
				grid[x,y] = new Node(walkable, worldPoint, x, y);
			}
		}
		
	}

	public Node nodeFromWorldPos(Vector3 pos)
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

	public List<Node> getNeighbours(Node node)
	{
		List<Node> neighbours = new List<Node>();

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
		Node startNode = nodeFromWorldPos(transform.position);
		// End node
		Node endNode = nodeFromWorldPos(target);
		
		// A list of all open nodes
		List<Node> openNodes = new List<Node>();
		// A hashset of all closed nodes
		HashSet<Node> closedNodes = new HashSet<Node>();
		
		// Add start node to open nodes
		openNodes.Add(startNode);

		// While there are open nodes keep looping
		while (openNodes.Count > 0)
		{
			
			// Get the node with the lowers f cost
			Node currentNode = openNodes[0];
			for (int i = 0; i < openNodes.Count; i++)
			{
				if (openNodes[i].fCost() < currentNode.fCost() || openNodes[i].fCost() == currentNode.fCost() && openNodes[i].hCost < currentNode.hCost)
				{
					currentNode = openNodes[i];
				}
			}

			// Remove node from open nodes and add to close nodes
			openNodes.Remove(currentNode);
			closedNodes.Add(currentNode);

			// If node is end node finished path
			if (currentNode == endNode)
			{
				// A list with all the nodes used in the path
				path = getPath(currentNode);
				return;
			}

			foreach (Node neighbour in getNeighbours(currentNode))
			{
				if (!neighbour.walkable || closedNodes.Contains(neighbour))
				{
					continue;
				}

				int newMovementCost = currentNode.gCost + getDistance(currentNode, neighbour);

				if (newMovementCost < currentNode.gCost || !openNodes.Contains(neighbour))
				{
					neighbour.gCost = newMovementCost;
					neighbour.hCost = getDistance(neighbour, endNode);
					neighbour.parent = currentNode;
					if (!openNodes.Contains(neighbour))
					{
						openNodes.Add(neighbour);
					}
				}
			}
		}
	}

	private List<Node> getPath(Node endNode)
	{
		List<Node> path = new List<Node>();

		Node currentNode = endNode;

		while (currentNode.parent != null)
		{
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}

		path.Reverse();
		
		return path;

	}

	private int getDistance(Node nodeA, Node nodeB)
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
			Node playerNode = nodeFromWorldPos(target);
			
			foreach (Node n in grid)
			{

				if (n == playerNode)
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