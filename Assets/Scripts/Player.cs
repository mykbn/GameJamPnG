using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	public static Player Instance;

	public GameObject goPlayer;
	public Grid grid;
	public Node currentNode;
	public DIRECTIONS currentDirection;

	void Awake(){
		Instance = this;
	}

	// Use this for initialization
	void Start () {
		SwipeControls.Swipe += FacePlayer;
		currentNode = grid.NodeFromWorldPoint(goPlayer.transform.position);
		goPlayer.transform.position = currentNode.worldPosition;
		Debug.Log(goPlayer.transform.position);

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
	public void MovePlayer(){
		List<Node> neighbors = grid.GetNeighbours(currentNode);
		if(currentDirection == DIRECTIONS.LEFT){
			goPlayer.GetComponent<TweenPosition>().ResetToBeginning();
			goPlayer.GetComponent<TweenPosition>().from = currentNode.worldPosition;
			goPlayer.GetComponent<TweenPosition>().to = neighbors[1].worldPosition;
			goPlayer.GetComponent<TweenPosition>().PlayForward();
//			goPlayer.transform.position = neighbors[1].worldPosition;
			currentNode = neighbors[1];
			return;

		}
		if(currentDirection == DIRECTIONS.RIGHT){
			goPlayer.GetComponent<TweenPosition>().ResetToBeginning();
			goPlayer.GetComponent<TweenPosition>().from = currentNode.worldPosition;
			goPlayer.GetComponent<TweenPosition>().to = neighbors[6].worldPosition;
			goPlayer.GetComponent<TweenPosition>().PlayForward();
//			goPlayer.transform.position = neighbors[6].worldPosition;
			currentNode = neighbors[6];
			return;

		}
		if(currentDirection == DIRECTIONS.UP){
			goPlayer.GetComponent<TweenPosition>().ResetToBeginning();
			goPlayer.GetComponent<TweenPosition>().from = currentNode.worldPosition;
			goPlayer.GetComponent<TweenPosition>().to = neighbors[4].worldPosition;
			goPlayer.GetComponent<TweenPosition>().PlayForward();
//			goPlayer.transform.position = neighbors[4].worldPosition;
			currentNode = neighbors[4];
			return;

		}
		if(currentDirection == DIRECTIONS.DOWN){
			goPlayer.GetComponent<TweenPosition>().ResetToBeginning();
			goPlayer.GetComponent<TweenPosition>().from = currentNode.worldPosition;
			goPlayer.GetComponent<TweenPosition>().to = neighbors[3].worldPosition;
			goPlayer.GetComponent<TweenPosition>().PlayForward();
//			goPlayer.transform.position = neighbors[3].worldPosition;
			currentNode = neighbors[3];
			return;

		}

	}
}
