using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	public static Player Instance;

	public GameObject goPlayer;
	public Rigidbody rgdPlayer;
	public Grid grid;
	public Node currentNode;
	public DIRECTIONS currentDirection;
	public Animator animPlayer;
	public MAZES currentMaze;

	public SpriteRenderer playerSprite;

	public bool isMoving;
	public float speed;

	public float lightVal;
	public float foodVal;
	public float score;
	public int difficulty = 0;

	void Awake(){
		if (Instance == null) {
			Instance = this; 
		} else {
			Instance = null;
			Instance = this;
		}
		SwipeControls.Swipe = null;
	}

	// Use this for initialization
	void Start () {
		SwipeControls.Swipe += PlayerDirection;
//		currentNode = grid.NodeFromWorldPoint(goPlayer.transform.position);
//		goPlayer.transform.position = currentNode.worldPosition;
//		Debug.Log(goPlayer.transform.position);
//		MovePlayer(currentDirection);
	}

	// Update is called once per frame
	void Update () {
		if(rgdPlayer.velocity == Vector3.zero){
			animPlayer.Play("idle",0,0f);
		}else{
			if(!animPlayer.GetCurrentAnimatorStateInfo(0).IsName("walk")){
				animPlayer.Play("walk",0,0f);
			}
		}
	}

	public void ReduceLightValue(float reductionValue){
		lightVal -= reductionValue;
		if (lightVal < 0f) {
			lightVal = 0f;
		}
	}

	public void IncreaseLightValue(float increaseValue){
		lightVal += increaseValue;
		if (lightVal >= 100f) {
			lightVal = 100f;
		}
	}

	public void ReduceFoodValue(float reductionValue){
		foodVal -= reductionValue;
		if (foodVal < 0f) {
			foodVal = 0f;
		}
	}

	public void IncreaseFoodValue(float increaseValue){
		foodVal += increaseValue;
		if (foodVal >= 100f) {
			foodVal = 100f;
		}
	}

	public void SetScore(float score){
		this.score = score;
	}

	public void PlayerDirection(DIRECTIONS direction){
		FacePlayer(direction);
	}

	public void FacePlayer(DIRECTIONS direction){
		if(direction == currentDirection){
			return;
		}
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
//		isMoving = true;
		MovePlayer(direction);
	}

	public void MovePlayer(DIRECTIONS direction){

		rgdPlayer.velocity = Vector3.zero;
		rgdPlayer.drag = 0f;
		if(direction == DIRECTIONS.LEFT){
			rgdPlayer.AddForce (Vector3.left * speed, ForceMode.Force);
		}
		if(direction == DIRECTIONS.RIGHT){
			rgdPlayer.AddForce (Vector3.right * speed, ForceMode.Force);

		}
		if(direction == DIRECTIONS.UP){
			rgdPlayer.AddForce (Vector3.up * speed, ForceMode.Force);

		}
		if(direction == DIRECTIONS.DOWN){
			rgdPlayer.AddForce (Vector3.down * speed, ForceMode.Force);

		}
	}

	public void MovePlayer(){
		List<Node> neighbors = grid.GetNeighbours(currentNode);
		if(currentDirection == DIRECTIONS.LEFT){
			goPlayer.GetComponent<TweenPosition>().from = currentNode.worldPosition;
			goPlayer.GetComponent<TweenPosition>().to = neighbors[1].worldPosition;
			goPlayer.GetComponent<TweenPosition>().ResetToBeginning();

			goPlayer.GetComponent<TweenPosition>().PlayForward();
//			goPlayer.transform.position = neighbors[1].worldPosition;
			currentNode = neighbors[1];
			return;

		}
		if(currentDirection == DIRECTIONS.RIGHT){
			goPlayer.GetComponent<TweenPosition>().from = currentNode.worldPosition;
			goPlayer.GetComponent<TweenPosition>().to = neighbors[6].worldPosition;
			goPlayer.GetComponent<TweenPosition>().ResetToBeginning();

			goPlayer.GetComponent<TweenPosition>().PlayForward();
//			goPlayer.transform.position = neighbors[6].worldPosition;
			currentNode = neighbors[6];
			return;

		}
		if(currentDirection == DIRECTIONS.UP){
			goPlayer.GetComponent<TweenPosition>().from = currentNode.worldPosition;
			goPlayer.GetComponent<TweenPosition>().to = neighbors[4].worldPosition;
			goPlayer.GetComponent<TweenPosition>().ResetToBeginning();

			goPlayer.GetComponent<TweenPosition>().PlayForward();
//			goPlayer.transform.position = neighbors[4].worldPosition;
			currentNode = neighbors[4];
			return;

		}
		if(currentDirection == DIRECTIONS.DOWN){
			goPlayer.GetComponent<TweenPosition>().from = currentNode.worldPosition;
			goPlayer.GetComponent<TweenPosition>().to = neighbors[3].worldPosition;
			goPlayer.GetComponent<TweenPosition>().ResetToBeginning();

			goPlayer.GetComponent<TweenPosition>().PlayForward();
//			goPlayer.transform.position = neighbors[3].worldPosition;
			currentNode = neighbors[3];
			return;

		}

	}

	public void StopMoving(){
		if(rgdPlayer.velocity == Vector3.zero){
			animPlayer.Play("idle",0,0f);
		}
	}

	public void HidePlayer(bool state){
		if (state) {
			playerSprite.color = Color.black;
		} else {
			playerSprite.color = Color.white;
		}
	}
	
	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag == "wall"){
			Debug.Log("WALL");
			StopMoving();
		}
		if(other.gameObject.tag == "hole"){
			Debug.Log("HOLE");
			//DIE
			UserInterface.Instance.ShowGameOver();
		}

	}

	private void IncreaseDifficulty(){
		if (MazeManager.Instance.intFoodCount != 0 || MazeManager.Instance.intFuelCount != 0) {
			return;
		}

		difficulty += 1;

		if (difficulty == 0) {
			MazeManager.Instance.SpawnFuelAndFood (8);
		}else if (difficulty == 1) {
			MazeManager.Instance.SpawnFuelAndFood(6);
		}else if (difficulty == 2) {
			MazeManager.Instance.SpawnFuelAndFood(4);
		}else if (difficulty >= 3) {
			MazeManager.Instance.SpawnFuelAndFood(2);
			difficulty = 3;
		}

		MazeManager.Instance.RotateMazes();
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "fuel"){
			Debug.Log("FUEL");
			MazeManager.Instance.intFuelCount -= 1;
			IncreaseLightValue(30f);
			IncreaseDifficulty();
			Destroy(other.gameObject);
		}
		if(other.gameObject.tag == "food"){
			Debug.Log("FOOD");
			MazeManager.Instance.intFoodCount -= 1;
			IncreaseFoodValue(30f);
			IncreaseDifficulty();
			Destroy(other.gameObject);
		}
	}

	void OnTriggerStay(Collider other){
		if(other.gameObject.tag == "mazeA"){
			currentMaze = MAZES.A;
		}
		if(other.gameObject.tag == "mazeB"){
			currentMaze = MAZES.B;
		}
		if(other.gameObject.tag == "mazeC"){
			currentMaze = MAZES.C;
		}
		if(other.gameObject.tag == "mazeD"){
			currentMaze = MAZES.D;
		}
	}
}
