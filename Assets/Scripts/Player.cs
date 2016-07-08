using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public GameObject goPlayer;
	public Grid grid;
	public Node currentNode;
	public DIRECTIONS currentDirection;

	void Awake(){
	}

	// Use this for initialization
	void Start () {
		SwipeControls.Swipe += FacePlayer;
		currentNode = grid.NodeFromWorldPoint(goPlayer.transform.position);
		goPlayer.transform.position = currentNode.worldPosition;
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void FacePlayer(DIRECTIONS direction){
		currentDirection = direction;
		goPlayer.transform.rotation = Quaternion.identity;
		if(direction == DIRECTIONS.LEFT){
			goPlayer.transform.Rotate(new Vector3(0f,0f,180f));
		}
		if(direction == DIRECTIONS.RIGHT){
			goPlayer.transform.Rotate(new Vector3(0f,0f,0f));

		}
		if(direction == DIRECTIONS.UP){
			goPlayer.transform.Rotate(new Vector3(0f,0f,90f));

		}
		if(direction == DIRECTIONS.DOWN){
			goPlayer.transform.Rotate(new Vector3(0f,0f,-90f));

		}

	}
}
