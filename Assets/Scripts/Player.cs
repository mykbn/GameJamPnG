using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public GameObject goPlayer;
	public Grid grid;
	public Node currentNode;

	void Awake(){
	}

	// Use this for initialization
	void Start () {
		currentNode = grid.NodeFromWorldPoint(goPlayer.transform.position);
		goPlayer.transform.position = currentNode.worldPosition;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
