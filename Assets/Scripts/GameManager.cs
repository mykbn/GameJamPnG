using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public static GameManager Instance;
	
	public bool isGameStarted = false;
	public bool isGameOver = false;
	float timer = 0f;
	
	void Awake () {
		Instance = this;
	}
	
	void Start(){
		
	}
	
	// Update is called once per frame
	void Update () {
		if(isGameStarted){
			timer += Time.deltaTime;
			Player.Instance.SetScore(timer);
		}

		if(Player.Instance){
			if(Player.Instance.lightVal <= 0 || Player.Instance.foodVal <= 0){
				//SHOW GAME OVER
				if(!isGameOver){
					if(Player.Instance.lightVal <= 0){
						UserInterface.Instance.strGameOverMessage = "You ran out of fuel.";
					}else if(Player.Instance.foodVal <= 0){
						UserInterface.Instance.strGameOverMessage = "You died of hunger.";
					}else if(Player.Instance.lightVal <= 0 && Player.Instance.foodVal <= 0){
						UserInterface.Instance.strGameOverMessage = "You ran out of fuel and died of hunger.";
					}
					UserInterface.Instance.ShowGameOver();
				}
			}
		}
	}
	
	[ContextMenu("Start Game")]
	public void StartGame(){
		isGameStarted = true;
		InvokeRepeating("ReduceValues", 1f, 1f);
	}
	
	public void ReduceValues(){
		Player.Instance.ReduceLightValue(1f);
		Player.Instance.ReduceFoodValue(1f);
	}

	public void Retry(){
		Time.timeScale = 1f;
		Application.LoadLevel(Application.loadedLevelName);
	}
	
	
	
}
