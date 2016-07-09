using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

	public bool onlyDisplayPathGizmos;
	public LayerMask unwalkableMask; //Layer Mask for unwalkable terrain
	public Vector2 gridWorldSize; //Size of the world
	public float nodeRadius; //Size of each node(square in the grid)
	Node[,] grid; //Array of nodes

	float nodeDiameter;
	int gridSizeX, gridSizeY;

	void Awake(){
		nodeDiameter = nodeRadius * 2; //Because diameter is always double the radius (basic geometry)
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter); //To find out how many nodes can fit in the x axis of the grid
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter); //To find out how many nodes can fit in the y(actually z-axis in 3d) axis of the grid
		CreateGrid (); //Method name sort of explains itself
	}

	public int MaxSize{
		get{
			return gridSizeX * gridSizeY;
		}
	}

	void CreateGrid(){
		grid = new Node[gridSizeX, gridSizeY]; //Assign array with how many nodes can fit in x and y axis
		Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;

		for(int x = 0; x < gridSizeX; x++){
			for(int y = 0; y < gridSizeY; y++){
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x*nodeDiameter + nodeRadius) + Vector3.up * (y*nodeDiameter + nodeRadius);
				bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
				grid[x,y] = new Node(walkable,worldPoint,x,y);
			}
		}
	}

	public List<Node> GetNeighbours(Node node){
		List<Node> neighbours = new List<Node> ();

		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				if(x == 0 && y == 0)
					continue;

				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				if(checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY){
					neighbours.Add(grid[checkX,checkY]);
				}
			}
		}

		return neighbours;
	}

	public Node NodeFromWorldPoint(Vector3 worldPosition){
		float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
		float percentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;
		percentX = Mathf.Clamp01 (percentX);
		percentY = Mathf.Clamp01 (percentY);

		int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

		return grid [x, y];
	}


	public List<Node> path;
	//For grid visualization
	void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3 (gridWorldSize.x,gridWorldSize.y,1));

		if (onlyDisplayPathGizmos) {
			if (path != null) {
				foreach (Node n in path) {
					Gizmos.color = Color.black;
					Gizmos.DrawCube (n.worldPosition, Vector3.one * (nodeDiameter - .1f));
				}
			}
		} else {
			if (grid != null) {
				foreach(Node n in grid){
					Gizmos.color = (n.walkable)?Color.white:Color.red;
					if(path != null)
						if(path.Contains(n))
							Gizmos.color = Color.black;
					Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter-.1f));
				}
			}
		}


	}
}
